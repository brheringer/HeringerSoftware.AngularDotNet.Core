using HeringerSoftware.AngularDotNet.Core.DataTransferObjects;
using System;
using System.Collections.Generic;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Mocks
{
	public class MockCompositionDto : EntityDto
	{
		public string TheString { get; set; }
		public EntityReferenceDto TheReference { get; set; }
	}
}
