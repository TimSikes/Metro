using System;
using System.Collections.ObjectModel;
using System.Linq;
using Metro.Services.Models;
using MetroClient.Models;
using WebGrease.Css.Extensions;

namespace Metro.Services
{
	public static class MetroService
	{
		public static ReadOnlyCollection<Stop> GetStops(string routeId)
		{
			return s_metroClient.GetStops(routeId).Stops.Select(ToStop).ToSafeReadOnlyCollection();
		}

		public static Stop GetStop(string stopId)
		{
			return ToStop(s_metroClient.GetStop(stopId));
		}

		public static Prediction GetNextPrediction(string routeId, string stopId)
		{
			return GetPredictions(routeId, stopId).First();
		}

		public static ReadOnlyCollection<Prediction> GetPredictions(string routeId, string stopId)
		{
			return s_metroClient.GetPredictions(routeId, stopId).Predictions.Select(ToPrediction).ToSafeReadOnlyCollection();
		}

		public static TravelInformation GetTravelInformationDto(string routeId, string departureStopId, string arrivalStopId)
		{
			return ToTravelInformation(s_metroClient.GetTravelInformationDto(routeId, departureStopId, arrivalStopId));
		}

		private static Stop ToStop(StopDto stop)
		{
			return new Stop
			{
				DisplayName = stop.DisplayName,
				Id = stop.Id
			};
		}

		private static Prediction ToPrediction(PredictionDto prediction)
		{
			return new Prediction
			{
				Minutes = prediction.Minutes,
				Seconds = prediction.Seconds
			};
		}

		private static TravelInformation ToTravelInformation(TravelInformationDto travelInformation)
		{
			return new TravelInformation
			{
				Message = travelInformation.Message,
				TravelDurationMinutes = travelInformation.TravelDurationMinutes
			};
		}

		private static readonly MetroClient.MetroClient s_metroClient = new MetroClient.MetroClient(new Uri(Properties.Settings.Default.MetroUri));
	}
}
