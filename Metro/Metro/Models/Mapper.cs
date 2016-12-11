using MetroClient.Models;

namespace Metro.Models
{
	public static class Mapper
	{
		public static StopViewModel ToStopViewModel(StopDto stopDto)
		{
			return new StopViewModel
			{
				Id = stopDto.Id.ToString(),
				Title = stopDto.DisplayName
			};
		}
	}
}
