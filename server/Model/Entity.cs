using System;
using System.Linq.Expressions;

namespace HeringerSoftware.AngularDotNet.Core.Model
{
    public abstract class Entity
    {
        public virtual int Id { get; set; }
        public virtual int Version { get; set; } //TODO talvez passar para timespan por causa do telos
		public virtual DateTime? CreationDateTime { get; set; }
		public virtual string CreationUser { get; set; }
		public virtual string LastUpdateUser { get; set; }
		public virtual DateTime? LastUpdateDateTime { get; set; }

		public virtual bool IsPersistent
        {
            get
            {
                return this.Id > 0;
            }
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
            //TODO id?
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
            //TODO id?
        }

		public virtual Type GetTypeUnproxied()
		{
			//EF Core
			var thisType = this.GetType();
			if (thisType.Namespace == "Castle.Proxies")
				return thisType.BaseType;
			//https://stackoverflow.com/questions/25770369/get-underlying-entity-object-from-entity-framework-proxy/36899262

			//NHibnernate
			return GetType();
			//https://stackoverflow.com/questions/3755841/how-do-i-get-the-entity-type-on-an-object-that-may-be-a-nhibernate-proxy-object
			//Resolve falha em EntityHelper.EqualsByReferenceByType quando um dos objetos é um proxy do NHibernate
			//Infelizmente, é uma implementação para atender a persistência, o que mistura as camadas,
			//mas se trocarmos o NH por outra lib, esse método não vai atrapalhar, simplesmente não terá efeito.
		}

		public abstract void Validate();

		/// <summary>
		/// Método auxiliar para reuso.
		/// É um crivo de validação de propriedade do tipo string
		/// que é obrigatória e tem um valor máximo.
		/// </summary>
		/// <param name="entityName"></param>
		/// <param name="property"></param>
		/// <param name="value"></param>
		/// <param name="maxSize"></param>
		public virtual void ValidateRequiredProperty(Expression<Func<string>> propertyExpression, int maxSize = 0)
        {
            //TODO o nome do método não está comunicando que vai verificar o tamanho do texto
            Func<string> getter = propertyExpression.Compile();
            string text = getter();
            ValidateRequiredProperty(
                EntityHelper.GetClassName(propertyExpression),
                EntityHelper.GetPropertyName(propertyExpression),
                text,
                maxSize);
        }

        private void ValidateRequiredProperty(string entityName, string property, string value, int maxSize = 0)
        {
            if (string.IsNullOrEmpty(value))
                throw new RequiredPropertyException(entityName, property);

            if (maxSize > 0)
                ValidatePropertyValue(entityName, property, value, maxSize);
        }

		/// <summary>
		/// Método auxiliar para reuso.
		/// É um crivo de validação de propriedade do tipo Entidade
		/// que é obrigatória.
		/// </summary>
		/// <param name="entityName"></param>
		/// <param name="property"></param>
		/// <param name="value"></param>
		public virtual void ValidateRequiredProperty(Expression<Func<Entity>> propertyExpression)
        {
            Func<Entity> getter = propertyExpression.Compile();
            Entity e = getter();
            ValidateRequiredProperty(
                EntityHelper.GetClassName(propertyExpression),
                EntityHelper.GetPropertyName(propertyExpression),
                e);
        }

        private void ValidateRequiredProperty(string entityName, string property, Entity value)
        {
            if (value == null)
                throw new RequiredPropertyException(entityName, property);
        }

		public virtual void ValidateRequiredProperty(Expression<Func<DateTime>> propertyExpression)
        {
            Func<DateTime> getter = propertyExpression.Compile();
            DateTime date = getter();
            ValidateRequiredProperty(
                EntityHelper.GetClassName(propertyExpression),
                EntityHelper.GetPropertyName(propertyExpression),
                date);
        }

        private void ValidateRequiredProperty(string entityName, string property, DateTime date)
        {
            if (date == DateTime.MinValue)
                throw new RequiredPropertyException(entityName, property);
        }

		public virtual void ValidatePropertyValue<T>(Expression<Func<T>> propertyExpression, T minimum, T maximum)
            where T : IComparable
        {
            Func<T> getter = propertyExpression.Compile();
            T valor = getter();
            ValidatePropertyValue(
                EntityHelper.GetClassName(propertyExpression),
                EntityHelper.GetPropertyName(propertyExpression),
                valor, minimum, maximum);
        }

        private void ValidatePropertyValue<T>(string entityName, string property, T value, T minimum, T maximum)
            where T : IComparable
        {
            if (value.CompareTo(minimum) < 0 || value.CompareTo(maximum) > 0)
                throw new InvalidPropertyValueException(
                    entityName,
                    property,
                    value.ToString(),
                    string.Format("[{0}, {1}]", minimum, maximum)); //TODO
        }

		public virtual void ValidatePropertyValue(Expression<Func<string>> propertyExpression, int maxSize)
        {
            Func<string> getter = propertyExpression.Compile();
            string text = getter();
            ValidatePropertyValue(
                EntityHelper.GetClassName(propertyExpression),
                EntityHelper.GetPropertyName(propertyExpression),
                text,
                maxSize);
        }

        private void ValidatePropertyValue(string entityName, string property, string value, int maxSize)
        {
            if (value != null && value.Length > maxSize)
                throw new InvalidPropertyValueException(entityName, property, value.Length.ToString(), "Tamanho máximo permitido: " + maxSize.ToString());
        }

		/// <summary>
		/// Shallow copy (see Object.MemberwiseClone).
		/// </summary>
		/// <returns></returns>
		public virtual Entity Clone()
		{
			return (Entity)this.MemberwiseClone();
		}
    }

}
