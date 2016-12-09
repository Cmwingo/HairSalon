using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HairSalon
{
  public class Stylist
  {
    private int _id;
    private string _name;
    private string _availability;
    private string _services;

    public Stylist(string Name, string Availability, string Services, int Id=0)
    {
      _id = Id;
      _name = Name;
      _availability = Availability;
      _services = Services;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if(!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = this.GetId() == newStylist.GetId();
        bool nameEquality = this.GetName() == newStylist.GetName();
        return(idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }
    public string GetAvailability()
    {
      return _availability;
    }
    public string GetServices()
    {
      return _services;
    }

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      //Add Object data fields here to be retrieved from DB
      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        string stylistAvailability = rdr.GetString(2);
        string stylistServices = rdr.GetString(3);
        Stylist newStylist = new Stylist(stylistName, stylistAvailability, stylistServices, stylistId);
        allStylists.Add(newStylist);
      }

      if(rdr != null)
      {
        rdr.Close();
      }

      if(conn != null)
      {
        conn.Close();
      }

      return allStylists;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      //Add Object data fields to the SQLCMD and the Parameters to be saved to the DB
      SqlCommand cmd = new SqlCommand("INSERT INTO stylists (name, availability, services) OUTPUT INSERTED.id VALUES (@StylistName, @StylistAvailability, @StylistServices);", conn);

      SqlParameter stylistNameParameter = new SqlParameter();
      stylistNameParameter.ParameterName = "@StylistName";
      stylistNameParameter.Value = this.GetName();

      SqlParameter stylistAvailabilityParameter = new SqlParameter();
      stylistAvailabilityParameter.ParameterName = "@StylistAvailability";
      stylistAvailabilityParameter.Value = this.GetAvailability();

      SqlParameter stylistServicesParameter = new SqlParameter();
      stylistServicesParameter.ParameterName = "@StylistServices";
      stylistServicesParameter.Value = this.GetServices();

      cmd.Parameters.Add(stylistNameParameter);      cmd.Parameters.Add(stylistAvailabilityParameter);
      cmd.Parameters.Add(stylistServicesParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static Stylist Find(int stylistId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE id=@StylistId;", conn);
      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = stylistId.ToString();
      cmd.Parameters.Add(stylistIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      //Add Object data fields to be populated with the found record
      int foundStylistId = 0;
      string foundStylistName = null;
      string foundStylistAvailability = null;
      string foundStylistServices = null;

      while(rdr.Read())
      {
        foundStylistId = rdr.GetInt32(0);
        foundStylistName = rdr.GetString(1);
        foundStylistAvailability = rdr.GetString(2);
        foundStylistServices = rdr.GetString(3);
      }
      Stylist foundStylist = new Stylist(foundStylistName, foundStylistAvailability, foundStylistServices, foundStylistId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return foundStylist;
    }

    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      //Add fields to be updated to the command line and the parameter set
      SqlCommand cmd = new SqlCommand("UPDATE stylists SET name=@NewName OUTPUT INSERTED.name WHERE id=@StylistId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);
      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = this.GetId();
      cmd.Parameters.Add(stylistIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._name = rdr.GetString(0);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Client> GetClients()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE stylist_id=@StylistId;", conn);
      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = this.GetId();
      cmd.Parameters.Add(stylistIdParameter);

      List<Client> AllClients = new List<Client>{};
      SqlDataReader rdr = cmd.ExecuteReader();

      //Add Object data fields from Client class here
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int clientStylistId = rdr.GetInt32(2);
        string clientAppointmentDay = rdr.GetString(3);
        string clientAppointmentTime = rdr.GetString(4);
        Client newClient = new Client(clientName, clientStylistId, clientAppointmentDay, clientAppointmentTime, clientId);
        AllClients.Add(newClient);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return AllClients;
    }


    public static void Delete(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM stylists WHERE id=@StylistId;", conn);
      SqlParameter stylistIdParameter = new SqlParameter();
      stylistIdParameter.ParameterName = "@StylistId";
      stylistIdParameter.Value = id.ToString();
      cmd.Parameters.Add(stylistIdParameter);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
