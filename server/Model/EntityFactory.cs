using System;

namespace HeringerSoftware.AngularDotNet.Core.Model
{
	public class EntityFactory<T> where T : new()
	{
		private Func<T> Model { get; set; }

		public EntityFactory(Func<T> model)
		{
			this.Model = model;
		}

		public T Create()
		{
			return Create(null);
		}

		public T Create(Action<T> modifier)
		{
			T newInstance = Model();
			if (modifier != null)
				modifier(newInstance);
			return newInstance;
		}
	}
}
