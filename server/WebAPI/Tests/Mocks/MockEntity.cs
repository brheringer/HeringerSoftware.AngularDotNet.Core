using HeringerSoftware.AngularDotNet.Core.Model;
using System;
using System.Collections.Generic;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Mocks
{
	public class MockEntity : Entity
	{
		public virtual string Company { get; set; }
		public virtual bool TheBoolean { get; set; }
		public virtual char TheChar { get; set; }
		public virtual DateTime TheDateTime { get; set; }
		public virtual decimal TheDecimal { get; set; }
		public virtual double TheDouble { get; set; }
		public virtual DayOfWeek TheEnumeration { get; set; }
		public virtual float TheFloat { get; set; }
		public virtual int TheInteger { get; set; }
		public virtual object TheObject { get; set; }
		public virtual short TheShort { get; set; }
		public virtual string TheString { get; set; }
		public virtual MockAnotherEntity TheReference { get; set; }
		public virtual List<int> TheList { get; set; }
		public virtual MockEntity TheNullReference { get; set; }
		public virtual List<MockComposition> TheComposition { get; set; }

		public override void Validate()
		{
		}

		public override string ToString()
		{
			return this.TheString;
		}
	}
}
