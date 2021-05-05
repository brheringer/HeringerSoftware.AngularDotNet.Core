using System;
using System.Collections.Generic;
using HeringerSoftware.AngularDotNet.Core.Model;

namespace HeringerSoftware.AngularDotNet.Core.Persistence
{
	public interface AgnosticRepository
	{
		int SearchLimit { get; set; }
		int SearchPage { get; set; }

		IList<Entity> SmartSearch(string smartEntry, string contextFilter, int max);

	}
}
