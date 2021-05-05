using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HeringerSoftware.AngularDotNet.Core.Model;

namespace HeringerSoftware.AngularDotNet.Core.Model.Tests
{
    [TestClass]
    public class EntityHelperTest
    {
        [TestMethod]
        public void TestEqualsByReferenceByType()
        {
            object a = new DateTime(2017, 1, 1);
            object b = new DateTime(2017, 1, 1);
            object c = "asdf";
            object nullReference = null;
            object d_is_a = a;
            Assert.AreEqual(false, EntityHelper.EqualsByReferenceByType(a, nullReference), "Um objeto é nulo e o outro não é nulo. Então, pode-se concluir que não são iguais.");
            Assert.AreEqual(false, EntityHelper.EqualsByReferenceByType(nullReference, a), "Um objeto é nulo e o outro não é nulo. Então, pode-se concluir que não são iguais.");
            Assert.AreEqual(true, EntityHelper.EqualsByReferenceByType(a, d_is_a), "Os dois objetos são iguais por referência.");
            Assert.AreEqual(true, EntityHelper.EqualsByReferenceByType(d_is_a, a), "Os dois objetos são iguais por referência.");
            Assert.AreEqual(false, EntityHelper.EqualsByReferenceByType(a, c), "Objetos de tipos diferentes não são considerados iguais.");
            Assert.AreEqual(false, EntityHelper.EqualsByReferenceByType(c, a), "Objetos de tipos diferentes não são considerados iguais.");
            Assert.IsNull(EntityHelper.EqualsByReferenceByType(a, b), "As referências não são nulas e não são iguais, então não é possível concluir a equalidade.");
            Assert.IsNull(EntityHelper.EqualsByReferenceByType(b, a), "As referências não são nulas e não são iguais, então não é possível concluir a equalidade.");
        }

		[TestMethod]
		public void TestEqualsByReferenceByTypeSimulatingNHProxies()
		{
			MockEntity entity = new MockEntity();
			MockEntity proxy = new MockEntityAsNhProxy();
			Assert.IsNull(EntityHelper.EqualsByReferenceByType(entity, proxy));
			Assert.IsNull(EntityHelper.EqualsByReferenceByType(proxy, entity));
		}

		private class MockEntity : Entity
		{
			public override void Validate()
			{
			}
		}

		private class MockEntityAsNhProxy : MockEntity
		{
			public override Type GetTypeUnproxied()
			{
				return typeof(MockEntity);
				//não sei como o NH faz, mas assim é suficiente para o teste unitário
			}
		}
	}
}
