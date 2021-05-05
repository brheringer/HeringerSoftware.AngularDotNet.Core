using HeringerSoftware.AngularDotNet.Core.DataTransferObjects;
using System;
using System.Collections.Generic;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Mocks
{
	public class MockDto : EntityDto
	{
		public string Company { get; set; }
		public bool TheBoolean { get; set; }
		public char TheChar { get; set; }
		public DateTime TheDateTime { get; set; }
		public decimal TheDecimal { get; set; }
		public double TheDouble { get; set; }
		public string TheEnumeration { get; set; }
		public float TheFloat { get; set; }
		public int TheInteger { get; set; }
		public object TheObject { get; set; }
		public short TheShort { get; set; }
		public string TheString { get; set; }
		public EntityReferenceDto TheReference { get; set; }
		public List<int> TheList { get; set; }
		public EntityReferenceDto TheNullReference { get; set; }
		public virtual List<MockCompositionDto> TheComposition { get; set; } = new List<MockCompositionDto>();

	}
}
