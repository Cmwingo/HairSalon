using System.Collections.Generic;
using System;
using Xunit;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Client.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_EqualityMethodOverrideWorks()
    {
      //Arrange
      Client firstClient = new Client("Susie", 1, "Monday", "11:30");
      Client secondClient = new Client("Susie", 1, "Monday", "11:30");

      //Assert
      Assert.Equal(firstClient, secondClient);
    }

    [Fact]
    public void Test_SavesRestaurantToDatabase()
    {
      //Arrange
      Client testClient = new Client("Susie", 1, "Monday", "11:30");

      //Act
      testClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      Client testClient = new Client("Susie", 1, "Monday", "11:30");
      testClient.Save();
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsClientInDatabase()
    {
      Client testClient = new Client("Susie", 1, "Monday", "11:30");
      testClient.Save();

      Client foundClient = Client.Find(testClient.GetId());

      Assert.Equal(testClient, foundClient);
    }

    [Fact]
    public void Test_Delete_DeletesClientInDatabase()
    {
      //Arrange
      Client testClient1 = new Client("Susie", 1, "Monday", "11:30");
      Client testClient2 = new Client("Andrew", 2, "Monday", "11:30");


      //Act
      testClient1.Save();
      testClient2.Save();
      Client.Delete(testClient1.GetId());
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client> {testClient2};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Update_UpdatesClientsInDatabase()
    {
      //Arrange
      string name = "Lisa";
      Client testClient = new Client(name,1, "Monday", "11:30");
      testClient.Save();
      string newName = "Kim";

      //Act
      testClient.Update(newName);
      string result = testClient.GetName();

      //Assert
      Assert.Equal(newName, result);
    }

    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}
