using System.Collections.ObjectModel;

namespace Metro.Models
{
	public sealed class StopSelectorViewModel
	{
		public ReadOnlyCollection<StopViewModel> Stops { get; set; }
		public string SelectorName { get; set; }
	}
}