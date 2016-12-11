using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using Metro.Models;
using Metro.Services;
using Metro.Services.Models;

namespace Metro.Controllers
{
	public class HomeController : Controller
	{
		public HomeController()
		{
		}

		public ActionResult Index()
		{
			ReadOnlyCollection<Stop> stops = MetroService.GetStops(s_routeId);
			ViewBag.Stops = stops.Select(Mapper.ToStopViewModel);

			return View();
		}

		public ActionResult Route(string departure, string arrival)
		{
			ReadOnlyCollection<Prediction> predictions = MetroService.GetPredictions(s_routeId, departure);
			TravelInformation travelInformation = MetroService.GetTravelInformationDto(s_routeId, departure, arrival);
			string departureTitle =  MetroService.GetStop(departure).DisplayName;
			string arrivaTitle = MetroService.GetStop(arrival).DisplayName;

			ViewBag.NextBusMessage = GetNextBusDepartureMessage(predictions.First());
			ViewBag.OtherBusTimes = GetOtherBusTimes(predictions);
			ViewBag.Message = travelInformation.Message;
			ViewBag.TravelTime = $"Your trip will take you {travelInformation.TravelDurationMinutes} minutes!";
			ViewBag.DepartureTitle = departureTitle;
			ViewBag.ArrivalTitle = arrivaTitle;

			return View();
		}

		private static string GetNextBusDepartureMessage(Prediction prediction)
		{
			if (prediction.Minutes < 1)
				return $"Hurry! Your next bus leaves in {prediction.Seconds} seconds!";

			int minutes = prediction.Minutes;
			return $"Your next bus leaves in {minutes} minute{(minutes != 1 ? "s" : "" )}";
		}

		private static string GetOtherBusTimes(ReadOnlyCollection<Prediction> predictions)
		{
			return string.Join(", ", predictions.Skip(1).Select(p => p.Minutes));
		}

		private static readonly string s_routeId = Properties.Settings.Default.RouteId;
	}
}
