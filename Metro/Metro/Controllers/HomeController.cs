using System;
using System.Linq;
using System.Web.Mvc;

namespace Metro.Controllers
{
	public class HomeController : Controller
	{
		public HomeController() {}

		public HomeController(MetroClient.MetroClient metroiClient)
		{
			m_metroClient = metroiClient;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public ActionResult Route(int departure, int arrival)
		{
			MetroClient.MetroClient client = new MetroClient.MetroClient(new Uri(c_metroUri));

			ViewBag.Vehicles = client.GetVehicles(c_routeId).Vehicles.ToList();

			ViewBag.Departure = departure;
			ViewBag.Arrival = arrival;

			return View();
		}

		private readonly MetroClient.MetroClient m_metroClient;
		private const string c_metroUri = "http://api.metro.net/agencies/lametro/";
		private const string c_routeId = "704";
	}
}