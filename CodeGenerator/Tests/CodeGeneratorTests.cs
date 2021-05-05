using Microsoft.VisualStudio.TestTools.UnitTesting;
using MetalSoft.Core.CodeGenerator.CodeGenerators;
using MetalSoft.Core.CodeGenerator.CodeGenerators.Angular;
using MetalSoft.Core.CodeGenerator.Model;

namespace UnitTestProject1
{
    [TestClass]
    public class CodeGeneratorTests
	{
		[TestMethod]
		public void TestModelCodeGenerator()
		{
			var entity = BuildEntityCategoriaRisco();
			var g = new ModelCodeGenerator() { BaseEntity = entity, Template = GetTemplate("Entity.template") };

			var code = g.GenerateCode();
			var lines = GetLines(code);

			int i = 0;
			Assert.AreEqual("using MetalSoft.Core.Model;", lines[i++], "Linha " + i);
			Assert.AreEqual("using System;", lines[i++], "Linha " + i);
			Assert.AreEqual("using System.Collections.Generic;", lines[i++], "Linha " + i);
			Assert.AreEqual(string.Empty, lines[i++], "Linha " + i);
			Assert.AreEqual("namespace MetalSoft.Riscos.Model", lines[i++], "Linha " + i);
			Assert.AreEqual("{", lines[i++], "Linha " + i);
			Assert.AreEqual("	public class CategoriaRisco", lines[i++], "Linha " + i);
			Assert.AreEqual("		: MetalSoft.Core.Model.Entity", lines[i++], "Linha " + i);
			Assert.AreEqual("	{", lines[i++], "Linha " + i);
			Assert.AreEqual("		public virtual string Codigo { get; set; }", lines[i++], "Linha " + i);
			Assert.AreEqual("		public virtual string Descricao { get; set; }", lines[i++], "Linha " + i);
			Assert.AreEqual("		public virtual PeriodoRisco Periodo { get; set; }", lines[i++], "Linha " + i);
			Assert.AreEqual("		public virtual Tipo Tipo { get; set; }", lines[i++], "Linha " + i);
			Assert.AreEqual("		public virtual IList<ComposicaoCategoria> Composicoes { get; set; }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		public override void Validate()", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("			ValidateCodigo();", lines[i++], "Linha " + i);
			Assert.AreEqual("			ValidateDescricao();", lines[i++], "Linha " + i);
			Assert.AreEqual("			ValidatePeriodo();", lines[i++], "Linha " + i);
			Assert.AreEqual("			ValidateTipo();", lines[i++], "Linha " + i);
			Assert.AreEqual("			ValidateComposicoes();", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		public virtual void ValidateCodigo()", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("			ValidateRequiredProperty(() => this.Codigo);", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		public virtual void ValidateDescricao()", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("			ValidateRequiredProperty(() => this.Descricao);", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		public virtual void ValidatePeriodo()", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("			ValidateRequiredProperty(() => this.Periodo);", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		public virtual void ValidateTipo()", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("			ValidateRequiredProperty(() => this.Tipo);", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		public virtual void ValidateComposicoes()", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		public override bool Equals(object obj)", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("			return EntityHelper.EqualsByReferenceByType(obj, this) ?? string.Equals(this.Codigo, ((CategoriaRisco)obj).Codigo);", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		public override int GetHashCode()", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("			return EntityHelper.GetHashCode(Codigo);", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		public override string ToString()", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("			return base.ToString();", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("	}", lines[i++], "Linha " + i);
			Assert.AreEqual("}", lines[i++], "Linha " + i);
		}

		[TestMethod]
		public void TestEntityDaoCodeGenerator()
		{
			var entity = BuildEntityCategoriaRisco();
			entity.Properties.Add(new Property(entity) { Name = "Data", Type = "DateTime" });
			var g = new EntityDaoCodeGenerator() { BaseEntity = entity, Template = GetTemplate("EntityDAO.template") };

			var code = g.GenerateCode();
			var lines = GetLines(code);

			int i = 0;
			Assert.AreEqual("using MetalSoft.Core.Persistence;", lines[i++], "Linha " + i);
			Assert.AreEqual("using MetalSoft.Riscos.Model;", lines[i++], "Linha " + i);
			Assert.AreEqual("using System;", lines[i++], "Linha " + i);
			Assert.AreEqual("using System.Collections.Generic;", lines[i++], "Linha " + i);
			Assert.AreEqual(string.Empty, lines[i++], "Linha " + i);
			Assert.AreEqual("namespace MetalSoft.Riscos.Persistence", lines[i++], "Linha " + i);
			Assert.AreEqual("{", lines[i++], "Linha " + i);
			Assert.AreEqual("	public interface CategoriaRiscoDAO", lines[i++], "Linha " + i);
			Assert.AreEqual("		: DAO<CategoriaRisco>", lines[i++], "Linha " + i);
			Assert.AreEqual("	{", lines[i++], "Linha " + i);
			Assert.AreEqual("		IList<CategoriaRisco> Search(string filterCodigo, PeriodoRisco filterPeriodo, Tipo? filterTipo, DateTime? filterData);", lines[i++], "Linha " + i);
			Assert.AreEqual("	}", lines[i++], "Linha " + i);
			Assert.AreEqual("}", lines[i++], "Linha " + i);
		}

		[TestMethod]
		public void TestApplicationDaoFactoryCodeGenerator()
		{
			var entity = BuildEntityCategoriaRisco();
			var g = new ApplicationDaoFactoryCodeGenerator() { BaseEntity = entity, Template = GetTemplate("ApplicationDAOFactory.template") };

			var code = g.GenerateCode();
			var lines = GetLines(code);

			int i = 0;
			Assert.AreEqual("CategoriaRiscoDAO CategoriaRiscoDAO { get; }", lines[i++], "Linha " + i);
		}

		[TestMethod]
		public void TestNhEntityDaoCodeGenerator()
		{
			var entity = BuildEntityCategoriaRisco();
			entity.Properties.Add(new Property(entity) { Name = "Data", Type = "DateTime" });
			var g = new NhEntityDaoCodeGenerator() { BaseEntity = entity, Template = GetTemplate("NhEntityDAO.template") };

			var code = g.GenerateCode();
			var lines = GetLines(code);

			int i = 0;
			Assert.AreEqual("using MetalSoft.Core.Persistence.NhPersistence;", lines[i++], "Linha " + i);
			Assert.AreEqual("using MetalSoft.Riscos.Model;", lines[i++], "Linha " + i);
			Assert.AreEqual("using NHibernate;", lines[i++], "Linha " + i);
			Assert.AreEqual("using NHibernate.Criterion;", lines[i++], "Linha " + i);
			Assert.AreEqual("using System;", lines[i++], "Linha " + i);
			Assert.AreEqual("using System.Collections.Generic;", lines[i++], "Linha " + i);
			Assert.AreEqual(string.Empty, lines[i++], "Linha " + i);
			Assert.AreEqual("namespace MetalSoft.Riscos.Persistence.NhPersistence", lines[i++], "Linha " + i);
			Assert.AreEqual("{", lines[i++], "Linha " + i);
			Assert.AreEqual("	public class NhCategoriaRiscoDAO", lines[i++], "Linha " + i);
			Assert.AreEqual("		: NHibernateDAO<CategoriaRisco>, CategoriaRiscoDAO", lines[i++], "Linha " + i);
			Assert.AreEqual("	{", lines[i++], "Linha " + i);
			Assert.AreEqual("		public NhCategoriaRiscoDAO(ISession session)", lines[i++], "Linha " + i);
			Assert.AreEqual("			: base(session)", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		public IList<CategoriaRisco> Search(string filterCodigo, PeriodoRisco filterPeriodo, Tipo? filterTipo, DateTime? filterData)", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("			var query = this.SessionInstance.QueryOver<CategoriaRisco>();", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			if (FilterHelper.IsFilterUsed(filterCodigo))", lines[i++], "Linha " + i);
			Assert.AreEqual("				query.WhereRestrictionOn(x => x.Codigo).IsLike(filterCodigo, MatchMode.Anywhere);", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			if (FilterHelper.IsFilterUsed(filterPeriodo))", lines[i++], "Linha " + i);
			Assert.AreEqual("				query.Where(x => x.Periodo == filterPeriodo);", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			if (FilterHelper.IsFilterUsed(filterTipo))", lines[i++], "Linha " + i);
			Assert.AreEqual("				query.Where(x => x.Tipo == filterTipo);", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			if (FilterHelper.IsFilterUsed(filterData))", lines[i++], "Linha " + i);
			Assert.AreEqual("				query.Where(x => x.Data == filterData);", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			query.OrderBy(x => x.AutoId).Desc();", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			if (this.SearchLimit > 0)", lines[i++], "Linha " + i);
			Assert.AreEqual("				query.Take(this.SearchLimit);", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			if (this.SearchPage > 0)", lines[i++], "Linha " + i);
			Assert.AreEqual("				throw new NotImplementedException();", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			return query.List();", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("	}", lines[i++], "Linha " + i);
			Assert.AreEqual("}", lines[i++], "Linha " + i);
		}

		[TestMethod]
		public void TestNhApplicationDaoFactoryCodeGenerator()
		{
			var entity = BuildEntityDisciplina();
			var g = new NhApplicationDaoFactoryCodeGenerator() { BaseEntity = entity, Template = GetTemplate("NhApplicationDAOFactory.template") };

			var code = g.GenerateCode();
			var lines = GetLines(code);

			int i = 0;
			Assert.AreEqual("public DisciplinaDAO DisciplinaDAO", lines[i++], "Linha " + i);
			Assert.AreEqual("{", lines[i++], "Linha " + i);
			Assert.AreEqual("	get", lines[i++], "Linha " + i);
			Assert.AreEqual("	{", lines[i++], "Linha " + i);
			Assert.AreEqual("		if (_disciplinaDAO == null)", lines[i++], "Linha " + i);
			Assert.AreEqual("			_disciplinaDAO = new NhDisciplinaDAO(this.SessionInstance);", lines[i++], "Linha " + i);
			Assert.AreEqual("		return _disciplinaDAO;", lines[i++], "Linha " + i);
			Assert.AreEqual("	}", lines[i++], "Linha " + i);
			Assert.AreEqual("}", lines[i++], "Linha " + i);
			Assert.AreEqual("private DisciplinaDAO _disciplinaDAO = null;", lines[i++], "Linha " + i);
		}

		[TestMethod]
		public void TestMappingCodeGenerator()
		{
			var entity = BuildEntityCategoriaRisco();
			var g = new MappingFileCodeGenerator() { BaseEntity = entity, Template = GetTemplate("Mapping.template") };

			var code = g.GenerateCode();
			var lines = GetLines(code);

			int i = 0;
			Assert.AreEqual("<?xml version=\"1.0\" encoding=\"utf-8\" ?>", lines[i++], "Linha " + i);
			Assert.AreEqual(string.Empty, lines[i++], "Linha " + i);
			Assert.AreEqual("<hibernate-mapping xmlns=\"urn:nhibernate-mapping-2.2\"", lines[i++], "Linha " + i);
			Assert.AreEqual("				   assembly=\"MetalSoft.Riscos.Model\"", lines[i++], "Linha " + i);
			Assert.AreEqual("				   namespace=\"MetalSoft.Riscos.Model\">", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("	<class name=\"CategoriaRisco\">", lines[i++], "Linha " + i);
			Assert.AreEqual("		<id name=\"AutoId\" >", lines[i++], "Linha " + i);
			Assert.AreEqual("			<generator class=\"native\"  />", lines[i++], "Linha " + i);
			Assert.AreEqual("		</id>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		<version name=\"Version\" column=\"Version\" />", lines[i++], "Linha " + i);
			Assert.AreEqual("		<property name=\"CreationDateTime\" column=\"TelosRgDt\" />", lines[i++], "Linha " + i);
			Assert.AreEqual("		<property name=\"LastUpdateDateTime\" column=\"TelosUpDt\" />", lines[i++], "Linha " + i);
			Assert.AreEqual("		<property name=\"CreationUser\" column=\"TelosRgUs\" />", lines[i++], "Linha " + i);
			Assert.AreEqual("		<property name=\"LastUpdateUser\" column=\"TelosUpUs\" />", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		<property name=\"Codigo\" not-null=\"true\" unique-key=\"CategoriaRisco_NaturalKey\"/>", lines[i++], "Linha " + i);
			Assert.AreEqual("		<property name=\"Descricao\" not-null=\"true\" type=\"StringClob\"/>", lines[i++], "Linha " + i);
			Assert.AreEqual("		<many-to-one name=\"Periodo\" not-null=\"true\"/>", lines[i++], "Linha " + i);
			Assert.AreEqual("		<property name=\"Tipo\" not-null=\"true\"/>", lines[i++], "Linha " + i);
			Assert.AreEqual("		<bag name=\"Composicoes\" inverse=\"true\" lazy=\"true\" cascade=\"delete-orphan\">", lines[i++], "Linha " + i);
			Assert.AreEqual("			<key column=\"CategoriaComposta\" />", lines[i++], "Linha " + i);
			Assert.AreEqual("			<one-to-many class=\"ComposicaoCategoria\" />", lines[i++], "Linha " + i);
			Assert.AreEqual("		</bag>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("	</class>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("</hibernate-mapping>", lines[i++], "Linha " + i);
		}

		[TestMethod]
		public void TestCompositionMappingCodeGenerator()
		{
			var entity = new Entity();
			entity.Application = new Application() { Name = "Riscos" };
			entity.EntityName = "ComposicaoCategoria";
			entity.EntityLabel = "Composição";
			entity.IsComposition = true;
			entity.Properties.Add(new Property(entity) { Name = "CategoriaComposta", Type = "CategoriaRisco", Label = "Categoria Composta", Tip = "Categoria composta.", Required = true, IsContainer = true });
			entity.Properties.Add(new Property(entity) { Name = "NomeComposicao", Type = "string", Label = "Nome", Tip = "Nome.", Required = true });

			var g = new MappingFileCodeGenerator() { BaseEntity = entity, Template = GetTemplate("Mapping.template") };

			var code = g.GenerateCode();
			var lines = GetLines(code);

			int i = 0;
			Assert.AreEqual("<?xml version=\"1.0\" encoding=\"utf-8\" ?>", lines[i++], "Linha " + i);
			Assert.AreEqual(string.Empty, lines[i++], "Linha " + i);
			Assert.AreEqual("<hibernate-mapping xmlns=\"urn:nhibernate-mapping-2.2\"", lines[i++], "Linha " + i);
			Assert.AreEqual("				   assembly=\"MetalSoft.Riscos.Model\"", lines[i++], "Linha " + i);
			Assert.AreEqual("				   namespace=\"MetalSoft.Riscos.Model\">", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("	<class name=\"ComposicaoCategoria\">", lines[i++], "Linha " + i);
			Assert.AreEqual("		<id name=\"AutoId\" >", lines[i++], "Linha " + i);
			Assert.AreEqual("			<generator class=\"native\"  />", lines[i++], "Linha " + i);
			Assert.AreEqual("		</id>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		<version name=\"Version\" column=\"Version\" />", lines[i++], "Linha " + i);
			Assert.AreEqual("		<property name=\"CreationDateTime\" column=\"TelosRgDt\" />", lines[i++], "Linha " + i);
			Assert.AreEqual("		<property name=\"LastUpdateDateTime\" column=\"TelosUpDt\" />", lines[i++], "Linha " + i);
			Assert.AreEqual("		<property name=\"CreationUser\" column=\"TelosRgUs\" />", lines[i++], "Linha " + i);
			Assert.AreEqual("		<property name=\"LastUpdateUser\" column=\"TelosUpUs\" />", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		<many-to-one name=\"CategoriaComposta\" not-null=\"true\" index=\"ComposicaoCategoria_CategoriaComposta_Index\"/>", lines[i++], "Linha " + i);
			Assert.AreEqual("		<property name=\"NomeComposicao\" not-null=\"true\"/>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("	</class>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("</hibernate-mapping>", lines[i++], "Linha " + i);
		}

		[TestMethod]
		public void TestDtoCodeGenerator()
		{
			var entity = BuildEntityCategoriaRisco();
			var g = new DtoCodeGenerator() { BaseEntity = entity, Template = GetTemplate("EntityDto.template") };

			var code = g.GenerateCode();
			var lines = GetLines(code);

			int i = 0;
			Assert.AreEqual("using MetalSoft.Core.DataTransferObjects;", lines[i++], "Linha " + i);
			Assert.AreEqual("using System;", lines[i++], "Linha " + i);
			Assert.AreEqual("using System.Collections.Generic;", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("namespace MetalSoft.Riscos.DataTransferObjects", lines[i++], "Linha " + i);
			Assert.AreEqual("{", lines[i++], "Linha " + i);
			Assert.AreEqual("	public class CategoriaRiscoDto", lines[i++], "Linha " + i);
			Assert.AreEqual("		: EntityDto", lines[i++], "Linha " + i);
			Assert.AreEqual("	{", lines[i++], "Linha " + i);
			Assert.AreEqual("		public string Codigo { get; set; }", lines[i++], "Linha " + i);
			Assert.AreEqual("		public string Descricao { get; set; }", lines[i++], "Linha " + i);
			Assert.AreEqual("		public EntityReferenceDto Periodo { get; set; }", lines[i++], "Linha " + i);
			Assert.AreEqual("		public string Tipo { get; set; }", lines[i++], "Linha " + i);
			Assert.AreEqual("		public IList<ComposicaoCategoriaDto> Composicoes { get; set; }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		public CategoriaRiscoDto()", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("			this.Composicoes = new List<ComposicaoCategoriaDto>();", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("	}", lines[i++], "Linha " + i);
			Assert.AreEqual("}", lines[i++], "Linha " + i);
		}

		[TestMethod]
		public void TestCompositionDtoCodeGenerator()
		{
			var entity = new Entity();
			entity.Application = new Application() { Name = "Riscos" };
			entity.EntityName = "ComposicaoCategoria";
			entity.EntityLabel = "Composição";
			entity.IsComposition = true;
			entity.Properties.Add(new Property(entity) { Name = "CategoriaComposta", Type = "CategoriaRisco", Label = "Categoria Composta", Tip = "Categoria composta.", Required = true, IsContainer = true });
			entity.Properties.Add(new Property(entity) { Name = "NomeComposicao", Type = "string", Label = "Nome", Tip = "Nome.", Required = true });
			entity.Properties.Add(new Property(entity) { Name = "PeriodoComposicao", Type = "PeriodoRisco", Label = "Período", Tip = "Período.", Required = true });
			entity.Properties.Add(new Property(entity) { Name = "TipoComposicao", Type = "Tipo", Label = "Tipo", Tip = "Tipo.", Required = true, IsEnum = true });

			var g = new DtoCodeGenerator() { BaseEntity = entity, Template = GetTemplate("EntityDto.template") };

			var code = g.GenerateCode();
			var lines = GetLines(code);

			int i = 0;
			Assert.AreEqual("using MetalSoft.Core.DataTransferObjects;", lines[i++], "Linha " + i);
			Assert.AreEqual("using System;", lines[i++], "Linha " + i);
			Assert.AreEqual("using System.Collections.Generic;", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("namespace MetalSoft.Riscos.DataTransferObjects", lines[i++], "Linha " + i);
			Assert.AreEqual("{", lines[i++], "Linha " + i);
			Assert.AreEqual("	public class ComposicaoCategoriaDto", lines[i++], "Linha " + i);
			Assert.AreEqual("		: EntityDto", lines[i++], "Linha " + i);
			Assert.AreEqual("	{", lines[i++], "Linha " + i);
			Assert.AreEqual("		public string NomeComposicao { get; set; }", lines[i++], "Linha " + i);
			Assert.AreEqual("		public EntityReferenceDto PeriodoComposicao { get; set; }", lines[i++], "Linha " + i);
			Assert.AreEqual("		public string TipoComposicao { get; set; }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		public ComposicaoCategoriaDto()", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			AssertIsEmptyIgnoresWhiteSpace(lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("	}", lines[i++], "Linha " + i);
			Assert.AreEqual("}", lines[i++], "Linha " + i);
		}

		[TestMethod]
		public void TestDtoCodeGeneratorWithNoComposition()
		{
			var entity = BuildEntityDisciplina();
			var g = new DtoCodeGenerator() { BaseEntity = entity, Template = GetTemplate("EntityDto.template") };

			var code = g.GenerateCode();
			var lines = GetLines(code);

			int i = 0;
			Assert.AreEqual("using MetalSoft.Core.DataTransferObjects;", lines[i++], "Linha " + i);
			Assert.AreEqual("using System;", lines[i++], "Linha " + i);
			Assert.AreEqual("using System.Collections.Generic;", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("namespace MetalSoft.Riscos.DataTransferObjects", lines[i++], "Linha " + i);
			Assert.AreEqual("{", lines[i++], "Linha " + i);
			Assert.AreEqual("	public class DisciplinaDto", lines[i++], "Linha " + i);
			Assert.AreEqual("		: EntityDto", lines[i++], "Linha " + i);
			Assert.AreEqual("	{", lines[i++], "Linha " + i);
			Assert.AreEqual("		public string Codigo { get; set; }", lines[i++], "Linha " + i);
			Assert.AreEqual("		public string Nome { get; set; }", lines[i++], "Linha " + i);
			Assert.AreEqual("		public EntityReferenceDto Periodo { get; set; }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		public DisciplinaDto()", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("	}", lines[i++], "Linha " + i);
			Assert.AreEqual("}", lines[i++], "Linha " + i);
		}

		[TestMethod]
		public void TestCollectionDtoCodeGenerator()
		{
			var entity = BuildEntityCategoriaRisco();
			entity.Properties.Add(new Property(entity) { Name = "Data", Type = "DateTime" });
			entity.Properties.Add(new Property(entity) { Name = "Number", Type = "int" });
			var g = new CollectionDtoCodeGenerator() { BaseEntity = entity, Template = GetTemplate("CollectionDto.template") };

			var code = g.GenerateCode();
			var lines = GetLines(code);

			int i = 0;
			Assert.AreEqual("using MetalSoft.Core.DataTransferObjects;", lines[i++], "Linha " + i);
			Assert.AreEqual("using System;", lines[i++], "Linha " + i);
			Assert.AreEqual("using System.Collections.Generic;", lines[i++], "Linha " + i);
			Assert.AreEqual(string.Empty, lines[i++], "Linha " + i);
			Assert.AreEqual("namespace MetalSoft.Riscos.DataTransferObjects", lines[i++], "Linha " + i);
			Assert.AreEqual("{", lines[i++], "Linha " + i);
			Assert.AreEqual("	public class CategoriasRiscosDto", lines[i++], "Linha " + i);
			Assert.AreEqual("		: CollectionDto<CategoriaRiscoDto>", lines[i++], "Linha " + i);
			Assert.AreEqual("	{", lines[i++], "Linha " + i);
			Assert.AreEqual("		public string FilterCodigo { get; set; }", lines[i++], "Linha " + i);
			Assert.AreEqual("		public EntityReferenceDto FilterPeriodo { get; set; }", lines[i++], "Linha " + i);
			Assert.AreEqual("		public string FilterTipo { get; set; }", lines[i++], "Linha " + i);
			Assert.AreEqual("		public DateTime? FilterData { get; set; }", lines[i++], "Linha " + i);
			Assert.AreEqual("		public int? FilterNumber { get; set; }", lines[i++], "Linha " + i);
			Assert.AreEqual("	}", lines[i++], "Linha " + i);
			Assert.AreEqual("}", lines[i++], "Linha " + i);
		}

		[TestMethod]
		public void TestEntityControllerCodeGenerator_Full()
		{
			var entity = BuildEntityCategoriaRisco();
			var g = new EntityControllerCodeGenerator() { BaseEntity = entity, Template = GetTemplate("FullController.template") };

			var code = g.GenerateCode();
			var lines = GetLines(code);

			int i = 0;
			Assert.AreEqual("using MetalSoft.Core.DataTransferObjects;", lines[i++], "Linha " + i);
			Assert.AreEqual("using MetalSoft.Core.Persistence;", lines[i++], "Linha " + i);
			Assert.AreEqual("using MetalSoft.Core.WebAPI;", lines[i++], "Linha " + i);
			Assert.AreEqual("using MetalSoft.Core.WebAPI.Controllers;", lines[i++], "Linha " + i);
			Assert.AreEqual("using MetalSoft.Core.WebAPI.Wrappers;", lines[i++], "Linha " + i);
			Assert.AreEqual("using MetalSoft.Riscos.DataTransferObjects;", lines[i++], "Linha " + i);
			Assert.AreEqual("using MetalSoft.Riscos.Model;", lines[i++], "Linha " + i);
			Assert.AreEqual("using MetalSoft.Riscos.Persistence;", lines[i++], "Linha " + i);
			Assert.AreEqual("using Microsoft.AspNetCore.Mvc;", lines[i++], "Linha " + i);
			Assert.AreEqual("using Microsoft.Extensions.Options;", lines[i++], "Linha " + i);
			Assert.AreEqual("using System;", lines[i++], "Linha " + i);
			Assert.AreEqual(string.Empty, lines[i++], "Linha " + i);
			Assert.AreEqual("namespace MetalSoft.Riscos.WebAPI.Controllers", lines[i++], "Linha " + i);
			Assert.AreEqual("{", lines[i++], "Linha " + i);
			Assert.AreEqual("	[Route(\"api/[controller]\")]", lines[i++], "Linha " + i);
			Assert.AreEqual("	public class CategoriaRiscoController", lines[i++], "Linha " + i);
			Assert.AreEqual("		: TransactionalApiContoller", lines[i++], "Linha " + i);
			Assert.AreEqual("	{", lines[i++], "Linha " + i);
			Assert.AreEqual("		public CategoriaRiscoController(IOptions<AppSettings> appSettings, TransactionManagerFactory tmf)", lines[i++], "Linha " + i);
			Assert.AreEqual("			: base(appSettings, tmf)", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		[HttpGet]", lines[i++], "Linha " + i);
			Assert.AreEqual("		public IActionResult Get(int id)", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("			return Ok(InvokeCommandInsideTransaction(daoFactory => Get(daoFactory, id)));", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		private CategoriaRiscoDto Get(DAOFactory daoFactory, int id)", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("			CategoriaRiscoDAO dao = GetDAO(daoFactory);", lines[i++], "Linha " + i);
			Assert.AreEqual("			var entidade = dao.Load(id);", lines[i++], "Linha " + i);
			Assert.AreEqual("			return EntityWrapper.Wrap<CategoriaRiscoDto>(entidade);", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		[HttpPost]", lines[i++], "Linha " + i);
			Assert.AreEqual("		public IActionResult Update([FromBody]CategoriaRiscoDto dto)", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("			return Ok(InvokeCommandInsideTransaction(daoFactory => Update(daoFactory, dto)));", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		private CategoriaRiscoDto Update(DAOFactory daoFactory, CategoriaRiscoDto dto)", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("			if (dto == null)", lines[i++], "Linha " + i);
			Assert.AreEqual("				throw new ArgumentNullException(nameof(dto));", lines[i++], "Linha " + i);
			Assert.AreEqual("			var appDaoFactory = (RiscosDAOFactory)daoFactory;", lines[i++], "Linha " + i);
			Assert.AreEqual("			CategoriaRiscoDAO dao = appDaoFactory.CategoriaRiscoDAO;", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			//LOAD/WRAP", lines[i++], "Linha " + i);
			Assert.AreEqual("			CategoriaRisco entity = dto.AutoId > 0", lines[i++], "Linha " + i);
			Assert.AreEqual("				? dao.Load(dto.AutoId)", lines[i++], "Linha " + i);
			Assert.AreEqual("				: new CategoriaRisco();", lines[i++], "Linha " + i);
			Assert.AreEqual("			EntityWrapper.Copy(dto, entity);", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			entity.Periodo = SolveProxy(appDaoFactory.PeriodoRiscoDAO, dto.Periodo);", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			List<ComposicaoCategoria> condemnedComposicoes = new List<ComposicaoCategoria>();", lines[i++], "Linha " + i);
			Assert.AreEqual("			foreach (ComposicaoCategoriaDto childDto in dto.Composicoes)", lines[i++], "Linha " + i);
			Assert.AreEqual("			{", lines[i++], "Linha " + i);
			Assert.AreEqual("				ComposicaoCategoria childEntity = childDto.AutoId > 0", lines[i++], "Linha " + i);
			Assert.AreEqual("					? appDaoFactory.ComposicaoCategoriaDAO.Load(childDto.AutoId)", lines[i++], "Linha " + i);
			Assert.AreEqual("					: new ComposicaoCategoria();", lines[i++], "Linha " + i);
			Assert.AreEqual("				EntityWrapper.Copy(childDto, childEntity);", lines[i++], "Linha " + i);
			Assert.AreEqual("				//TODO solve proxies", lines[i++], "Linha " + i);
			Assert.AreEqual("				if (!childEntity.IsPersistent) ", lines[i++], "Linha " + i);
			Assert.AreEqual("				{", lines[i++], "Linha " + i);
			Assert.AreEqual("					if (!childDto.DeleteMe)", lines[i++], "Linha " + i);
			Assert.AreEqual("						entity.Composicoes.Add(childEntity);", lines[i++], "Linha " + i);
			Assert.AreEqual("				}", lines[i++], "Linha " + i);
			Assert.AreEqual("				else if (childDto.DeleteMe)", lines[i++], "Linha " + i);
			Assert.AreEqual("				{", lines[i++], "Linha " + i);
			Assert.AreEqual("					entity.Composicoes.Remove(childEntity);", lines[i++], "Linha " + i);
			Assert.AreEqual("					condemnedComposicoes.Add(childEntity);", lines[i++], "Linha " + i);
			Assert.AreEqual("				}", lines[i++], "Linha " + i);
			Assert.AreEqual("			}", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			//DELEGATE BUSINESS", lines[i++], "Linha " + i);
			Assert.AreEqual("			entity.Validate();", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			foreach (ComposicaoCategoria childEntity in entity.Composicoes)", lines[i++], "Linha " + i);
			Assert.AreEqual("				childEntity.Validate();", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			//PERSIST", lines[i++], "Linha " + i);
			Assert.AreEqual("			dao.Update(entity);", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			foreach (ComposicaoCategoria childEntity in condemnedComposicoes)", lines[i++], "Linha " + i);
			Assert.AreEqual("				appDaoFactory.ComposicaoCategoriaDAO.Delete(childEntity);", lines[i++], "Linha " + i);
			Assert.AreEqual("			foreach (ComposicaoCategoria childEntity in entity.Composicoes)", lines[i++], "Linha " + i);
			Assert.AreEqual("				appDaoFactory.ComposicaoCategoriaDAO.Update(childEntity);", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			//DELIVER/WRAP", lines[i++], "Linha " + i);
			Assert.AreEqual("			CategoriaRiscoDto dtoAfterUpdate = EntityWrapper.Wrap<CategoriaRiscoDto>(entity);", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			foreach (ComposicaoCategoria childEntity in entity.Composicoes)", lines[i++], "Linha " + i);
			Assert.AreEqual("				dtoAfterUpdate.Composicoes.Add(EntityWrapper.Wrap<ComposicaoCategoriaDto>(childEntity));", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			return dtoAfterUpdate;", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		[HttpDelete]", lines[i++], "Linha " + i);
			Assert.AreEqual("		public CategoriaRiscoDto Delete(int id)", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("			return InvokeCommandInsideTransaction(daoFactory => Delete(daoFactory, id));", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		private CategoriaRiscoDto Delete(DAOFactory daoFactory, int id)", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("			var appDaoFactory = (RiscosDAOFactory)daoFactory;", lines[i++], "Linha " + i);
			Assert.AreEqual("			CategoriaRiscoDAO dao = appDaoFactory.CategoriaRiscoDAO;", lines[i++], "Linha " + i);
			Assert.AreEqual("			CategoriaRisco entity = dao.Load(id);", lines[i++], "Linha " + i);
			Assert.AreEqual("			dao.Delete(entity);", lines[i++], "Linha " + i);
			Assert.AreEqual("			return new CategoriaRiscoDto();", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		[HttpPost]", lines[i++], "Linha " + i);
			Assert.AreEqual("		[Route(\"search\")]", lines[i++], "Linha " + i);
			Assert.AreEqual("		public IActionResult Search([FromBody]CategoriasRiscosDto dto)", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("			return Ok(InvokeCommandInsideTransaction(daoFactory => Search(daoFactory, dto)));", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		private CategoriasRiscosDto Search(DAOFactory daoFactory, CategoriasRiscosDto dto)", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("			if (dto == null)", lines[i++], "Linha " + i);
			Assert.AreEqual("				throw new ArgumentNullException(nameof(dto));", lines[i++], "Linha " + i);
			Assert.AreEqual("			var appDaoFactory = (RiscosDAOFactory)daoFactory;", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			PeriodoRisco filterPeriodo = SolveProxy(appDaoFactory.PeriodoRiscoDAO, dto.FilterPeriodo);", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			Tipo? filterTipo = EntityWrapper.ParseEnum<Tipo>(dto.FilterTipo);", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			var items = appDaoFactory.CategoriaRiscoDAO.Search(dto.FilterCodigo, dto.FilterDescricao, filterPeriodo, filterTipo);", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			return EntityWrapper.Wrap<CategoriasRiscosDto, CategoriaRiscoDto, CategoriaRisco>(items);", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);

			//TODO o wrapper vai ter que mudar pq nao trata a composicao
			//TODO testar restante
		}

		[TestMethod]
		public void TestEntityControllerCodeGenerator_Inherited()
		{
			var entity = BuildEntityCategoriaRisco();
			var g = new EntityControllerCodeGenerator() { BaseEntity = entity, Template = GetTemplate("InheritedController.template") };

			var code = g.GenerateCode();
			var lines = GetLines(code);

			int i = 0;
			Assert.AreEqual("using MetalSoft.Core.DataTransferObjects;", lines[i++], "Linha " + i);
			Assert.AreEqual("using MetalSoft.Core.Persistence;", lines[i++], "Linha " + i);
			Assert.AreEqual("using MetalSoft.Core.WebAPI;", lines[i++], "Linha " + i);
			Assert.AreEqual("using MetalSoft.Core.WebAPI.Controllers;", lines[i++], "Linha " + i);
			Assert.AreEqual("using MetalSoft.Core.WebAPI.Wrappers;", lines[i++], "Linha " + i);
			Assert.AreEqual("using MetalSoft.Riscos.DataTransferObjects;", lines[i++], "Linha " + i);
			Assert.AreEqual("using MetalSoft.Riscos.Model;", lines[i++], "Linha " + i);
			Assert.AreEqual("using MetalSoft.Riscos.Persistence;", lines[i++], "Linha " + i);
			Assert.AreEqual("using Microsoft.AspNetCore.Mvc;", lines[i++], "Linha " + i);
			Assert.AreEqual("using Microsoft.Extensions.Options;", lines[i++], "Linha " + i);
			Assert.AreEqual("using System;", lines[i++], "Linha " + i);
			Assert.AreEqual(string.Empty, lines[i++], "Linha " + i);
			Assert.AreEqual("namespace MetalSoft.Riscos.WebAPI.Controllers", lines[i++], "Linha " + i);
			Assert.AreEqual("{", lines[i++], "Linha " + i);
			Assert.AreEqual("	[Route(\"api/[controller]\")]", lines[i++], "Linha " + i);
			Assert.AreEqual("	public class CategoriaRiscoController", lines[i++], "Linha " + i);
			Assert.AreEqual("		: GenericController<CategoriaRiscoDto, CategoriaRisco>", lines[i++], "Linha " + i);
			Assert.AreEqual("	{", lines[i++], "Linha " + i);
			Assert.AreEqual("		public CategoriaRiscoController(IOptions<AppSettings> appSettings, TransactionManagerFactory tmf)", lines[i++], "Linha " + i);
			Assert.AreEqual("			: base(appSettings, tmf)", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		protected override DAO<CategoriaRisco> GetDAO(DAOFactory daoFactory)", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("			return ((RiscosDAOFactory)daoFactory).CategoriaRiscoDAO;", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		[HttpPost]", lines[i++], "Linha " + i);
			Assert.AreEqual("		[Route(\"search\")]", lines[i++], "Linha " + i);
			Assert.AreEqual("		public IActionResult Search([FromBody]CategoriasRiscosDto dto)", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("			return Ok(InvokeCommandInsideTransaction(daoFactory => Search(daoFactory, dto)));", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("		private CategoriasRiscosDto Search(DAOFactory daoFactory, CategoriasRiscosDto dto)", lines[i++], "Linha " + i);
			Assert.AreEqual("		{", lines[i++], "Linha " + i);
			Assert.AreEqual("			if (dto == null)", lines[i++], "Linha " + i);
			Assert.AreEqual("				throw new ArgumentNullException(nameof(dto));", lines[i++], "Linha " + i);
			Assert.AreEqual("			CategoriaRiscoDAO dao = ((RiscosDAOFactory)daoFactory).CategoriaRiscoDAO;", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			PeriodoRisco filterPeriodo = SolveProxy(appDaoFactory.PeriodoRiscoDAO, dto.FilterPeriodo);", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			Tipo? filterTipo = EntityWrapper.ParseEnum<Tipo>(dto.FilterTipo);", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			var items = dao.Search(dto.FilterCodigo, dto.FilterDescricao, filterPeriodo, filterTipo);", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("			return EntityWrapper.Wrap<CategoriasRiscosDto, CategoriaRiscoDto, CategoriaRisco>(items);", lines[i++], "Linha " + i);
			Assert.AreEqual("		}", lines[i++], "Linha " + i);
			Assert.AreEqual("	}", lines[i++], "Linha " + i);
			Assert.AreEqual("}", lines[i++], "Linha " + i);
			//TODO o wrapper vai ter que mudar pq nao trata a composicao
		}

		[TestMethod]
		public void TestServerFileNames()
		{
			var entity = BuildEntityDisciplina();
			Assert.AreEqual("RiscosDAOFactory-TODO-add-Disciplina.txt", GetFileName(entity, new ApplicationDaoFactoryCodeGenerator()));
			Assert.AreEqual("DisciplinasDto.cs", GetFileName(entity, new CollectionDtoCodeGenerator()));
			Assert.AreEqual("DisciplinaDto.cs", GetFileName(entity, new DtoCodeGenerator()));
			Assert.AreEqual("DisciplinaController.cs", GetFileName(entity, new EntityControllerCodeGenerator()));
			Assert.AreEqual("DisciplinaDAO.cs", GetFileName(entity, new EntityDaoCodeGenerator()));
			Assert.AreEqual("Disciplina.hbm.xml", GetFileName(entity, new MappingFileCodeGenerator()));
			Assert.AreEqual("Disciplina.cs", GetFileName(entity, new ModelCodeGenerator()));
			Assert.AreEqual("NhRiscosDAOFactory-TODO-add-Disciplina.txt", GetFileName(entity, new NhApplicationDaoFactoryCodeGenerator()));
			Assert.AreEqual("NhDisciplinaDAO.cs", GetFileName(entity, new NhEntityDaoCodeGenerator()));
			Assert.AreEqual("disciplina.model.ts", GetFileName(entity, new AngularEntryModelCodeGenerator()));
		}

		[TestMethod]
		public void TestAngularFileNames()
		{
			var entity = BuildEntityCategoriaRisco();
			Assert.AreEqual("categoria-risco.model.ts", GetFileName(entity, new AngularEntryModelCodeGenerator()));
			Assert.AreEqual("categorias-riscos.model.ts", GetFileName(entity, new AngularListModelCodeGenerator()));
			Assert.AreEqual("categoria-risco-entry.component.ts", GetFileName(entity, new AngularEntryComponentCodeGenerator()));
			Assert.AreEqual("categorias-riscos-list.component.ts", GetFileName(entity, new AngularListComponentCodeGenerator()));
			Assert.AreEqual("categoria-risco-entry.component.html", GetFileName(entity, new AngularEntryComponentTemplateCodeGenerator()));
			Assert.AreEqual("categorias-riscos-list.component.html", GetFileName(entity, new AngularListComponentTemplateCodeGenerator()));
			Assert.AreEqual("categoria-risco-entry.component.css", GetFileName(entity, new AngularEntryComponentCssCodeGenerator()));
			Assert.AreEqual("categorias-riscos-list.component.css", GetFileName(entity, new AngularListComponentCssCodeGenerator()));
		}

		private string GetFileName(Entity e, CodeGenerator g)
		{
			g.BaseEntity = e;
			return g.GetFileName();
		}

		//TODO webapi: get tem que fazer fullwrap quando tem composicao ou associacao (resolver proxy)
		//TODO webapi: update de entidade com composição... falta carregar proxies da composicao
		//TODO webapi: update de entidade com composição... nao trata composicao de composicao
		//TODO angular: entry: possibleValuesFor... quando tem mais de um, nao da quebra de linha entre eles
		//TODO criar classes de parametro de pesquisa?
		//TODO angular: entry: a grid conta itens excluídos
		//TODO gerar dao factoy e nhdaofactory em arquivo único e em ordem alfabética
		//TODO gerar nhibernate.cfg.xml
		//TODO testes do AppBuilder?
		//TODO gerar enumerações no modelo, no angular/model e imports em angular entry e list components
		//TODO webapi: diferenciar se vai gerar contoller full ou inherited (acho que gerar sempre inherited)
		//TODO webapi: é melhor o wrap da pesquisa ser gerado sem usar EntityWrapper.Wrap para facilitar a customizacao

		[TestMethod]
		public void TestAngularEntryModelCodeGenerator()
		{
			var entity = BuildEntityCategoriaRisco();
			var g = new AngularEntryModelCodeGenerator() { BaseEntity = entity, Template = GetTemplate(@"angular\entry-model.template") };

			var code = g.GenerateCode();
			var lines = GetLines(code);

			int i = 0;
			Assert.AreEqual("import { Entity } from '../metalsoft-core/model/entity';", lines[i++], "Linha " + i);
			Assert.AreEqual("import { EntityReference } from '../metalsoft-core/model/entity-reference';", lines[i++], "Linha " + i);
			Assert.AreEqual("import { ComposicaoCategoria } from './composicao-categoria.model';", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("export class CategoriaRisco extends Entity {", lines[i++], "Linha " + i);
			Assert.AreEqual("  codigo: string;", lines[i++], "Linha " + i);
			Assert.AreEqual("  descricao: string;", lines[i++], "Linha " + i);
			Assert.AreEqual("  periodo: EntityReference;", lines[i++], "Linha " + i);
			Assert.AreEqual("  tipo: string;", lines[i++], "Linha " + i);
			Assert.AreEqual("  composicoes: Array<ComposicaoCategoria> = new Array<ComposicaoCategoria>();", lines[i++], "Linha " + i);
			Assert.AreEqual("}", lines[i++], "Linha " + i);
		}

		[TestMethod]
		public void TestAngularCompositionModelCodeGenerator()
		{
			var entity = new Entity();
			entity.Application = new Application() { Name = "Riscos" };
			entity.EntityName = "ComposicaoCategoria";
			entity.EntityLabel = "Composição";
			entity.IsComposition = true;
			entity.Properties.Add(new Property(entity) { Name = "CategoriaComposta", Type = "CategoriaRisco", Label = "Categoria Composta", Tip = "Categoria composta.", Required = true, IsContainer = true });
			entity.Properties.Add(new Property(entity) { Name = "NomeComposicao", Type = "string", Label = "Nome", Tip = "Nome.", Required = true });
			entity.Properties.Add(new Property(entity) { Name = "PeriodoComposicao", Type = "PeriodoRisco", Label = "Período", Tip = "Período.", Required = true });
			entity.Properties.Add(new Property(entity) { Name = "TipoComposicao", Type = "Tipo", Label = "Tipo", Tip = "Tipo.", Required = true, IsEnum = true });

			var g = new AngularEntryModelCodeGenerator() { BaseEntity = entity, Template = GetTemplate(@"angular\entry-model.template") };

			var code = g.GenerateCode();
			var lines = GetLines(code);

			int i = 0;
			Assert.AreEqual("import { Entity } from '../metalsoft-core/model/entity';", lines[i++], "Linha " + i);
			Assert.AreEqual("import { EntityReference } from '../metalsoft-core/model/entity-reference';", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("export class ComposicaoCategoria extends Entity {", lines[i++], "Linha " + i);
			Assert.AreEqual("  nomeComposicao: string;", lines[i++], "Linha " + i);
			Assert.AreEqual("  periodoComposicao: EntityReference;", lines[i++], "Linha " + i);
			Assert.AreEqual("  tipoComposicao: string;", lines[i++], "Linha " + i);
			Assert.AreEqual("}", lines[i++], "Linha " + i);
		}

		[TestMethod]
		public void TestAngularListModelCodeGenerator()
		{
			var entity = BuildEntityCategoriaRisco();
			var g = new AngularListModelCodeGenerator() { BaseEntity = entity, Template = GetTemplate(@"angular\list-model.template") };

			var code = g.GenerateCode();
			var lines = GetLines(code);

			int i = 0;
			Assert.AreEqual("import { Collection } from '../metalsoft-core/model/collection';", lines[i++], "Linha " + i);
			Assert.AreEqual("import { EntityReference } from '../metalsoft-core/model/entity-reference';", lines[i++], "Linha " + i);
			Assert.AreEqual("import { CategoriaRisco } from './categoria-risco.model';", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("export class CategoriasRiscos extends Collection {", lines[i++], "Linha " + i);
			Assert.AreEqual("  filterCodigo: string;", lines[i++], "Linha " + i);
			Assert.AreEqual("  filterPeriodo: EntityReference;", lines[i++], "Linha " + i);
			Assert.AreEqual("  filterTipo: string;", lines[i++], "Linha " + i);
			Assert.AreEqual("  items: Array<CategoriaRisco>;", lines[i++], "Linha " + i);
			Assert.AreEqual("}", lines[i++], "Linha " + i);
		}

		[TestMethod]
		public void TestAngularServiceCodeGenerator()
		{
			var entity = BuildEntityCategoriaRisco();
			var g = new AngularServiceCodeGenerator() { BaseEntity = entity, Template = GetTemplate(@"angular\service.template") };

			var code = g.GenerateCode();
			var lines = GetLines(code);

			int i = 0;
			Assert.AreEqual("import { Injectable } from '@angular/core';", lines[i++], "Linha " + i);
			Assert.AreEqual("import { Observable } from 'rxjs/Observable';", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("import { EntitiesReferences } from '../metalsoft-core/model/entities-references';", lines[i++], "Linha " + i);
			Assert.AreEqual("import { GenericService } from '../metalsoft-core/services/generic.service';", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("import { CategoriaRisco } from '../model/categoria-risco.model';", lines[i++], "Linha " + i);
			Assert.AreEqual("import { CategoriasRiscos } from '../model/categorias-riscos.model';", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("@Injectable()", lines[i++], "Linha " + i);
			Assert.AreEqual("export class CategoriaRiscoService {", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  uri = 'CategoriaRisco';", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  constructor(private generic: GenericService) { }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  delete(id: number): Observable<CategoriaRisco>", lines[i++], "Linha " + i);
			Assert.AreEqual("  {", lines[i++], "Linha " + i);
			Assert.AreEqual("    return this.generic.delete<CategoriaRisco>(this.uri, id);", lines[i++], "Linha " + i);
			Assert.AreEqual("  }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  get(id: number): Observable<CategoriaRisco> {", lines[i++], "Linha " + i);
			Assert.AreEqual("    return this.generic.get<CategoriaRisco>(this.uri, id);", lines[i++], "Linha " + i);
			Assert.AreEqual("  }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  loadAllAsReferences(): Observable<EntitiesReferences>", lines[i++], "Linha " + i);
			Assert.AreEqual("  {", lines[i++], "Linha " + i);
			Assert.AreEqual("    return this.generic.getAll<EntitiesReferences>(`${this.uri}/all`);", lines[i++], "Linha " + i);
			Assert.AreEqual("  }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  search(dto: CategoriasRiscos): Observable<CategoriasRiscos> {", lines[i++], "Linha " + i);
			Assert.AreEqual("    return this.generic.post<CategoriasRiscos>(`${this.uri}/search`, dto);", lines[i++], "Linha " + i);
			Assert.AreEqual("  }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  update(dto: CategoriaRisco): Observable<CategoriaRisco> {", lines[i++], "Linha " + i);
			Assert.AreEqual("    return this.generic.post<CategoriaRisco>(this.uri, dto);", lines[i++], "Linha " + i);
			Assert.AreEqual("  }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("}", lines[i++], "Linha " + i);
		}

		[TestMethod]
		public void TestAngularEntryComponentCodeGenerator()
		{
			var entity = BuildEntityCategoriaRisco();
			var g = new AngularEntryComponentCodeGenerator() { BaseEntity = entity, Template = GetTemplate(@"angular\entry-component.template") };

			var code = g.GenerateCode();
			var lines = GetLines(code);

			int i = 0;

			Assert.AreEqual("import { Observable } from 'rxjs/Observable';", lines[i++], "Linha " + i);
			Assert.AreEqual("import { Subject } from 'rxjs/Subject';", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("import { Component, OnInit } from '@angular/core';", lines[i++], "Linha " + i);
			Assert.AreEqual("import { Router, ActivatedRoute, ParamMap } from '@angular/router';", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("import { EntityReference } from '../../metalsoft-core/model/entity-reference';", lines[i++], "Linha " + i);
			Assert.AreEqual("import { EntitiesReferences } from '../../metalsoft-core/model/entities-references';", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("import { AlertService } from '../../metalsoft-core/local-services/alert.service';", lines[i++], "Linha " + i);
			Assert.AreEqual("import { CategoriaRisco } from '../../model/categoria-risco.model';", lines[i++], "Linha " + i);
			Assert.AreEqual("import { ComposicaoCategoria } from '../../model/composicao-categoria.model';", lines[i++], "Linha " + i);
			Assert.AreEqual("import { CategoriaRiscoService } from '../../services/categoria-risco.service';", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("@Component({", lines[i++], "Linha " + i);
			Assert.AreEqual("  selector: 'categoria-risco-entry',", lines[i++], "Linha " + i);
			Assert.AreEqual("  templateUrl: './categoria-risco-entry.component.html',", lines[i++], "Linha " + i);
			Assert.AreEqual("  styleUrls: ['./categoria-risco-entry.component.css'],", lines[i++], "Linha " + i);
			Assert.AreEqual("  providers: [CategoriaRiscoService]", lines[i++], "Linha " + i);
			Assert.AreEqual("})", lines[i++], "Linha " + i);
			Assert.AreEqual("export class CategoriaRiscoEntryComponent implements OnInit {", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  public model = new CategoriaRisco();", lines[i++], "Linha " + i);
			Assert.AreEqual("  public possibleValuesForTipo = Tipo.All;", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  constructor(", lines[i++], "Linha " + i);
			Assert.AreEqual("    private service: CategoriaRiscoService,", lines[i++], "Linha " + i);
			Assert.AreEqual("    private alertService: AlertService,", lines[i++], "Linha " + i);
			Assert.AreEqual("    private route: ActivatedRoute,", lines[i++], "Linha " + i);
			Assert.AreEqual("    private router: Router) { }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  ngOnInit() {", lines[i++], "Linha " + i);
			Assert.AreEqual("    let id = Number(this.route.snapshot.paramMap.get('id'));", lines[i++], "Linha " + i);
			Assert.AreEqual("    if (id != NaN && id > 0)", lines[i++], "Linha " + i);
			Assert.AreEqual("      this.load(id);", lines[i++], "Linha " + i);
			Assert.AreEqual("    else", lines[i++], "Linha " + i);
			Assert.AreEqual("      this.createNew();", lines[i++], "Linha " + i);
			Assert.AreEqual("  }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  load(id: number): void {", lines[i++], "Linha " + i);
			Assert.AreEqual("    this.service.get(id).subscribe(dto => {", lines[i++], "Linha " + i);
			Assert.AreEqual("      if (!dto.response.hasException) {", lines[i++], "Linha " + i);
			Assert.AreEqual("        this.model = dto;", lines[i++], "Linha " + i);
			Assert.AreEqual("      }", lines[i++], "Linha " + i);
			Assert.AreEqual("    });", lines[i++], "Linha " + i);
			Assert.AreEqual("  }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  update(): void {", lines[i++], "Linha " + i);
			Assert.AreEqual("    this.service.update(this.model).subscribe(dto => {", lines[i++], "Linha " + i);
			Assert.AreEqual("      if (!dto.response.hasException) {", lines[i++], "Linha " + i);
			Assert.AreEqual("        this.alertService.success('Update CategoriaRisco ok!');", lines[i++], "Linha " + i);
			Assert.AreEqual("        this.model = dto; //refresh", lines[i++], "Linha " + i);
			Assert.AreEqual("      }", lines[i++], "Linha " + i);
			Assert.AreEqual("    });", lines[i++], "Linha " + i);
			Assert.AreEqual("  }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  delete(): void {", lines[i++], "Linha " + i);
			Assert.AreEqual("    this.service.delete(this.model.autoId).subscribe(dto => {", lines[i++], "Linha " + i);
			Assert.AreEqual("      if (!dto.response.hasException) {", lines[i++], "Linha " + i);
			Assert.AreEqual("        this.alertService.success('Delete CategoriaRisco ok!');", lines[i++], "Linha " + i);
			Assert.AreEqual("        this.model = dto; //refresh", lines[i++], "Linha " + i);
			Assert.AreEqual("      }", lines[i++], "Linha " + i);
			Assert.AreEqual("    });", lines[i++], "Linha " + i);
			Assert.AreEqual("  }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  createNew(): void {", lines[i++], "Linha " + i);
			Assert.AreEqual("    this.model = new CategoriaRisco();", lines[i++], "Linha " + i);
			Assert.AreEqual("  }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  search() : void {", lines[i++], "Linha " + i);
			Assert.AreEqual("    this.router.navigateByUrl('/categorias-riscos');", lines[i++], "Linha " + i);
			Assert.AreEqual("  }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  isPersistent(): boolean {", lines[i++], "Linha " + i);
			Assert.AreEqual("    return this.model.autoId > 0;", lines[i++], "Linha " + i);
			Assert.AreEqual("  }", lines[i++], "Linha " + i);
			AssertIsEmptyIgnoresWhiteSpace(lines[i++], "Linha " + i);
			AssertIsEmptyIgnoresWhiteSpace(lines[i++], "Linha " + i);
			Assert.AreEqual("  removeFromComposicoes(index: number): void {", lines[i++], "Linha " + i);
			Assert.AreEqual("    this.model.composicoes[index].deleteMe = true;", lines[i++], "Linha " + i);
			Assert.AreEqual("  }", lines[i++], "Linha " + i);
			AssertIsEmptyIgnoresWhiteSpace(lines[i++], "Linha " + i);
			Assert.AreEqual("  addToComposicoes(): void {", lines[i++], "Linha " + i);
			Assert.AreEqual("    this.model.composicoes.push(new ComposicaoCategoria());", lines[i++], "Linha " + i);
			Assert.AreEqual("  }", lines[i++], "Linha " + i);
			AssertIsEmptyIgnoresWhiteSpace(lines[i++], "Linha " + i);
			Assert.AreEqual("}", lines[i++], "Linha " + i);
		}

		[TestMethod]
		public void TestAngularEntryComponentTemplateCodeGenerator()
		{
			var entity = BuildEntityCategoriaRisco();
			var g = new AngularEntryComponentTemplateCodeGenerator() { BaseEntity = entity, Template = GetTemplate(@"angular\entry-component-template.template") };

			var code = g.GenerateCode();
			var lines = GetLines(code);

			int i = 0;

			Assert.AreEqual("<div>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  <div>", lines[i++], "Linha " + i);
			Assert.AreEqual("    <div class=\"msf-toolbar\">", lines[i++], "Linha " + i);
			Assert.AreEqual("      <button class=\"btn msf-toolbar-button\" title=\"Salvar\"", lines[i++], "Linha " + i);
			Assert.AreEqual("              [disabled]=\"!thisForm.form.valid\"", lines[i++], "Linha " + i);
			Assert.AreEqual("              (click)=\"update()\">", lines[i++], "Linha " + i);
			Assert.AreEqual("        <img src=\"../../../assets/save_24px.svg\">", lines[i++], "Linha " + i);
			Assert.AreEqual("      </button>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("      <button class=\"btn msf-toolbar-button\" title=\"Excluir\"", lines[i++], "Linha " + i);
			Assert.AreEqual("              [disabled]=\"model.isPersistent && !model.isPersistent()\"", lines[i++], "Linha " + i);
			Assert.AreEqual("              (click)=\"delete()\">", lines[i++], "Linha " + i);
			Assert.AreEqual("        <img src=\"../../../assets/remove_24px.svg\">", lines[i++], "Linha " + i);
			Assert.AreEqual("      </button>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("      <button class=\"btn msf-toolbar-button\" title=\"Novo\"", lines[i++], "Linha " + i);
			Assert.AreEqual("              (click)=\"createNew()\">", lines[i++], "Linha " + i);
			Assert.AreEqual("        <img src=\"../../../assets/new_24px.svg\">", lines[i++], "Linha " + i);
			Assert.AreEqual("      </button>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("      <button class=\"btn msf-toolbar-button\" title=\"Pesquisar\"", lines[i++], "Linha " + i);
			Assert.AreEqual("              (click)=\"search()\">", lines[i++], "Linha " + i);
			Assert.AreEqual("        <img src=\"../../../assets/back_24px.svg\">", lines[i++], "Linha " + i);
			Assert.AreEqual("      </button>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("      <button class=\"btn msf-toolbar-button\" title=\"{{model|json}}\">", lines[i++], "Linha " + i);
			Assert.AreEqual("        <img src=\"../../../assets/bug_24px.svg\">", lines[i++], "Linha " + i);
			Assert.AreEqual("      </button>", lines[i++], "Linha " + i);
			Assert.AreEqual("    </div>", lines[i++], "Linha " + i);
			Assert.AreEqual("  </div>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  <div class=\"msf-form-title\">Categoria de Risco</div>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  <form class=\"msf-form\" #thisForm=\"ngForm\">", lines[i++], "Linha " + i);
			AssertIsEmptyIgnoresWhiteSpace(lines[i++], "Linha " + i);
			Assert.AreEqual("    <div class=\"form-group col-lg-10\">", lines[i++], "Linha " + i);
			Assert.AreEqual("      <metalsoft-text-field [(text)]=\"model.codigo\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                            [label]=\"'Código'\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                            [tip]=\"'Código mnemônico.'\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                            [required]=\"true\"></metalsoft-text-field>", lines[i++], "Linha " + i);
			Assert.AreEqual("    </div>", lines[i++], "Linha " + i);
			AssertIsEmptyIgnoresWhiteSpace(lines[i++], "Linha " + i);
			Assert.AreEqual("    <div class=\"form-group col-lg-10\">", lines[i++], "Linha " + i);
			Assert.AreEqual("      <metalsoft-text-area-field [(text)]=\"model.descricao\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                 [label]=\"'Descrição'\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                 [tip]=\"'Descrição.'\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                 [required]=\"true\"></metalsoft-text-area-field>", lines[i++], "Linha " + i);
			Assert.AreEqual("    </div>", lines[i++], "Linha " + i);
			AssertIsEmptyIgnoresWhiteSpace(lines[i++], "Linha " + i);
			Assert.AreEqual("    <div class=\"form-group col-lg-10\">", lines[i++], "Linha " + i);
			Assert.AreEqual("      <metalsoft-smart-search-field [(model)]=\"model.periodo\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                    [targetService]=\"'PeriodoRisco'\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                    [label]=\"'Período'\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                    [tip]=\"'Período.'\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                    [required]=\"true\"></metalsoft-smart-search-field>", lines[i++], "Linha " + i);
			Assert.AreEqual("    </div>", lines[i++], "Linha " + i);
			AssertIsEmptyIgnoresWhiteSpace(lines[i++], "Linha " + i);
			Assert.AreEqual("    <div class=\"form-group col-lg-10\">", lines[i++], "Linha " + i);
			Assert.AreEqual("      <metalsoft-select-primitive-field [(item)]=\"model.tipo\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                        [items]=\"possibleValuesForTipo\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                        [label]=\"'Tipo'\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                        [tip]=\"'Tipo.'\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                        [required]=\"true\"></metalsoft-select-primitive-field>", lines[i++], "Linha " + i);
			Assert.AreEqual("    </div>", lines[i++], "Linha " + i);
			AssertIsEmptyIgnoresWhiteSpace(lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("    <div class=\"msf-grid-title\">Composições</div>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("    <table class=\"table table-responsive table-striped\">", lines[i++], "Linha " + i);
			Assert.AreEqual("      <thead>", lines[i++], "Linha " + i);
			Assert.AreEqual("        <tr>", lines[i++], "Linha " + i);
			Assert.AreEqual("          <th></th>", lines[i++], "Linha " + i);
			Assert.AreEqual("          <th>Nome</th>", lines[i++], "Linha " + i);
			Assert.AreEqual("          <th>Período</th>", lines[i++], "Linha " + i);
			Assert.AreEqual("          <th>Tipo</th>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("        </tr>", lines[i++], "Linha " + i);
			Assert.AreEqual("      </thead>", lines[i++], "Linha " + i);
			Assert.AreEqual("      <tbody>", lines[i++], "Linha " + i);
			Assert.AreEqual("        <ng-container *ngFor=\"let child of model.composicoes; index as i\">", lines[i++], "Linha " + i);
			Assert.AreEqual("          <tr *ngIf=\"!child.deleteMe\">", lines[i++], "Linha " + i);
			Assert.AreEqual("            <td>", lines[i++], "Linha " + i);
			Assert.AreEqual("              <button class=\"btn msf-toolbar-button\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                      (click)=\"removeFromComposicoes(i)\">", lines[i++], "Linha " + i);
			Assert.AreEqual("                <img src=\"assets/delete_24px.svg\" />", lines[i++], "Linha " + i);
			Assert.AreEqual("              </button>", lines[i++], "Linha " + i);
			Assert.AreEqual("            </td>", lines[i++], "Linha " + i);
			AssertIsEmptyIgnoresWhiteSpace(lines[i++], "Linha " + i);
			Assert.AreEqual("            <td>", lines[i++], "Linha " + i);
			Assert.AreEqual("              <metalsoft-text-field [(text)]=\"child.nomeComposicao\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                    [label]=\"''\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                    [tip]=\"'Nome.'\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                    [required]=\"true\"></metalsoft-text-field>", lines[i++], "Linha " + i);
			Assert.AreEqual("            </td>", lines[i++], "Linha " + i);
			AssertIsEmptyIgnoresWhiteSpace(lines[i++], "Linha " + i);
			Assert.AreEqual("            <td>", lines[i++], "Linha " + i);
			Assert.AreEqual("              <metalsoft-smart-search-field [(model)]=\"child.periodoComposicao\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                            [targetService]=\"'PeriodoRisco'\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                            [label]=\"''\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                            [tip]=\"'Período.'\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                            [required]=\"true\"></metalsoft-smart-search-field>", lines[i++], "Linha " + i);
			Assert.AreEqual("            </td>", lines[i++], "Linha " + i);
			AssertIsEmptyIgnoresWhiteSpace(lines[i++], "Linha " + i);
			Assert.AreEqual("            <td>", lines[i++], "Linha " + i);
			Assert.AreEqual("              <metalsoft-select-primitive-field [(item)]=\"child.tipoComposicao\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                                [items]=\"possibleValuesForTipoComposicao\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                                [label]=\"''\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                                [tip]=\"'Tipo.'\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                                [required]=\"true\"></metalsoft-select-primitive-field>", lines[i++], "Linha " + i);
			Assert.AreEqual("            </td>", lines[i++], "Linha " + i);
			AssertIsEmptyIgnoresWhiteSpace(lines[i++], "Linha " + i);
			Assert.AreEqual("          </tr>", lines[i++], "Linha " + i);
			Assert.AreEqual("        </ng-container>", lines[i++], "Linha " + i);
			Assert.AreEqual("        <tr class=\"bg-dark text-white\">", lines[i++], "Linha " + i);
			Assert.AreEqual("          <td>", lines[i++], "Linha " + i);
			Assert.AreEqual("            <button class=\"btn msf-toolbar-button\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                    (click)=\"addToComposicoes()\">", lines[i++], "Linha " + i);
			Assert.AreEqual("              <img src=\"assets/add_24px.svg\">", lines[i++], "Linha " + i);
			Assert.AreEqual("            </button>", lines[i++], "Linha " + i);
			Assert.AreEqual("          </td>", lines[i++], "Linha " + i);
			Assert.AreEqual("          <td colspan=\"2\">{{model.composicoes.length}} itens.</td>", lines[i++], "Linha " + i);
			Assert.AreEqual("        </tr>", lines[i++], "Linha " + i);
			Assert.AreEqual("      </tbody>", lines[i++], "Linha " + i);
			Assert.AreEqual("    </table>", lines[i++], "Linha " + i);

			i++; //Assert.AreEqual("  </form>", lines[i++], "Linha " + i); //TODO da uma diferença...
			i++; //Assert.AreEqual("</div>", lines[i++], "Linha " + i); //TODO da uma diferença...
		}

		//TODO angular entry css

		[TestMethod]
		public void TestAngularListComponentCodeGenerator()
		{
			var entity = BuildEntityCategoriaRisco();
			var g = new AngularListComponentCodeGenerator() { BaseEntity = entity, Template = GetTemplate(@"angular\list-component.template") };

			var code = g.GenerateCode();
			var lines = GetLines(code);

			int i = 0;

			Assert.AreEqual("import { Component, OnInit } from '@angular/core';", lines[i++], "Linha " + i);
			Assert.AreEqual("import { Router } from '@angular/router';", lines[i++], "Linha " + i);
			Assert.AreEqual("import { Observable } from 'rxjs/Observable';", lines[i++], "Linha " + i);
			Assert.AreEqual("import { Subject } from 'rxjs/Subject';", lines[i++], "Linha " + i);
			Assert.AreEqual("import { Subscription } from 'rxjs/Subscription';", lines[i++], "Linha " + i);
			Assert.AreEqual("import 'rxjs/add/operator/debounceTime';", lines[i++], "Linha " + i);
			Assert.AreEqual("import 'rxjs/add/operator/distinctUntilChanged';", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("import { AlertService } from '../../metalsoft-core/local-services/alert.service';", lines[i++], "Linha " + i);
			Assert.AreEqual("import { CategoriasRiscos } from '../../model/categorias-riscos.model';", lines[i++], "Linha " + i);
			Assert.AreEqual("import { CategoriaRiscoService } from '../../services/categoria-risco.service';", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("@Component({", lines[i++], "Linha " + i);
			Assert.AreEqual("  selector: 'categorias-riscos-list',", lines[i++], "Linha " + i);
			Assert.AreEqual("  templateUrl: './categorias-riscos-list.component.html',", lines[i++], "Linha " + i);
			Assert.AreEqual("  styleUrls: ['./categorias-riscos-list.component.css'],", lines[i++], "Linha " + i);
			Assert.AreEqual("  providers: [CategoriaRiscoService]", lines[i++], "Linha " + i);
			Assert.AreEqual("})", lines[i++], "Linha " + i);
			Assert.AreEqual("export class CategoriasRiscosListComponent implements OnInit {", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  public model = new CategoriasRiscos();", lines[i++], "Linha " + i);
			Assert.AreEqual("  public showFilters = true;", lines[i++], "Linha " + i);
			Assert.AreEqual("  public searchAsYouTypeSubject = new Subject();", lines[i++], "Linha " + i);
			Assert.AreEqual("  public searchAsYouTypeObservable = new Subscription(); //new Observable<string>();", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  constructor(", lines[i++], "Linha " + i);
			Assert.AreEqual("    private service: CategoriaRiscoService,", lines[i++], "Linha " + i);
			Assert.AreEqual("    private alertService: AlertService,", lines[i++], "Linha " + i);
			Assert.AreEqual("    private router: Router) { }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  ngOnInit() {", lines[i++], "Linha " + i);
			Assert.AreEqual("    this.search();", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("    this.searchAsYouTypeObservable = this.searchAsYouTypeSubject", lines[i++], "Linha " + i);
			Assert.AreEqual("      .debounceTime(300)", lines[i++], "Linha " + i);
			Assert.AreEqual("      .subscribe(() => {", lines[i++], "Linha " + i);
			Assert.AreEqual("        this.search();", lines[i++], "Linha " + i);
			Assert.AreEqual("      })", lines[i++], "Linha " + i);
			Assert.AreEqual("  }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  searchAsYouType(): void {", lines[i++], "Linha " + i);
			Assert.AreEqual("    this.searchAsYouTypeSubject.next();", lines[i++], "Linha " + i);
			Assert.AreEqual("  }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  search(): void", lines[i++], "Linha " + i);
			Assert.AreEqual("  {", lines[i++], "Linha " + i);
			Assert.AreEqual("    this.service.search(this.model)", lines[i++], "Linha " + i);
			Assert.AreEqual("      .subscribe(dto => {", lines[i++], "Linha " + i);
			Assert.AreEqual("      if (!dto.response.hasException) {", lines[i++], "Linha " + i);
			Assert.AreEqual("        this.model.items = dto.items; //refresh", lines[i++], "Linha " + i);
			Assert.AreEqual("      }", lines[i++], "Linha " + i);
			Assert.AreEqual("    });", lines[i++], "Linha " + i);
			Assert.AreEqual("  }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  createNew(): void", lines[i++], "Linha " + i);
			Assert.AreEqual("  {", lines[i++], "Linha " + i);
			Assert.AreEqual("    this.router.navigateByUrl('/categoria-risco');", lines[i++], "Linha " + i);
			Assert.AreEqual("  }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  expand(id: number): void", lines[i++], "Linha " + i);
			Assert.AreEqual("  {", lines[i++], "Linha " + i);
			Assert.AreEqual("    this.router.navigateByUrl(`/categoria-risco/${id}`);", lines[i++], "Linha " + i);
			Assert.AreEqual("  }", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("}", lines[i++], "Linha " + i);
			//TODO enumeração (Tipo)
		}

		[TestMethod]
		public void TestAngularListComponentTemplateCodeGenerator()
		{
			var entity = BuildEntityCategoriaRisco();
			entity.Properties.Add(new Property(entity) { Name = "Data", Type = "DateTime", Required = true, Label="Data", Tip="Data." });
			var g = new AngularListComponentTemplateCodeGenerator() { BaseEntity = entity, Template = GetTemplate(@"angular\list-component-template.template") };

			var code = g.GenerateCode();
			var lines = GetLines(code);

			int i = 0;

			Assert.AreEqual("<div>", lines[i++], "Linha " + i);
			Assert.AreEqual("  <div>", lines[i++], "Linha " + i);
			Assert.AreEqual("      <button class=\"btn msf-toolbar-button\" title=\"Novo\"", lines[i++], "Linha " + i);
			Assert.AreEqual("              (click)=\"createNew()\">", lines[i++], "Linha " + i);
			Assert.AreEqual("        <img src=\"../../../assets/new_24px.svg\">", lines[i++], "Linha " + i);
			Assert.AreEqual("      </button>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("      <button class=\"btn msf-toolbar-button\" title=\"Pesquisar\"", lines[i++], "Linha " + i);
			Assert.AreEqual("              (click)=\"search()\">", lines[i++], "Linha " + i);
			Assert.AreEqual("        <img src=\"../../../assets/search_24px.svg\">", lines[i++], "Linha " + i);
			Assert.AreEqual("      </button>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("      <button class=\"btn msf-toolbar-button\" title=\"{{model|json}}\">", lines[i++], "Linha " + i);
			Assert.AreEqual("        <img src=\"../../../assets/bug_24px.svg\">", lines[i++], "Linha " + i);
			Assert.AreEqual("      </button>", lines[i++], "Linha " + i);
			Assert.AreEqual("  </div>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  <div class=\"msf-form-title\">Pesquisa de Categorias de Riscos</div>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("  <form class=\"msf-form\" #thisForm=\"ngForm\">", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("    <div class=\"form-group col-lg-10\">", lines[i++], "Linha " + i);
			Assert.AreEqual("      <metalsoft-text-field [(text)]=\"model.filterCodigo\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                            (textChange)=\"searchAsYouType()\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                            [label]=\"'Código'\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                            [tip]=\"'Código mnemônico.'\"></metalsoft-text-field>", lines[i++], "Linha " + i);
			Assert.AreEqual("    </div>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("    <div class=\"form-group col-lg-10\">", lines[i++], "Linha " + i);
			Assert.AreEqual("      <metalsoft-smart-search-field [(model)]=\"model.filterPeriodo\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                    (modelChange)=\"searchAsYouType()\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                    [targetService]=\"'PeriodoRisco'\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                    [label]=\"'Período'\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                    [tip]=\"'Período.'\"></metalsoft-smart-search-field>", lines[i++], "Linha " + i);
			Assert.AreEqual("    </div>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("    <div class=\"form-group col-lg-10\">", lines[i++], "Linha " + i);
			Assert.AreEqual("      <metalsoft-select-primitive-field [(item)]=\"model.filterTipo\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                        (itemChange)=\"searchAsYouType()\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                        [items]=\"possibleValuesForTipo\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                        [label]=\"'Tipo'\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                                        [tip]=\"'Tipo.'\"></metalsoft-select-primitive-field>", lines[i++], "Linha " + i);
			Assert.AreEqual("    </div>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("    <div class=\"form-group col-lg-10\">", lines[i++], "Linha " + i);
			Assert.AreEqual("      <metalsoft-date-field [(date)]=\"model.filterData\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                            (dateChange)=\"searchAsYouType()\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                            [label]=\"'Data'\"", lines[i++], "Linha " + i);
			Assert.AreEqual("                            [tip]=\"'Data.'\"></metalsoft-date-field>", lines[i++], "Linha " + i);
			Assert.AreEqual("    </div>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			i++; //Assert.AreEqual("</form>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("    <table class=\"table table-responsive table-striped\">", lines[i++], "Linha " + i);
			Assert.AreEqual("      <thead>", lines[i++], "Linha " + i);
			Assert.AreEqual("        <tr>", lines[i++], "Linha " + i);
			Assert.AreEqual("          <th scope=\"col\"></th>", lines[i++], "Linha " + i);
			Assert.AreEqual("          <th scope=\"col\">Código</th>", lines[i++], "Linha " + i);
			Assert.AreEqual("          <th scope=\"col\">Descrição</th>", lines[i++], "Linha " + i);
			Assert.AreEqual("          <th scope=\"col\">Período</th>", lines[i++], "Linha " + i);
			Assert.AreEqual("          <th scope=\"col\">Tipo</th>", lines[i++], "Linha " + i);
			Assert.AreEqual("          <th scope=\"col\">Data</th>", lines[i++], "Linha " + i);
			Assert.AreEqual("        </tr>", lines[i++], "Linha " + i);
			Assert.AreEqual("      </thead>", lines[i++], "Linha " + i);
			Assert.AreEqual("      <tbody>", lines[i++], "Linha " + i);
			Assert.AreEqual("        <tr *ngFor=\"let i of model.items\">", lines[i++], "Linha " + i);
			Assert.AreEqual("          <td><button class=\"btn msf-toolbar-button\" (click)=\"expand(i.autoId)\"><img src=\"../../..//assets/search_24px.svg\" /></button></td>", lines[i++], "Linha " + i);
			Assert.AreEqual("          <td>{{i.codigo}}</td>", lines[i++], "Linha " + i);
			Assert.AreEqual("          <td>{{i.descricao}}</td>", lines[i++], "Linha " + i);
			Assert.AreEqual("          <td>{{i.periodo?.presentation}}</td>", lines[i++], "Linha " + i);
			Assert.AreEqual("          <td>{{i.tipo}}</td>", lines[i++], "Linha " + i);
			Assert.AreEqual("          <td>{{i.data | metalsoftDateFormat}}</td>", lines[i++], "Linha " + i);
			Assert.AreEqual("        </tr>", lines[i++], "Linha " + i);
			Assert.AreEqual("      </tbody>", lines[i++], "Linha " + i);
			Assert.AreEqual("    </table>", lines[i++], "Linha " + i);
			Assert.AreEqual("", lines[i++], "Linha " + i);
			Assert.AreEqual("</div>", lines[i++], "Linha " + i);
			//TODO enumeração (Tipo)
		}

		//TODO angular list css

		//TODO angular app module
		//	import { CriterioEntryComponent } from './components/criterio-entry/criterio-entry.component';
		//	import { CriteriosListComponent } from './components/criterios-list/criterios-list.component';
		//	@NgModule({ declarations: [CriterioEntryComponent, CriteriosListComponent

		//TODO angular routing module
		//	import { CriterioEntryComponent } from './components/criterio-entry/criterio-entry.component';
		//	import { CriteriosListComponent } from './components/criterios-list/criterios-list.component';
		//	const appRoutes: Routes = [
		//		{ path: 'criterio', component: CriterioEntryComponent, canActivate: [AuthGuard] },
		//		{ path: 'criterio/:id', component: CriterioEntryComponent, canActivate: [AuthGuard] },
		//		{ path: 'criterios', component: CriteriosListComponent, canActivate: [AuthGuard] },

		[TestMethod]
		public void TestIsEntityReference()
		{
			Property p = new Property(new Entity());
			p.Type = "string";
			Assert.IsFalse(p.IsEntityReference);
			p.Type = "int";
			Assert.IsFalse(p.IsEntityReference);
			p.Type = "DateTime";
			Assert.IsFalse(p.IsEntityReference);
			p.Type = "Asdf";
			Assert.IsTrue(p.IsEntityReference);
			p.Type = "List<Asdf>";
			Assert.IsFalse(p.IsEntityReference);
		}

		[TestMethod]
		public void TestIsCollection()
		{
			Property p = new Property(new Entity());
			p.Type = "string";
			Assert.IsFalse(p.IsCollection);
			p.Type = "int";
			Assert.IsFalse(p.IsCollection);
			p.Type = "DateTime";
			Assert.IsFalse(p.IsCollection);
			p.Type = "Asdf";
			Assert.IsFalse(p.IsCollection);
			p.Type = "IList<Asdf>";
			Assert.IsTrue(p.IsCollection);
		}

		private Entity BuildEntityDisciplina()
		{
			var entity = new Entity();
			entity.Application = new Application() { Name = "Riscos" };
			entity.EntityName = "Disciplina";
			entity.EntityLabel = "Disciplina";
			entity.CollectionName = "Disciplinas";
			entity.CollectionLabel = "Disciplinas";
			entity.Properties.Add(new Property(entity) { Name = "Codigo", Type = "string", Required = true, CriterionForEquals = true });
			entity.Properties.Add(new Property(entity) { Name = "Nome", Type = "string", Required = true });
			entity.Properties.Add(new Property(entity) { Name = "Periodo", Type = "PeriodoRisco", Required = true });
			return entity;
		}

		private Entity BuildEntityCategoriaRisco()
		{
			var composition = new Entity();
			composition.EntityName = "ComposicaoCategoria";
			composition.EntityLabel = "Composição";
			composition.IsComposition = true;
			composition.Properties.Add(new Property(composition) { Name = "CategoriaComposta", Type = "CategoriaRisco", Label = "Categoria Composta", Tip = "Categoria composta.", Required = true, IsContainer = true });
			composition.Properties.Add(new Property(composition) { Name = "NomeComposicao", Type = "string", Label = "Nome", Tip = "Nome.", Required = true });
			composition.Properties.Add(new Property(composition) { Name = "PeriodoComposicao", Type = "PeriodoRisco", Label = "Período", Tip = "Período.", Required = true });
			composition.Properties.Add(new Property(composition) { Name = "TipoComposicao", Type = "Tipo", Label = "Tipo", Tip = "Tipo.", Required = true, IsEnum = true });

			var entity = new Entity();
			entity.Application = new Application() { Name = "Riscos" };
			entity.EntityName = "CategoriaRisco";
			entity.EntityLabel = "Categoria de Risco";
			entity.CollectionName = "CategoriasRiscos";
			entity.CollectionLabel = "Categorias de Riscos";
			entity.Properties.Add(new Property(entity) { Name = "Codigo", Type = "string", Label="Código", Tip="Código mnemônico.", Required = true, CriterionForEquals = true });
			entity.Properties.Add(new Property(entity) { Name = "Descricao", Type = "StringClob", Label = "Descrição", Tip = "Descrição.", Required = true });
			entity.Properties.Add(new Property(entity) { Name = "Periodo", Type = "PeriodoRisco", Label = "Período", Tip = "Período.", Required = true });
			entity.Properties.Add(new Property(entity) { Name = "Tipo", Type = "Tipo", Label = "Tipo", Tip = "Tipo.", Required = true, IsEnum = true });
			entity.Properties.Add(new Property(entity) { Name = "Composicoes", Type = "IList<ComposicaoCategoria>",
				Label = "Composições", Tip = "Composições.", Required = false,
				ForeignKeyFieldName = "CategoriaComposta",
				CompositionInfo = composition });
			return entity;
		}

		private string GetTemplate(string fileName)
		{
			//TODO rever
			string dir = @"C:\projetos2\metalsoft-core\CodeGenerator\Templates";
			return System.IO.File.ReadAllText(System.IO.Path.Combine(dir, fileName));
		}

		private string[] GetLines(string code)
		{
			return code.Split(System.Environment.NewLine);
		}

		private void AssertIsEmptyIgnoresWhiteSpace(string line, string msg)
		{
			Assert.AreEqual(string.Empty, line.Replace(" ", string.Empty), msg);
		}
	}
}
