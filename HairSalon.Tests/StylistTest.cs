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
      Stylist newStylist1 = new Stylist("Kim", "Ito", 50, 40);
      newStylist1.Save();
      Stylist newStylist2 = new Stylist("Julie", "Oka", 80, 50);
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
    public void UpdateStylistLastName_UpdatesStylistLastName_StylistName()
    {
      Stylist testStylist = new Stylist("Kim", "Ito", 50, 40);
      testStylist.Save();
      string newLastName = "Smith";
      testStylist.UpdateStylistLastName(newLastName);
      string result = testStylist.GetLastName();
      Assert.AreEqual(newLastName, result);
    }

  }
}
