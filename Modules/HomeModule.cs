using System.Collections.Generic;
using System;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace HairSalon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["index.cshtml", AllStylists];
      };

      Get["/stylists"] = _ => {
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["stylists.cshtml", AllStylists];
      };

      Get["/stylists/new"] = _ => {
        return View["stylists_form.cshtml"];
      };

      Post["/stylists/new"] = _ => {
        Stylist newStylist = new Stylist(Request.Form["stylist-name"], Request.Form["stylist-availability"], Request.Form["stylist-services"]);
        newStylist.Save();
        return View["success.cshtml"];
      };

      Get["/clients"] = _ => {
        List<Client> AllClients = Client.GetAll();
        return View["clients.cshtml", AllClients];
      };

      Get["/clients/new"] = _ => {
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["clients_form.cshtml", AllStylists];
      };

      Post["/clients/new"] = _ => {
        Client newClient = new Client(Request.Form["client-name"], Request.Form["stylist-id"], Request.Form["client-appointment-day"], Request.Form["client-appointment-time"]);
        newClient.Save();
        return View["success.cshtml"];
      };
    }
  }
}
