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
      Client firstClient = new Client("Susie");
      Client secondClient = new Client("Susie");

      //Assert
      Assert.Equal(firstClient, secondClient);
    }

    [Fact]
    public void Test_SavesRestaurantToDatabase()
    {
      //Arrange
      Client testClient = new Client("Susie");

      //Act
      testClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      //Assert
      Assert.Equal(testList, result);
    }

    public void Dispose()
    {
      Client.DeleteAll();
    }
  }
}
