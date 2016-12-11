using Metro.Services.Models;

namespace Metro.Models
{
	public static class Mapper
	{
		public static RouteViewModel ToRouteViewModel(Route route)
		{
			return new RouteViewModel
			{
				Id = route.Id,
				Name = route.DisplayName,
				BackgroundColor = route.BackgroundColor,
				ForegroundColor = route.ForegroundColor
			};
		}

		public static StopViewModel ToStopViewModel(Stop stop)
		{
			return new StopViewModel
			{
				Id = stop.Id.ToString(),
				Title = stop.DisplayName
			};
		}
	}
}
