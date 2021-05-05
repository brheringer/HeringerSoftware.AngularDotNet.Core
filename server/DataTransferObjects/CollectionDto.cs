using System;
using System.Collections.Generic;

namespace HeringerSoftware.AngularDotNet.Core.DataTransferObjects
{
	public class CollectionDto<T>
		: DataTransferObject
	{
		public int SearchMaxResults { get; set; }
		public int SearchPage { get; set; }
        public List<T> Items { get; set; } 

		public CollectionDto()
			: base()
		{
			this.SearchMaxResults = 25;
			this.SearchPage = 0;
            this.Items = new List<T>();
		}
	}
}
