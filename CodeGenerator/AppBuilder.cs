using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MetalSoft.Core.CodeGenerator.CodeGenerators;
using MetalSoft.Core.CodeGenerator.CodeGenerators.Angular;
using MetalSoft.Core.CodeGenerator.Model;

namespace MetalSoft.Core.CodeGenerator
{
    public class AppBuilder
    {
		public string OutputBaseDir { get; set; }

		public string TemplatesDir { get; set; }

		private List<Entity> Entities { get; set; }

		private Dictionary<string, string> Paths { get; set; }

		public AppBuilder()
		{
			this.Entities = new List<Entity>();
			this.Paths = new Dictionary<string, string>();
			this.TemplatesDir = "./Templates";
		}

		public void AddEntity(Entity entity)
		{
			this.Entities.Add(entity);
		}

		public void Build()
		{
			GenerateFiles();
		}

		private void GenerateFiles()
		{
			foreach (Entity e in this.Entities)
				GenerateFiles(e);
		}

		private void GenerateFiles(Entity entity)
		{
			Write(
				new ModelCodeGenerator() { BaseEntity = entity, Template = GetTemplate("Entity.template") },
				new string[] { "Server", "Model" });

			string template = entity.IsComposition ? "CompositionDao.template" : "EntityDao.template";
			Write(
				new EntityDaoCodeGenerator() { BaseEntity = entity, Template = GetTemplate(template)  },
				new string[] { "Server", "Persistence" });

			Write(
				new ApplicationDaoFactoryCodeGenerator() { BaseEntity = entity, Template = GetTemplate("ApplicationDAOFactory.template")  },
				new string[] { "Server", "Persistence" });

			template = entity.IsComposition ? "NhCompositionDao.template" : "NhEntityDao.template";
			Write(
				new NhEntityDaoCodeGenerator() { BaseEntity = entity, Template = GetTemplate(template)  },
				new string[] { "Server", "Persistence", "NhPersistence" });

			Write(
				new NhApplicationDaoFactoryCodeGenerator() { BaseEntity = entity, Template = GetTemplate("NhApplicationDAOFactory.template")  },
				new string[] { "Server", "Persistence", "NhPersistence" });

			Write(
				new MappingFileCodeGenerator() { BaseEntity = entity, Template = GetTemplate("Mapping.template")  },
				new string[] { "Server", "Persistence", "NhPersistence", "Mapping" });

			Write(
				new DtoCodeGenerator() { BaseEntity = entity, Template = GetTemplate("EntityDto.template")  },
				new string[] { "Common", "DataTransferObjects" });

			Write(
				new AngularEntryModelCodeGenerator() { BaseEntity = entity, Template = GetTemplate("angular/entry-model.template") },
				new string[] { "Client", "app", "model" });

			if (!entity.IsComposition)
			{
				Write(
					new CollectionDtoCodeGenerator() { BaseEntity = entity, Template = GetTemplate("CollectionDto.template") },
					new string[] { "Common", "DataTransferObjects" });

				Write(
					new EntityControllerCodeGenerator() { BaseEntity = entity, Template = GetTemplate("FullController.template") },
					new string[] { "Server", "WebAPI", "Controllers" });

				Write(
					new AngularServiceCodeGenerator() { BaseEntity = entity, Template = GetTemplate("angular/service.template") },
					new string[] { "Client", "app", "services" });

				Write(
					new AngularListModelCodeGenerator() { BaseEntity = entity, Template = GetTemplate("angular/list-model.template") },
					new string[] { "Client", "app", "model" });

				string angularEntrySubPath = $"{AngularNormalizer.NormalizeFileName(entity.EntityName)}-entry";
				Write(
					new AngularEntryComponentCodeGenerator() { BaseEntity = entity, Template = GetTemplate("angular/entry-component.template") },
					new string[] { "Client", "app", "components", angularEntrySubPath });

				Write(
					new AngularEntryComponentTemplateCodeGenerator() { BaseEntity = entity, Template = GetTemplate("angular/entry-component-template.template") },
					new string[] { "Client", "app", "components", angularEntrySubPath });

				Write(
					new AngularEntryComponentCssCodeGenerator() { BaseEntity = entity, Template = GetTemplate("angular/entry-component-css.template") },
					new string[] { "Client", "app", "components", angularEntrySubPath });

				string angularListSubPath = $"{AngularNormalizer.NormalizeFileName(entity.CollectionName)}-list";
				Write(
					new AngularListComponentCodeGenerator() { BaseEntity = entity, Template = GetTemplate("angular/list-component.template") },
					new string[] { "Client", "app", "components", angularListSubPath });

				Write(
					new AngularListComponentTemplateCodeGenerator() { BaseEntity = entity, Template = GetTemplate("angular/list-component-template.template") },
					new string[] { "Client", "app", "components", angularListSubPath });

				Write(
					new AngularListComponentCssCodeGenerator() { BaseEntity = entity, Template = GetTemplate("angular/list-component-css.template") },
					new string[] { "Client", "app", "components", angularListSubPath });
			}
		}

		private void Write(CodeGenerators.CodeGenerator g, string[] pathSubTree)
		{
			string fullPath = this.OutputBaseDir;
			foreach(string sub in pathSubTree)
			{
				fullPath = Path.Combine(fullPath, sub);
				if (!Directory.Exists(fullPath))
					Directory.CreateDirectory(fullPath);
			}
			fullPath = Path.Combine(fullPath, g.GetFileName());
			File.WriteAllText(fullPath, g.GenerateCode(), Encoding.UTF8);
		}

		private string GetTemplate(string fileName)
		{
			//string dir = @"C:\projetos2\metalsoft-core\CodeGenerator\Templates";
			return File.ReadAllText(System.IO.Path.Combine(this.TemplatesDir, fileName));
		}
	}
}
