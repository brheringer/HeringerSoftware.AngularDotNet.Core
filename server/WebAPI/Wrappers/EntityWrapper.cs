using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HeringerSoftware.AngularDotNet.Core.DataTransferObjects;
using HeringerSoftware.AngularDotNet.Core.Model;
using HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Wrappers
{
	public class EntityWrapper
	{
		private DbContext DbContext { get; set; }

		public EntityWrapper(DbContext dbContext)
		{
			this.DbContext = dbContext;
		}

		public void WrapCommonFieldsIntoDto(Entity entity, EntityDto dto)
		{
			if (entity == null || dto == null)
				throw new ArgumentNullException();
			dto.Id = entity.Id;
			dto.CreationDateTime = entity.CreationDateTime;
			dto.CreationUser = entity.CreationUser;
			dto.LastUpdateDateTime = entity.LastUpdateDateTime;
			dto.LastUpdateUser = entity.LastUpdateUser;
			dto.Presentation = entity.ToString();
			dto.Version = entity.Version;
		}

		public void WrapCommonFieldsIntoEntity(EntityDto dto, Entity entity)
		{
			if (entity == null || dto == null)
				throw new ArgumentNullException();
			entity.Id = dto.Id;
			entity.CreationDateTime = dto.CreationDateTime;
			entity.CreationUser = dto.CreationUser;
			entity.LastUpdateDateTime = dto.LastUpdateDateTime;
			entity.LastUpdateUser = dto.LastUpdateUser;
			entity.Version = dto.Version;
		}

		public EntitiesReferencesDto WrapToReferences<T>(IEnumerable<T> entities)
			where T : Entity
		{
			EntitiesReferencesDto references = new EntitiesReferencesDto();
			foreach (T e in entities)
			{
				references.References.Add(WrapToReference(e));
			}
			return references;
		}

		public EntityReferenceDto WrapToReference(Entity entity)
		{
			EntityReferenceDto dto = null;
			if (entity != null)
			{
				dto = new EntityReferenceDto();
				dto.Id = entity.Id;
				dto.Presentation = entity.ToString();
				dto.Version = entity.Version;
			}
			return dto;
		}

		public T CreateProxy<T>(EntityReferenceDto dto)
			where T : Entity//, new()
		{
			return CreateProxy<T>(dto, typeof(T));
		}

		private T CreateProxy<T>(EntityReferenceDto dto, Type entityType)
			where T : Entity//, new()
		{
			if (dto == null || dto.Id == 0)
				return null;

			//assim era suficiente para NHibernate, mas não para o EF
			//T entity = new T();
			//SetProxyProperties(entity, dto);
			//return entity;

			T proxy;
			EntityEntry attached = DbContext.ChangeTracker.Entries()
				.FirstOrDefault(e => ((Entity)e.Entity).Id == dto.Id && entityType == e.Metadata.ClrType);
			if (attached == null)
			{
				proxy = (T)DbContext.CreateProxy(entityType, new object[0]);
				//proxy = (T)entityType.GetConstructor(new Type[0]).Invoke(null); //nota: assim parece funcionar tambem
				//tentei set shadow property ao inves de usar proxy, deu certo salvar, mas nao funciona lazy initialization depois
				SetProxyProperties(proxy, dto);
				DbContext.Entry(proxy).State = EntityState.Unchanged;
			}
			else
			{
				proxy = (T)attached.Entity;
			}

			return proxy;
		}

		public void SetProxyProperties(Entity proxy, EntityReferenceDto dto)
		{
			proxy.Id = dto.Id;
			proxy.Version = dto.Version;
		}

		public void CopyInto(Entity fromEntity, EntityDto toDto)
		{
			CopyPropertiesInto(fromEntity, toDto);
		}

		public void CopyInto(EntityDto fromDto, Entity toEntity)
		{
			CopyPropertiesInto(fromDto, toEntity);
		}

		private void CopyPropertiesInto(object from, object to)
		{
			//problemas: coleções
			var fromProperties = from.GetType().GetProperties();
			var toProperties = to.GetType().GetProperties();
			foreach(var p in fromProperties)
			{
				if (p.PropertyType.IsEnum)
				{
					CopyEnumPropertyInto(from, to, toProperties, p);
				}
				else if (IsBasicType(p))
				{
					CopyBasicTypePropertyInto(from, to, toProperties, p);
				}
				else if (IsEntity(p))
				{
					CopyEntityPropertyInto(from, to, toProperties, p);
				}
				else if (IsEntityReferenceDto(p))
				{
					CopyEntityReferenceDtoPropertyInto(from, to, toProperties, p);
				}
				else if (IsGenericCollection(p))
				{
					CopyGenericCollectionPropertyInto(from, to, toProperties, p);
				}
			}
		}

		private void CopyEnumPropertyInto(object from, object to, PropertyInfo[] toProperties, PropertyInfo p)
		{
			System.Reflection.PropertyInfo setting = FindByName(toProperties, p.Name);
			if (setting != null)
				setting.SetValue(to, p.GetValue(from).ToString());
		}

		private bool IsBasicType(PropertyInfo p)
		{
			return p.PropertyType.IsPrimitive || p.PropertyType.IsValueType || p.PropertyType == typeof(string);
		}

		private void CopyBasicTypePropertyInto(object from, object to, PropertyInfo[] toProperties, PropertyInfo p)
		{
			System.Reflection.PropertyInfo setting = FindByName(toProperties, p.Name);
			if (setting != null)
			{
				if (setting.PropertyType.IsEnum)
				{
					string enumString = p.GetValue(from)?.ToString();
					if (!string.IsNullOrEmpty(enumString))
					{
						setting.SetValue(to, Enum.Parse(setting.PropertyType, p.GetValue(from)?.ToString(), true));
					}
					else if (!IsOfNullableType(p.PropertyType))
					{
						var enumerator = Enum.GetValues(setting.PropertyType).GetEnumerator();
						enumerator.MoveNext();
						setting.SetValue(to, enumerator.Current);
					}
				}
				else if (setting.GetSetMethod() != null)
				{
					setting.SetValue(to, p.GetValue(from));
				}
			}
		}

		private bool IsEntity(PropertyInfo p)
		{
			return p.PropertyType == typeof(Entity)
				|| p.PropertyType.IsSubclassOf(typeof(Entity));
		}

		private void CopyEntityPropertyInto(object from, object to, PropertyInfo[] toProperties, PropertyInfo p)
		{
			System.Reflection.PropertyInfo setting = FindByName(toProperties, p.Name);
			if (setting != null && setting.GetSetMethod() != null)
			{
				var referencedEntity = (Entity)p.GetValue(from);
				//bool isProxy = referencedEntity != null && referencedEntity.GetType().Namespace == "Castle.Proxies"; //TODO deve ter um jeito melhor
				//if (isProxy)
				//{
				//	((Microsoft.EntityFrameworkCore.Proxies.Internal.IProxyLazyLoader)referencedEntity).LazyLoader.Load(from, p.Name);
				//}
				setting.SetValue(to, WrapToReference(referencedEntity));
			}
		}

		private bool IsEntityReferenceDto(PropertyInfo p)
		{
			return p.PropertyType == typeof(EntityReferenceDto)
				|| p.PropertyType.IsSubclassOf(typeof(EntityReferenceDto));
		}

		private void CopyEntityReferenceDtoPropertyInto(object from, object to, PropertyInfo[] toProperties, PropertyInfo p)
		{
			PropertyInfo setting = FindByName(toProperties, p.Name);
			if (setting != null && setting.GetSetMethod() != null)
			{
				EntityReferenceDto erdto = (EntityReferenceDto)p.GetValue(from);
				Entity proxy = null;
				if (erdto != null)
				{
					proxy = CreateProxy<Entity>(erdto, setting.PropertyType);
				}
				setting.SetValue(to, proxy);
			}
		}

		private bool IsGenericCollection(PropertyInfo p)
		{
			return p.PropertyType.IsGenericType && (
				typeof(ICollection<>).IsAssignableFrom(p.PropertyType.GetGenericTypeDefinition()) || 
				typeof(IList<>).IsAssignableFrom(p.PropertyType.GetGenericTypeDefinition()) || 
				typeof(List<>).IsAssignableFrom(p.PropertyType.GetGenericTypeDefinition())
			) && (
				p.PropertyType.GetGenericArguments().First().IsSubclassOf(typeof(Entity)) || 
				p.PropertyType.GetGenericArguments().First().IsInstanceOfType(typeof(Entity)) || 
				p.PropertyType.GetGenericArguments().First().IsSubclassOf(typeof(EntityDto)) ||
				p.PropertyType.GetGenericArguments().First().IsInstanceOfType(typeof(EntityDto))
			);
		}

		private void CopyGenericCollectionPropertyInto(object from, object to, PropertyInfo[] toProperties, PropertyInfo p)
		{
			PropertyInfo toProperty = FindByName(toProperties, p.Name);
			if (toProperty != null && toProperty.GetSetMethod() != null)
			{
				object destinationCollection = toProperty.GetGetMethod().Invoke(to, null);
				if (destinationCollection == null)
					destinationCollection = Activator.CreateInstance(toProperty.PropertyType);

				toProperty.SetValue(to, destinationCollection);
				object sourceCollection = p.GetValue(from);
				if (sourceCollection != null)
				{
					Type collectionGenericArgumentType = toProperty.PropertyType.GetGenericArguments().First();
					CopyGenericCollectionPropertyInto(sourceCollection, destinationCollection, collectionGenericArgumentType);
				}
			}
			//else throw new InvalidOperationException($"Property {p.Name} (generic collection at Dto or Entity) should exists with a setter.");
		}

		private void CopyGenericCollectionPropertyInto(object sourceCollection, object destinationCollection, Type collectionGenericArgumentType)
		{
			foreach (object obj in (System.Collections.IEnumerable)sourceCollection)
			{
				object pleaseAddMeToCollection = null;

				EntityDto fromDto = obj as EntityDto;
				if (fromDto != null)
				{
					pleaseAddMeToCollection = CopyGenericCollectionOfDtoIntoGenericCollectionOfEntity(collectionGenericArgumentType, fromDto, destinationCollection);
				}
				else
				{
					Entity fromEntity = obj as Entity;
					if (fromEntity != null)
					{
						pleaseAddMeToCollection = CopyGenericCollectionOfEntityIntoGenericCollectionOfDto(collectionGenericArgumentType, fromEntity);
					}
				}

				if (pleaseAddMeToCollection != null)
				{
					MethodInfo mInfo = destinationCollection.GetType().GetMethods().SingleOrDefault(m => m.Name == nameof(ICollection<object>.Add));
					mInfo.Invoke(destinationCollection, new object[] { pleaseAddMeToCollection });
				}

			}
		}

		private object CopyGenericCollectionOfDtoIntoGenericCollectionOfEntity(Type collectionGenericArgumentType, EntityDto fromDto, object destinationCollection)
		{
			object pleaseAddMeToCollection = null;
			//Por causa do EF Core, tem que sempre manter a instância carregada do banco e não criar uma nova com mesmo autoid
			if (fromDto.Id < 1)
			{
				if (!fromDto.DeleteMe)
				{
					Entity toProxy = (Entity)Activator.CreateInstance(collectionGenericArgumentType);
					CopyPropertiesInto(fromDto, toProxy);
					pleaseAddMeToCollection = toProxy;
				}
			}
			else
			{
				bool found = false;
				foreach (Entity potentialEntity in (System.Collections.IEnumerable)destinationCollection)
				{
					if (potentialEntity.Id == fromDto.Id)
					{
						if (fromDto.DeleteMe)
						{
							RemoveItemFromCollection(destinationCollection, potentialEntity);
						}
						else
						{
							CopyPropertiesInto(fromDto, potentialEntity);
						}
						found = true;
						break;
					}
				}

				if (!found)
					throw new InvalidOperationException("Violação de premissa. Uma composição não deveria ter Id (persistente) e não estar na coleção de composição de sua entidade 'mãe'.");

				pleaseAddMeToCollection = null;
			}

			return pleaseAddMeToCollection;
		}

		private void RemoveItemFromCollection(object collection, Entity item)
		{
			MethodInfo mInfo = collection.GetType().GetMethods().SingleOrDefault(m => m.Name == nameof(ICollection<object>.Remove));
			mInfo.Invoke(collection, new object[] { item });
		}

		private object CopyGenericCollectionOfEntityIntoGenericCollectionOfDto(Type collectionGenericArgumentType, Entity fromEntity)
		{
			object pleaseAddMeToCollection;
			EntityDto toDto = (EntityDto)Activator.CreateInstance(collectionGenericArgumentType);
			CopyPropertiesInto(fromEntity, toDto);
			pleaseAddMeToCollection = toDto;
			return pleaseAddMeToCollection;
		}

		private PropertyInfo FindByName(PropertyInfo[] properties, string name)
		{
			return properties.SingleOrDefault(p => p.Name == name);
		}

		private bool IsOfNullableType(Type type)
		{
			return Nullable.GetUnderlyingType(type) != null;
		}

		public TCollectionDto Wrap<TCollectionDto, TEntityDto, TEntity>(IList<TEntity> entities)
            where TCollectionDto : CollectionDto<TEntityDto>, new()
			where TEntityDto : EntityDto, new()
			where TEntity : Entity
        {
            var dtoCollection = new TCollectionDto();
            foreach (TEntity e in entities)
                dtoCollection.Items.Add(Wrap<TEntityDto>(e));
            return dtoCollection;
        }

        public TEntityDto Wrap<TEntityDto>(Entity entity)
			where TEntityDto : EntityDto, new()
		{
			if (entity == null)
				return null;

			TEntityDto dto = new TEntityDto();
			CopyInto(entity, dto);
			dto.Presentation = entity.ToString();
			return dto;
		}

		public TEntity Wrap<TEntity>(EntityDto dto)
			where TEntity : Entity, new()
		{
			if (dto == null)
				return null;

			TEntity entidade = new TEntity();
			CopyInto(dto, entidade);
			return entidade;
		}

		public void WrapIntoEntity(EntityDto fromDto, Entity intoEntity)
		{
			CopyInto(fromDto, intoEntity);
		}

		public void WrapIntoDto(Entity fromEntity, EntityDto toDto)
		{
			CopyInto(fromEntity, toDto);
			toDto.Presentation = fromEntity.ToString();
		}

		public T? ParseEnum<T>(string enumValue)
			where T : struct, IConvertible
		{
			return string.IsNullOrEmpty(enumValue)
				? (T?)null
				: (T?)Enum.Parse<T>(enumValue);
		}

		public List<TEntity> WrapCollection<TEntity, TEntityDto>(List<TEntityDto> dtoList)
		{
			List<TEntity> entities = new List<TEntity>();
			CopyGenericCollectionPropertyInto(dtoList, entities, typeof(TEntity));
			return entities;
		}

		public List<TEntity> WrapCollection<TEntity, TEntityDto>(List<TEntityDto> dtoList, List<TEntity> entities)
		{
			CopyGenericCollectionPropertyInto(dtoList, entities, typeof(TEntity));
			return entities;
		}
	}
}
