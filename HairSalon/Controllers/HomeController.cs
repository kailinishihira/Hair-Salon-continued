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
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/stylists/new-stylist")]
    public ActionResult AddStylist()
    {
      return View();
    }

    [HttpPost("/stylists/stylists-all")]
    public ActionResult NewStylist()
    {
      Stylist newStylist = new Stylist(Request.Form["stylist-first-name"], Request.Form["stylist-last-name"], int.Parse(Request.Form["stylist-womens-cut"]), int.Parse(Request.Form["stylist-mens-cut"]));
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      return View("Index", allStylists);
    }

    [HttpGet("/stylists/stylists-all")]
    public ActionResult StylistsList()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View("AllStylists", allStylists);
    }

    [HttpGet("/stylists/clients-all")]
    public ActionResult AllClients(int id)
    {
      List<Client> allClients = Client.GetAll();
      return View(allClients);
    }

    [HttpGet("/stylists/new-client")]
    public ActionResult AddClient()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View("AddClient", allStylists);
    }

    [HttpPost("/stylists/new-client/add")]
    public ActionResult AddNewClient()
    {
      Client newClient = new Client(Request.Form["client-first-name"], Request.Form["client-last-name"], Request.Form["client-phone"], Request.Form["client-email"], int.Parse(Request.Form["stylist-id"]));
      newClient.Save();
      List<Client> allClients = Client.GetAll();
      return View("AllClients", allClients);
    }

    [HttpPost("/stylists/{id}/stylists/new-client")]
    public ActionResult StylistAddClient(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      Client newClient = new Client(Request.Form["client-first-name"], Request.Form["client-last-name"], Request.Form["client-phone"], Request.Form["client-email"], id);
      newClient.Save();
      List<Client> allClients = thisStylist.GetClients();
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("stylists", thisStylist);
      model.Add("clients", allClients);
      return View("StylistDetails", model);
    }

    [HttpGet("/stylists/{id}/details")]
    public ActionResult AllClientsList(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      List<Client> allClients = thisStylist.GetClients();
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("stylists", thisStylist);
      model.Add("clients", allClients);
      return View("StylistDetails", model);
    }

    [HttpGet("/stylists/{id}/details/delete")]
    public ActionResult DeleteStylist(int id)
    {
      Stylist.DeleteStylist(id);
      return View();
    }

    [HttpGet("/stylists/{id}/details/edit")]
    public ActionResult EditStylist(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      return View(thisStylist);
    }

    [HttpPost("/stylists/{id}/details/edit-first-name")]
    public ActionResult EditStylistFirstName(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      thisStylist.UpdateStylistFirstName(Request.Form["stylist-first-name"]);
      return View("EditStylist", thisStylist);
    }

    [HttpPost("/stylists/{id}/details/edit-last-name")]
    public ActionResult EditStylistLastName(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      thisStylist.UpdateStylistLastName(Request.Form["stylist-last-name"]);
      return View("EditStylist", thisStylist);
    }

    [HttpPost("/stylists/{id}/details/edit-womens-cut")]
    public ActionResult EditStylistWomensCut(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      thisStylist.UpdateStylistWomensCut(int.Parse(Request.Form["stylist-womens-cut"]));
      return View("EditStylist", thisStylist);
    }

    [HttpPost("/stylists/{id}/details/edit-mens-cut")]
    public ActionResult EditStylistMensCut(int id)
    {
      Stylist thisStylist = Stylist.Find(id);
      thisStylist.UpdateStylistMensCut(int.Parse(Request.Form["stylist-mens-cut"]));
      return View("EditStylist", thisStylist);
    }

    [HttpGet("/stylists/{id}/details/{id2}details")]
    public ActionResult ClientDetails(int id2)
    {
      Client thisClient = Client.Find(id2);
      return View(thisClient);
    }

    [HttpPost("/stylists/{id}/details/{id2}details/edit-first-name")]
    public ActionResult ClientEditFirstName(int id2)
    {
      Client thisClient = Client.Find(id2);
      thisClient.UpdateClientFirstName(Request.Form["client-first-name"]);
      return RedirectToAction("ClientDetails", thisClient);
    }

    [HttpPost("/stylists/{id}/details/{id2}details/edit-last-name")]
    public ActionResult ClientEditLastName(int id2)
    {
      Client thisClient = Client.Find(id2);
      thisClient.UpdateClientLastName(Request.Form["client-last-name"]);
      return RedirectToAction("ClientDetails", thisClient);
    }

    [HttpPost("/stylists/{id}/details/{id2}details/edit-phone")]
    public ActionResult ClientEditPhone(int id2)
    {
      Client thisClient = Client.Find(id2);
      thisClient.UpdateClientPhone(Request.Form["client-phone"]);
      return RedirectToAction("ClientDetails", thisClient);
    }

    [HttpPost("/stylists/{id}/details/{id2}details/edit-email")]
    public ActionResult ClientEditEmail(int id2)
    {
      Client thisClient = Client.Find(id2);
      thisClient.UpdateClientEmail(Request.Form["client-email"]);
      return RedirectToAction("ClientDetails", thisClient);
    }

    [HttpPost("/stylists/{id}/details/{id2}details/edit-stylist-id")]
    public ActionResult ClientEditStylistID(int id2)
    {
      Client thisClient = Client.Find(id2);
      thisClient.UpdateClientStylistId(int.Parse(Request.Form["stylist-id"]));
      return RedirectToAction("ClientDetails", thisClient);
    }

    [HttpGet("/stylists/{id}/details/{id2}details/delete")]
    public ActionResult DeleteClient(int id, int id2)
    {
      Client.DeleteClient(id2);
      Stylist thisStylist = Stylist.Find(id);
      List<Client> allClients = thisStylist.GetClients();
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("stylists", thisStylist);
      model.Add("clients", allClients);
      return View(model);
    }

  }
}
