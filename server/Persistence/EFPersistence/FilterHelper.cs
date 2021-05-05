using System;
using System.Collections.Generic;

namespace HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence
{
	public static class FilterHelper
	{
		//public static void ApplyCommonFilters<EntityType>(DbSet<EntityType> dbset, int maxResults, int page)
		//{
		//	if (maxResults > 0)
		//	{
		//		queryOver.Take(maxResults);

		//		if (page > 0)
		//		{
		//			queryOver.Skip(page * maxResults);
		//		}
		//	}
		//}

		public static bool IsFilterUsed(string filter)
		{
			return !string.IsNullOrEmpty(filter);
		}

		public static bool IsFilterUsed(DateTime? filter)
		{
			return filter == null
				? false
				: IsFilterUsed(filter.Value);
		}

		public static bool IsFilterUsed(DateTime filter)
		{
			return filter > DateTime.MinValue;
		}

		public static bool IsFilterUsed(decimal? filter)
		{
			return filter == null
				? false
				: IsFilterUsed(filter.Value);
		}

		public static bool IsFilterUsed(decimal filter)
		{
			return filter != 0;
		}

		public static bool IsFilterUsed(object filter)
		{
			return filter != null;
		}

		public static bool IsFilterUsed<T>(ICollection<T> filter)
		{
			return filter != null && filter.Count > 0;
		}
	}
}
