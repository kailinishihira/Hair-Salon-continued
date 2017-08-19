using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=hair_salon_test;";
    }

    public void Dispose()
    {
      Client.DeleteAll();
      Stylist.DeleteAll();
    }

    [TestMethod]
    public void GetAll_StylistsEmptyAtFirst_0()
    {
      int result = Stylist.GetAll().Count;
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueForSameInfo_Stylist()
    {
      Stylist firstStylist = new Stylist("Kim", "Ito", 50, 40);
      Stylist secondStylist = new Stylist("Kim", "Ito", 50, 40);
      Assert.AreEqual(firstStylist, secondStylist);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToStylist_Id()
    {
      Stylist testStylist = new Stylist("Kim", "Ito", 50, 40);
      testStylist.Save();
      Stylist savedStylist = Stylist.GetAll()[0];
      int result = savedStylist.GetId();
      int testId = testStylist.GetId();
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Save_SavesStylistToDatabase_StylistList()
    {

      Stylist testStylist = new Stylist("Kim", "Ito", 50, 40);
      testStylist.Save();
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetAll_ListAllStylists_StylistList()
    {
      Stylist newStylist1 = new Stylist("Julie", "Oka", 80, 50);
      newStylist1.Save();
      Stylist newStylist2 = new Stylist("Kim", "Ito", 50, 40);
      newStylist2.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      List<Stylist> expectedList = new List<Stylist>{newStylist1, newStylist2};
      CollectionAssert.AreEqual(allStylists, expectedList);
    }

    [TestMethod]
    public void Find_FindsStylistInDatabase_Stylist()
    {
      Stylist testStylist = new Stylist("Kim", "Ito", 50, 40);
      testStylist.Save();
      Stylist foundStylist = Stylist.Find(testStylist.GetId());
      Assert.AreEqual(testStylist, foundStylist);
    }

    [TestMethod]
    public void UpdateStylistLastName_UpdatesStylistLastName_StylistLastName()
    {
      Stylist testStylist = new Stylist("Kim", "Ito", 50, 40);
      testStylist.Save();
      string newLastName = "Smith";
      testStylist.UpdateStylistLastName(newLastName);
      string result = testStylist.GetLastName();
      Assert.AreEqual(newLastName, result);
    }

    [TestMethod]
    public void UpdateStylistFirstName_UpdatesStylistFirstName_StylistFirstName()
    {
      Stylist testStylist = new Stylist("Kim", "Ito", 50, 40);
      testStylist.Save();
      string newFirstName = "Kimberly";
      testStylist.UpdateStylistFirstName(newFirstName);
      string result = testStylist.GetFirstName();
      Assert.AreEqual(newFirstName, result);
    }

    [TestMethod]
    public void UpdateStylistWomensCut_UpdatesStylistWomensCut_StylistWomensCut()
    {
      Stylist testStylist = new Stylist("Kim", "Ito", 50, 40);
      testStylist.Save();
      int newWomensCut = 60;
      testStylist.UpdateStylistWomensCut(newWomensCut);
      int result = testStylist.GetWomensCut();
      Assert.AreEqual(newWomensCut, result);
    }

    [TestMethod]
    public void UpdateStylistMensCut_UpdatesStylistMensCut_StylistMensCut()
    {
      Stylist testStylist = new Stylist("Kim", "Ito", 50, 40);
      testStylist.Save();
      int newMensCut = 45;
      testStylist.UpdateStylistMensCut(newMensCut);
      int result = testStylist.GetMensCut();
      Assert.AreEqual(newMensCut, result);
    }

    [TestMethod]
    public void UpdateStylist_UpdatesAllStylistsDetails_True()
    {
      Stylist testStylist = new Stylist("Kim", "Ito", 50, 40);
      testStylist.Save();
      string newFirstName = "Kimberly";
      string newLastName = "Smith";
      int newWomensCut = 60;
      int newMensCut = 50;

      testStylist.UpdateStylist(newFirstName, newLastName, newWomensCut, newMensCut);
      bool expected = (testStylist.GetFirstName() == newFirstName && testStylist.GetLastName() == newLastName && testStylist.GetWomensCut() == newWomensCut && testStylist.GetMensCut() == newMensCut);
      Assert.AreEqual(true, expected);
    }

    [TestMethod]
    public void GetClients_RetrievesAllStylistsClient_ClientList()
    {
      Stylist testStylist = new Stylist("Kim", "Ito", 50, 40);
      testStylist.Save();
      Client firstClient = new Client("Abby", "Kline", "303-555-1234", "abby.kline@gmail.com", testStylist.GetId());
      firstClient.Save();
      Client secondClient = new Client("Sam", "Ruth", "206-555-1234", "sam.ruth@gmail.com", testStylist.GetId());
      secondClient.Save();
      List<Client> testClientList = new List<Client> {firstClient, secondClient};
      List<Client> resultClientList = testStylist.GetClients();
      CollectionAssert.AreEqual(testClientList, resultClientList);
    }

    [TestMethod]
    public void DeleteStylist_RemoveOneStylistFromList_StylistList()
    {
      Stylist newStylist1 = new Stylist("Kim", "Ito", 50, 40);
      newStylist1.Save();
      Stylist newStylist2 = new Stylist("Ian", "Wynn", 60, 50);
      newStylist2.Save();
      Stylist.DeleteStylist(newStylist1.GetId());
      List<Stylist> allStylists = Stylist.GetAll();
      List<Stylist> expectedList = new List<Stylist>{newStylist2};
      CollectionAssert.AreEqual(allStylists, expectedList);
    }

  }
}
