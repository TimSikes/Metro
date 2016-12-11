using System;
using System.Linq;
using MetroClient.Models;
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

		public StopsDto GetStops(string routeId)
		{
			return ToStopsDto(GetJson($"routes/{routeId}/stops"));
		}

		public VehiclesDtos GetVehicles(string routeId)
		{
			return ToVehichlesDto(GetJson($"routes/{routeId}/vehicles"));
		}

		public PredictionsDto GetPredictions(string stopId)
		{
			return ToPredictionsDto(GetJson($"stops/{stopId}/predictions/"));
		}

		private static RouteDto ToRouteDto(JObject route)
		{
			return new RouteDto
			{
				BackgroundColor = route["bg_color"].ToString(),
				DisplayName = route["display_name"].ToString(),
				ForegroundColor = route["fg_color"].ToString(),
				Id = int.Parse(route["id"].ToString())
			};
		}

		private static StopDto ToStopDto(JToken stop)
		{
			return new StopDto
			{
				Id = stop["id"] != null
					? int.Parse(stop["id"].ToString())
					: (int?) null,
				DisplayName = stop["display_name"].ToString(),
				Latitude = double.Parse(stop["latitude"].ToString()),
				Longitude = double.Parse(stop["longitude"].ToString())
			};
		}

		private static VehicleDto ToVehicleDto(JToken vehicle)
		{
			return new VehicleDto
			{
				Id = int.Parse(vehicle["id"].ToString()),
				Heading = int.Parse(vehicle["heading"].ToString()),
				RunId = vehicle["run_id"].ToString(),
				Predictable = ToBool(vehicle["predictable"].ToString()),
				RouteId =  vehicle["route_id"] != null
					? int.Parse(vehicle["route_id"].ToString())
					: (int?) null,
				SecondsSinceReport = int.Parse(vehicle["seconds_since_report"].ToString()),
				Latitude = double.Parse(vehicle["latitude"].ToString()),
				Longitude = double.Parse(vehicle["longitude"].ToString())
			};
		}

		private static PredictionDto ToPredictionDto(JToken prediction)
		{
			return new PredictionDto
			{
				BlockId = prediction["block_id"].ToString(),
				RunId = prediction["run_id"].ToString(),
				RouteId = int.Parse(prediction["route_id"].ToString()),
				IsDeparting = ToBool(prediction["is_departing"].ToString()),
				Minutes = int.Parse(prediction["minutes"].ToString()),
				Seconds = int.Parse(prediction["seconds"].ToString())
			};
		}

		private static StopsDto ToStopsDto(JObject stops)
		{
			return new StopsDto
			{
				Stops = stops["items"].Select(ToStopDto)
			};
		}

		private static VehiclesDtos ToVehichlesDto(JObject vehicles)
		{
			return new VehiclesDtos
			{
				Vehicles = vehicles["items"].Select(ToVehicleDto)
			};
		}

		private static PredictionsDto ToPredictionsDto(JObject predictions)
		{
			return new PredictionsDto
			{
				Predictions = predictions["items"].Select(ToPredictionDto)
			};
		}

		private static bool ToBool(string value)
		{
			return value == "true";
		}
	}
}
