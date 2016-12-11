using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using MetroClient.Models;
using WebGrease.Css.Extensions;

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

			ReadOnlyCollection<PredictionDto> predictions = client.GetPredictions(c_routeId, departure.ToString()).Predictions.ToSafeReadOnlyCollection();
			ReadOnlyCollection<VehicleDto> vehicles = client.GetVehicles(c_routeId).Vehicles.ToSafeReadOnlyCollection();
			TravelInformationDto travelInformation = client.GetTravelInformationDto(c_routeId, departure.ToString(), arrival.ToString());

			ViewBag.NextBusMessage = GetNextBusDepartureMessage(predictions);
			ViewBag.OtherBusTimes = GetOtherBusTimes(predictions);

			ViewBag.Vehicles = client.GetVehicles(c_routeId).Vehicles.ToList();
			ViewBag.Message = travelInformation.Message;
			ViewBag.TravelTime = $"Your trip will take you {travelInformation.TravelDurationMinutes} minutes!";

			return View();
		}

		private static string GetNextBusDepartureMessage(ReadOnlyCollection<PredictionDto> predictions)
		{
			if (predictions.First().Minutes < 1)
				return $"Hurry! Your next bus leaves in {predictions.First().Seconds} seconds!";

			int minutes = predictions.First().Minutes;
			return $"Your next bus leaves in {minutes} minute{(minutes != 1 ? "s" : "" )}";
		}

		private static string GetOtherBusTimes(ReadOnlyCollection<PredictionDto> predictions)
		{
			return string.Join(", ", predictions.Skip(1).Select(p => p.Minutes));
		}

		private readonly MetroClient.MetroClient m_metroClient;
		private const string c_metroUri = "http://api.metro.net/agencies/lametro/";
		private const string c_routeId = "704";
	}
}