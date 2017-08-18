using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet("/stylists/add")]
    public ActionResult AddStylist()
    {
      return View();
    }

    [HttpPost("/stylists/stylists-all")]
    public ActionResult AllStylists()
    {
      Stylist newStylist = new Stylist(Request.Form["stylist-first-name"], Request.Form["stylist-last-name"], int.Parse(Request.Form["stylist-womens-cut"]), int.Parse(Request.Form["stylist-mens-cut"]));
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

  }
}
