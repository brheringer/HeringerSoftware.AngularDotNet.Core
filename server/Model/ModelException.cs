using System;

namespace HeringerSoftware.AngularDotNet.Core.Model
{
	public class ModelException : Exception
	{
		public ModelException() { }
		public ModelException(string message) : base(message) { }
		public ModelException(string message, Exception inner) : base(message, inner) { }
	}
}
