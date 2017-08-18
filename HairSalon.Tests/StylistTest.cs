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






  }
}
