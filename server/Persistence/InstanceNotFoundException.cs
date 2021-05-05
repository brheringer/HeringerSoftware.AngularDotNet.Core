using System;

namespace HeringerSoftware.AngularDotNet.Core.Persistence
{
	public class InstanceNotFoundException
		: PersistenceException
	{
		public Type Type { get; set; }
		public object Key { get; set; }

		public InstanceNotFoundException(Type type, object chave)
			: base(string.Format("{0} {1}", type, chave))
		{
			this.Type = type;
			this.Key = chave;
		}

	}
}
