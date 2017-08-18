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

    [TestMethod]
    public void Find_FindsClientInDatabase_Client()
    {
      Client testClient = new Client("Abby", "Kline", "303-555-1234", "abby.kline@gmail.com", 1);
      testClient.Save();
      Client foundClient = Client.Find(testClient.GetId());
      Assert.AreEqual(testClient, foundClient);
    }

    [TestMethod]
    public void Update_UpdatesClientLastNameInDatabase_String()
    {
      Client testClient = new Client("Abby", "Kline", "303-555-1234", "abby.kline@gmail.com", 1);
      testClient.Save();
      string newLastName = "Berry";
      testClient.UpdateClientLastName(newLastName);
      string result = testClient.GetLastName();
      Assert.AreEqual(newLastName, result);
    }

    [TestMethod]
    public void Update_UpdatesClientFirstNameInDatabase_String()
    {
      Client testClient = new Client("Abby", "Kline", "303-555-1234", "abby.kline@gmail.com", 1);
      testClient.Save();
      string newFirstName = "Ben";
      testClient.UpdateClientFirstName(newFirstName);
      string result = testClient.GetFirstName();
      Assert.AreEqual(newFirstName, result);
    }

    [TestMethod]
    public void Update_UpdatesClientPhoneInDatabase_String()
    {
      Client testClient = new Client("Abby", "Kline", "303-555-1234", "abby.kline@gmail.com", 1);
      testClient.Save();
      string newPhone = "808-555-1234";
      testClient.UpdateClientPhone(newPhone);
      string result = testClient.GetPhone();
      Assert.AreEqual(newPhone, result);
    }

    [TestMethod]
    public void Update_UpdatesClientEmailInDatabase_String()
    {
      Client testClient = new Client("Abby", "Kline", "303-555-1234", "abby.kline@hotmail.com", 1);
      testClient.Save();
      string newEmail = "abby.kline@gmail.com";
      testClient.UpdateClientEmail(newEmail);
      string result = testClient.GetEmail();
      Assert.AreEqual(newEmail, result);
    }

    [TestMethod]
    public void Update_UpdatesClientInDatabase_True()
    {
      Client testClient = new Client("Abigail", "Kline", "303-555-1234", "abby.kline@hotmail.com", 1);
      testClient.Save();
      string newFirstName = "Abby";
      string newLastName = "Ford";
      string newPhone = "808-555-1234";
      string newEmail = "abs.8888@gmail.com";
      testClient.UpdateClient(newFirstName, newLastName, newPhone, newEmail);
      bool results = (testClient.GetFirstName() == newFirstName && testClient.GetLastName() == newLastName && testClient.GetPhone() == newPhone && testClient.GetEmail() == newEmail);
      Assert.AreEqual(true, results);
    }

    [TestMethod]
    public void DeleteClient_RemoveOneClientFromList_ClientList()
    {
      Client newClient1 = new Client("Abigail", "Kline", "303-555-1234", "abby.kline@hotmail.com", 1);
      newClient1.Save();
      Client newClient2 = new Client("Sam", "Ruth", "206-555-1234", "sam.ruth@gmail.com", 2);
      newClient2.Save();
      Client.DeleteClient(newClient1.GetId());
      List<Client> allClients = Client.GetAll();
      List<Client> expectedList = new List<Client>{newClient2};
      CollectionAssert.AreEqual(allClients, expectedList);
    }

    [TestMethod]
    public void GetStylistName_ReturnsStylistTypeForClient_String()
    {
      Stylist newStylist = new Stylist("Julie", "Reynolds", 50, 40, 1);
      newStylist.Save();
      Client newClient = new Client("Abigail", "Kline", "303-555-1234", "abby.kline@hotmail.com", newStylist.GetId());
      newClient.Save();
      Assert.AreEqual("Julie", newClient.GetStylist());
    }

  }
}
