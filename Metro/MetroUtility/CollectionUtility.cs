using System.Collections.Generic;

namespace MetroUtility
{
	public static class CollectionUtility
	{
		public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> collection)
		{
			return collection ?? new List<T>();
		}
	}
}
