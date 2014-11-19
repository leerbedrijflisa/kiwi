using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lisa.Kiwi.Web.Dashboard.Utils
{
	public static class SortHelper
	{
		public static string SortBy(string field, string currentSortBy)
		{
			// No current sort by, so default
			if (currentSortBy == null)
			{
				return field + " DESC";
			}

			var sorting = currentSortBy.Split(' ');

			// Invalid amount of spaces, so default
			if (sorting.Length < 1 || sorting.Length > 2)
			{
				return field + " DESC";
			}

			var currentField = sorting[0];

			// Not already this field or lacking DESC, so default
			if (field != currentField || sorting.Length == 1)
			{
				return field + " DESC";
			}

			// Already this field and with DESC, so flip
			return field;
		}
	}
}