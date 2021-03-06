using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Xunit;

namespace HairSalon
{
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_StylistsEmptyAtStart_True()
    {
      //Arrange
      int result = Stylist.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      Stylist testStylist1 = new Stylist("Lisa", "MTuWTh", "Color and Style");
      Stylist testStylist2 = new Stylist("Lisa", "MTuWTh", "Color and Style");

      Assert.Equal(testStylist1, testStylist2);
    }

    [Fact]
    public void Test_Save_SavesStylistToDatabase()
    {
      //Arrange
      Stylist testStylist = new Stylist("Lisa", "MTuWTh", "Color and Style");

      //Act
      testStylist.Save();
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist> {testStylist};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToStylistObjects()
    {
      //Arrange
      Stylist testStylist = new Stylist("Lisa", "MTuWTh", "Color and Style");

      //Act
      testStylist.Save();
      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.GetId();
      int testId = testStylist.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsStylistInDatabase()
    {
      //Arrange
      Stylist testStylist = new Stylist("Lisa", "MTuWTh", "Color and Style");

      //Act
      testStylist.Save();
      Stylist foundStylist = Stylist.Find(testStylist.GetId());

      //Assert
      Assert.Equal(testStylist, foundStylist);
    }

    [Fact]
    public void Test_Delete_DeletesStylistInDatabase()
    {
      //Arrange
      Stylist testStylist1 = new Stylist("Lisa", "MTuWTh", "Color and Style");
      Stylist testStylist2 = new Stylist("Kim", "TuWFSa", "Cuts Only");


      //Act
      testStylist1.Save();
      testStylist2.Save();
      Stylist.Delete(testStylist1.GetId());
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist> {testStylist2};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_GetClients_RetrievesAllClientsWithStylist()
    {
      Stylist testStylist = new Stylist("Lisa", "MTuWTh", "Color and Style");
      testStylist.Save();

      Client firstClient = new Client("Susie", testStylist.GetId(), "Monday", "11:30");
      firstClient.Save();
      Client secondClient = new Client("Andrew", testStylist.GetId(), "Monday", "11:30");
      secondClient.Save();

      List<Client> testClientList = new List<Client>{firstClient, secondClient};
      List<Client> resultClientList = testStylist.GetClients();

      Assert.Equal(testClientList, resultClientList);

    }

    [Fact]
    public void Test_Update_UpdatesStylistsInDatabase()
    {
      //Arrange
      string availability = "MTuWTh";
      Stylist testStylist = new Stylist("Lisa", availability, "Color and Style");
      testStylist.Save();
      string newAvailability = "TuWThF";

      //Act
      testStylist.Update(newAvailability, "Color and Style");
      string result = testStylist.GetAvailability();

      //Assert
      Assert.Equal(newAvailability, result);
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
      Client.DeleteAll();
    }
  }
}
