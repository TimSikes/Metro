using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using Metro.Models;
using Metro.Services;
using Metro.Services.Models;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;

namespace Metro.Controllers
{
	public class HomeController : Controller
	{
		public HomeController()
		{
		}

		public ActionResult Index()
		{
			ReadOnlyCollection<StopViewModel> stops = MetroService.GetStops(s_routeId).Select(Mapper.ToStopViewModel).ToSafeReadOnlyCollection();
			Route route = MetroService.GetRoute(s_routeId);

			IndexViewModel indexViewModel = new IndexViewModel
			{
				Route = Mapper.ToRouteViewModel(route),
				DepartureAndArrival = GetDeparturesAndArrivals(stops)
			};

			return View(indexViewModel);
		}

		public ActionResult Route(string departure, string arrival)
		{
			//TODO: Optimization: Some of this data is grabbed on each page load, and could be better passed around to avoid extra calls to the external service.
			ReadOnlyCollection<Stop> stops = MetroService.GetStops(s_routeId);
			DepartureAndArrivalViewModel departureAndArrivalViewModel = GetDeparturesAndArrivals(stops.Select(Mapper.ToStopViewModel).ToSafeReadOnlyCollection());

			if (departure.IsNullOrWhiteSpace() || arrival.IsNullOrWhiteSpace())
			{
				return View(new JourneyInformationViewModel
				{
					Error = new ErrorViewModel
					{
						Message = "Please select a valid arrival and destination"
					},
					DepartureAndArrival = departureAndArrivalViewModel
				});
			}

			ReadOnlyCollection<Prediction> predictions = MetroService.GetPredictions(s_routeId, departure);
			TravelInformation travelInformation = MetroService.GetTravelInformationDto(s_routeId, departure, arrival);
			Route route = MetroService.GetRoute(s_routeId);

			string departureTitle = stops.First(s => s.Id.ToString() == departure).DisplayName;
			string arrivaTitle = stops.First(s => s.Id.ToString() == arrival).DisplayName;

			JourneyInformationViewModel journeyInfo = new JourneyInformationViewModel
			{
				NextBusMessage = GetNextBusDepartureMessage(predictions.First()),
				OtherBusTimes = GetOtherBusTimes(predictions),
				Message = new WarningViewModel
				{
					Message = travelInformation.Message
				},
				TravelTimeMinutes = travelInformation.TravelDurationMinutes,
				DepartureTitle = departureTitle,
				ArrivalTitle = arrivaTitle,
				DepartureAndArrival = departureAndArrivalViewModel,
				Route = Mapper.ToRouteViewModel(route),
			};

			return View(journeyInfo);
		}

		private static DepartureAndArrivalViewModel GetDeparturesAndArrivals(ReadOnlyCollection<StopViewModel> stops)
		{
			return new DepartureAndArrivalViewModel
			{
				Arrival = new StopSelectorViewModel
				{
					Stops = stops,
					SelectorName = "arrival"
				},
				Departure = new StopSelectorViewModel
				{
					Stops = stops,
					SelectorName = "departure"
				}
			};
		}

		//TODO: Handle some of this view logic in a different area.
		private static string GetNextBusDepartureMessage(Prediction prediction)
		{
			if (!prediction.Minutes.HasValue && !prediction.Seconds.HasValue)
				return "The next bus time is unknown";
			if (prediction.Minutes.HasValue && prediction.Minutes < 1)
				return $"Hurry! Your next bus leaves in {prediction.Seconds} seconds!";

			int? minutes = prediction.Minutes;
			return $"Your next bus leaves in {minutes} minute{(minutes != 1 ? "s" : "" )}";
		}

		private static string GetOtherBusTimes(ReadOnlyCollection<Prediction> predictions)
		{
			//TODO: Include "and" in the list
			//TODO: Handle the case where there are no more busses coming.
			return string.Join(", ", predictions.Skip(1).Select(p => p.Minutes));
		}

		private static readonly string s_routeId = Properties.Settings.Default.RouteId;
	}
}
