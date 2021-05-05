using System;
using System.Collections.Generic;

namespace HeringerSoftware.AngularDotNet.Core.DataTransferObjects
{
	public class EntitiesReferencesDto
		: DataTransferObject
	{
		public List<EntityReferenceDto> References { get; set;  }

		public EntitiesReferencesDto()
		{
			this.References = new List<EntityReferenceDto>();
		}
	}
}
