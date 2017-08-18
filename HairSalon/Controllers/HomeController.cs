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

    [HttpGet("/stylists/stylists-all")]
    public ActionResult StylistsList()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View("AllStylists", allStylists);
    }

    [HttpGet("/stylists/{id}/{firstname}-details")]
    public ActionResult StylistDetails(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      return View(thisStylist);
    }

    [HttpGet("/stylists/{id}/{firstname}-details/add-client")]
    public ActionResult AddClient(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      return View(thisStylist);
    }

    [HttpPost("/stylists/{id}/{firstname}-details/client-list")]
    public ActionResult ClientList(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      Client newClient = new Client(Request.Form["client-first-name"], Request.Form["client-last-name"], Request.Form["client-phone"], Request.Form["client-email"], id);
      newClient.Save();
      List<Client> allClients = thisStylist.GetClients();
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("stylists", thisStylist);
      model.Add("clients", allClients);
      return View("ClientList", model);
    }

    [HttpGet("/stylists/{id}/{firstname}-details/client-list")]
    public ActionResult AllClientsList(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      List<Client> allClients = thisStylist.GetClients();
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("stylists", thisStylist);
      model.Add("clients", allClients);
      return View("ClientList", model);
    }

    [HttpGet("/stylists/{id}/{firstname}-details/client-list/{id2}details")]
    public ActionResult ClientDetails(int id2)
    {
      Client thisClient = Client.Find(id2);
      return View(thisClient);
    }

    [HttpGet("/stylists/{id}/{firstname}-details/client-list/{id2}details/edit")]
    public ActionResult EditClient(int id2)
    {
      Client thisClient = Client.Find(id2);
      return View(thisClient);
    }


  }
}
