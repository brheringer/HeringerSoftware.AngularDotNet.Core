using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeringerSoftware.AngularDotNet.Core.Model.Tests
{
	public class MockedEntity : Entity
	{
		public string Name { get; set; }
		public DateTime Date { get; set; }
		public int WholeNumber { get; set; }
		public decimal RationalNumber { get; set; }
		public Entity OtherInstance { get; set; }

		public override void Validate()
		{
			throw new NotImplementedException();
		}
	}
}
