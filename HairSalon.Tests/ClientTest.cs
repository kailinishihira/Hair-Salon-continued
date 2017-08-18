using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTest : IDisposable
  {
    public ClientTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=hair_salon_test;";
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
      Client.DeleteAll();
    }

    [TestMethod]
    public void GetAll_ClientsEmptyAtFirst_0()
    {
      int result = Client.GetAll().Count;
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ClientsAreEqual_true()
    {
      Client newClient = new Client("Abby", "Kline", "303-555-1234", "abby.kline@gmail.com", 1);
      Client newClient2 = new Client("Abby", "Kline", "303-555-1234", "abby.kline@gmail.com", 1);
      Assert.AreEqual(newClient, newClient2);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      Client testClient = new Client("Abby", "Kline", "303-555-1234", "abby.kline@gmail.com", 1);
      testClient.Save();
      Client savedClient = Client.GetAll()[0];
      int result = savedClient.GetId();
      int testId = testClient.GetId();
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Save_SavesToDatabase_ClientList()
    {
      Client newClient = new Client("Abby", "Kline", "303-555-1234", "abby.kline@gmail.com", 1);
      newClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> expected = new List<Client>{newClient};
      Console.WriteLine(newClient.GetDetails());
      CollectionAssert.AreEqual(expected, result);
    }

    [TestMethod]
    public void GetAll_Lists2Clients_Clients()
    {
      Client newClient1 = new Client("Abby", "Kline", "303-555-1234", "abby.kline@gmail.com", 1);
      newClient1.Save();
      Client newClient2 = new Client("Sam", "Ruth", "206-555-1234", "sam.ruth@gmail.com", 2);
      newClient2.Save();
      int result = Client.GetAll().Count;
      Assert.AreEqual(2, result);
    }

    [TestMethod]
    public void GetAll_ListofClients_ClientList()
    {
      Client newClient1 = new Client("Abby", "Kline", "303-555-1234", "abby.kline@gmail.com", 1);
      newClient1.Save();
      Client newClient2 = new Client("Sam", "Ruth", "206-555-1234", "sam.ruth@gmail.com", 2);
      newClient2.Save();
      List<Client> allClients = Client.GetAll();
      List<Client> expectedList = new List<Client>{newClient1, newClient2};
      CollectionAssert.AreEqual(allClients, expectedList);
    }


  }
}
