using System;

namespace HeringerSoftware.AngularDotNet.Core.Persistence
{
	[Serializable]
	public class TooManyInstancesFoundException 
		: PersistenceException
	{
		public Type Type { get; set; }
		public object Key { get; set; }

		public TooManyInstancesFoundException(Type type, object key)
			: base(string.Format("{0} {1}", type, key)) //TODO rever
		{
			this.Type = type;
			this.Key = key;
		}
	}
}
