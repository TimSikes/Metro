using Metro.Services.Models;

namespace Metro.Models
{
	public static class Mapper
	{
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
