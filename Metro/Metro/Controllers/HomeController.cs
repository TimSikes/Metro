﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Mvc;
using Metro.Models;
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
			MetroClient.MetroClient client = new MetroClient.MetroClient(new Uri(c_metroUri));

			ReadOnlyCollection<StopDto> stops = client.GetStops(c_routeId).Stops.ToSafeReadOnlyCollection();
			ViewBag.Stops = stops.Select(Mapper.ToStopViewModel);

			return View();
		}

		public ActionResult Route(string departure, string arrival)
		{
			MetroClient.MetroClient client = new MetroClient.MetroClient(new Uri(c_metroUri));

			ReadOnlyCollection<PredictionDto> predictions = client.GetPredictions(c_routeId, departure).Predictions.ToSafeReadOnlyCollection();
			TravelInformationDto travelInformation = client.GetTravelInformationDto(c_routeId, departure, arrival);
			string departureTitle = client.GetStop(departure).DisplayName;
			string arrivaTitle = client.GetStop(arrival).DisplayName;

			ViewBag.NextBusMessage = GetNextBusDepartureMessage(predictions);
			ViewBag.OtherBusTimes = GetOtherBusTimes(predictions);
			ViewBag.Message = travelInformation.Message;
			ViewBag.TravelTime = $"Your trip will take you {travelInformation.TravelDurationMinutes} minutes!";
			ViewBag.DepartureTitle = departureTitle;
			ViewBag.ArrivalTitle = arrivaTitle;

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