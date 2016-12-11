using System;
using System.Linq;
using MetroClient.Models;
using MetroUtility;
using Newtonsoft.Json.Linq;

namespace MetroClient
{
	public sealed class MetroClient : WebServiceClient
	{
		public MetroClient(Uri baseUri)
			: base(baseUri)
		{
		}

		public RouteDto GetRoute(string id)
		{
			return ToRouteDto(GetJson($"routes/{id}"));
		}

		public StopDto GetStop(string stopId)
		{
			return ToStopDto(GetJson($"stops/{stopId}"));
		}

		public StopsDto GetStops(string routeId)
		{
			return ToStopsDto(GetJson($"routes/{routeId}/stops"));
		}


		// TODO: Remove unused code.
		// (The scope of the project made it look like this information would be useful, but the API doesn't offer very meaningful information for this Vehicles object.
		public VehiclesDtos GetVehicles(string routeId)
		{
			return ToVehichlesDto(GetJson($"routes/{routeId}/vehicles"));
		}

		public PredictionsDto GetPredictions(string routeId, string stopId)
		{
			return ToPredictionsDto(GetJson($"routes/{routeId}/stops/{stopId}/predictions/"));
		}

		public TravelInformationDto GetTravelInformationDto(string routeId, string departureStopId, string arrivalStopId)
		{
			// I can't find a working example of a messasge object, and their trip making seems defunct so, I just made this up.
			return new TravelInformationDto
			{
				Message = "Looks like the bus driver is having a bad day today...",
				TravelDurationMinutes = 30
			};
		}

		private static RouteDto ToRouteDto(JObject route)
		{
			return new RouteDto
			{
				BackgroundColor = ConversionUtility.GetValueOrNull(route["bg_color"]),
				DisplayName = ConversionUtility.GetValueOrNull(route["display_name"]),
				ForegroundColor = ConversionUtility.GetValueOrNull(route["fg_color"]),
				Id = ConversionUtility.GetValueOrNull(route["id"], ConversionUtility.ParseInt),
			};
		}

		private static StopDto ToStopDto(JToken stop)
		{
			// Some of these values I would expect to be required values. Better documentation from the API would allow me to know which of these fields are required, and which are optional.  If I owned the API, I would let exceptions fly whenever a field is missing that is expected.
			return new StopDto
			{
				Id = ConversionUtility.GetValueOrNull(stop["id"], ConversionUtility.ParseInt),
				DisplayName = ConversionUtility.GetValueOrNull(stop["display_name"]),
				Latitude = ConversionUtility.GetValueOrNull(stop["latitude"], ConversionUtility.ParseDouble),
				Longitude = ConversionUtility.GetValueOrNull(stop["longitude"], ConversionUtility.ParseDouble),
			};
		}

		private static VehicleDto ToVehicleDto(JToken vehicle)
		{
			return new VehicleDto
			{
				Id = ConversionUtility.GetValueOrNull(vehicle["id"], ConversionUtility.ParseInt),
				Heading = ConversionUtility.GetValueOrNull(vehicle["heading"], ConversionUtility.ParseInt),
				RunId = ConversionUtility.GetValueOrNull(vehicle["run_id"]),
				Predictable = ConversionUtility.GetValueOrNull(vehicle["predictable"], ConversionUtility.ToBool),
				RouteId =  ConversionUtility.GetValueOrNull(vehicle["route_id"], ConversionUtility.ParseInt),
				SecondsSinceReport = ConversionUtility.GetValueOrNull(vehicle["seconds_since_report"], ConversionUtility.ParseInt),
				Latitude = ConversionUtility.GetValueOrNull(vehicle["latitude"], ConversionUtility.ParseDouble),
				Longitude = ConversionUtility.GetValueOrNull(vehicle["longitude"], ConversionUtility.ParseDouble),
			};
		}

		private static PredictionDto ToPredictionDto(JToken prediction)
		{
			return new PredictionDto
			{
				BlockId = ConversionUtility.GetValueOrNull(prediction["block_id"]),
				RunId = ConversionUtility.GetValueOrNull(prediction["run_id"]),
				RouteId = ConversionUtility.GetValueOrNull(prediction["route_id"], ConversionUtility.ParseInt),
				IsDeparting = ConversionUtility.GetValueOrNull(prediction["is_departing"], ConversionUtility.ToBool),
				Minutes = ConversionUtility.GetValueOrNull(prediction["minutes"], ConversionUtility.ParseInt),
				Seconds = ConversionUtility.GetValueOrNull(prediction["seconds"], ConversionUtility.ParseInt),
			};
		}

		private static StopsDto ToStopsDto(JObject stops)
		{
			return new StopsDto
			{
				Stops = stops["items"].Select(ToStopDto).EmptyIfNull()
			};
		}

		private static VehiclesDtos ToVehichlesDto(JObject vehicles)
		{
			return new VehiclesDtos
			{
				Vehicles = vehicles["items"].Select(ToVehicleDto).EmptyIfNull()
			};
		}

		private static PredictionsDto ToPredictionsDto(JObject predictions)
		{
			return new PredictionsDto
			{
				Predictions = predictions["items"].Select(ToPredictionDto).EmptyIfNull()
			};
		}
	}
}
