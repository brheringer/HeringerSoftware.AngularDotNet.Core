using HeringerSoftware.AngularDotNet.Core.Model;
using System;
using System.Collections.Generic;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Mocks
{
	public class MockComposition : Entity
	{
		public virtual string TheString { get; set; }
		public virtual MockEntity TheContainerEntity { get; set; }
		public virtual MockAnotherEntity TheReference { get; set; }

		public override void Validate()
		{
		}

		public override string ToString()
		{
			return this.TheString;
		}
	}
}
