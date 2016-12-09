using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace HairSalon
{
  public class Client
  {
    private int _id;
    private string _name;
    private int _stylistId;
    private string _appointmentDay;
    private string _appointmentTime;

    public Client(string Name, int StylistId, string AppointmentDay, string AppointmentTime, int Id=0)
    {
      _id = Id;
      _name = Name;
      _stylistId = StylistId;
      _appointmentDay = AppointmentDay;
      _appointmentTime = AppointmentTime;
    }

    public override bool Equals(System.Object otherClient)
    {
      if(!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client) otherClient;
        bool idEquality = this.GetId() == newClient.GetId();
        bool nameEquality = this.GetName() == newClient.GetName();
        bool stylistIdEquality = (this.GetStylistId() == newClient.GetStylistId());
        return(idEquality && nameEquality && stylistIdEquality);
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
    public int GetStylistId()
    {
      return _stylistId;
    }
    public string GetAppointmentDay()
    {
      return _appointmentDay;
    }
    public string GetAppointmentTime()
    {
      return _appointmentTime;
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      //Add object data fields for collection from DB here
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int clientStylistId = rdr.GetInt32(2);
        string clientAppointmentDay = rdr.GetString(3);
        string clientAppointmentTime = rdr.GetString(4);
        Client newClient = new Client(clientName, clientStylistId, clientAppointmentDay, clientAppointmentTime, clientId);
        allClients.Add(newClient);
      }

      if(rdr != null)
      {
        rdr.Close();
      }

      if(conn != null)
      {
        conn.Close();
      }

      return allClients;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      //Add Object data fields to SQLCMD and Parameters here
      SqlCommand cmd = new SqlCommand("INSERT INTO clients (name, stylist_id, appointment_day, appointment_time) OUTPUT INSERTED.id VALUES (@ClientName, @StylistId, @AppointmentDay, @AppointmentTime);", conn);

      SqlParameter clientNameParameter = new SqlParameter();
      clientNameParameter.ParameterName = "@ClientName";
      clientNameParameter.Value = this.GetName();

      SqlParameter clientStylistIdParameter = new SqlParameter();
      clientStylistIdParameter.ParameterName = "@StylistId";
      clientStylistIdParameter.Value = this.GetStylistId();

      SqlParameter clientAppointmentDayParameter = new SqlParameter();
      clientAppointmentDayParameter.ParameterName = "@AppointmentDay";
      clientAppointmentDayParameter.Value = this.GetAppointmentDay();

      SqlParameter clientAppointmentTimeParameter = new SqlParameter();
      clientAppointmentTimeParameter.ParameterName = "@AppointmentTime";
      clientAppointmentTimeParameter.Value = this.GetAppointmentTime();

      cmd.Parameters.Add(clientNameParameter);
      cmd.Parameters.Add(clientStylistIdParameter);
      cmd.Parameters.Add(clientAppointmentDayParameter);
      cmd.Parameters.Add(clientAppointmentTimeParameter);

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

    public static Client Find(int clientId)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id=@ClientId;", conn);
      SqlParameter clientIdParameter = new SqlParameter();
      clientIdParameter.ParameterName = "@ClientId";
      clientIdParameter.Value = clientId.ToString();
      cmd.Parameters.Add(clientIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      //Add Object data fields here to create object with the found record's data
      int foundClientId = 0;
      string foundClientName = null;
      int foundClientStylistId = 0;
      string foundClientAppointmentDay = null;
      string foundClientAppointmentTime = null;

      while(rdr.Read())
      {
        foundClientId = rdr.GetInt32(0);
        foundClientName = rdr.GetString(1);
        foundClientStylistId = rdr.GetInt32(2);
        foundClientAppointmentDay = rdr.GetString(3);
        foundClientAppointmentTime = rdr.GetString(4);
      }
      Client foundClient = new Client(foundClientName, foundClientStylistId, foundClientAppointmentDay, foundClientAppointmentTime, foundClientId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return foundClient;
    }

    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      //Add fields to be updated to the command line and the parameter set
      SqlCommand cmd = new SqlCommand("UPDATE clients SET name=@NewName OUTPUT INSERTED.name WHERE id=@ClientId;", conn);

      SqlParameter newNameParameter = new SqlParameter();
      newNameParameter.ParameterName = "@NewName";
      newNameParameter.Value = newName;
      cmd.Parameters.Add(newNameParameter);
      SqlParameter clientIdParameter = new SqlParameter();
      clientIdParameter.ParameterName = "@ClientId";
      clientIdParameter.Value = this.GetId();
      cmd.Parameters.Add(clientIdParameter);

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

    public static void Delete(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id=@ClientId;", conn);
      SqlParameter clientIdParameter = new SqlParameter();
      clientIdParameter.ParameterName = "@ClientId";
      clientIdParameter.Value = id.ToString();
      cmd.Parameters.Add(clientIdParameter);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
