using System;
using System.Collections.Generic;
using HeringerSoftware.AngularDotNet.Core.Model;

namespace HeringerSoftware.AngularDotNet.Core.Persistence
{
	public interface Repository<EntityType> : AgnosticRepository
		where EntityType : Entity
	{
		EntityType CreateProxy();

		void Delete(EntityType entity);

		EntityType Load(int id);
		
		EntityType LoadWithCompositions(int id);

		IList<EntityType> LoadAll();

		EntityType Update(EntityType entity);
	}
}
