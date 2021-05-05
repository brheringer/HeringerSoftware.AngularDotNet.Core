using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace MetalSoft.Core.Persistence.NhPersistence
{
	public class NHBootStrapper
	{
		private Configuration NhConfigurationInstance;

		public Configuration NhConfiguration
		{
			get
			{
				if (string.IsNullOrEmpty(ConfigurationFileName))
					throw new ArgumentException("ConfigurationFileName property was blank");
				NhConfigurationInstance = new Configuration();
				NhConfigurationInstance.Configure(ConfigurationFileName);
				return NhConfigurationInstance;
			}
		}

		/// <remarks>
		/// this is not intended to be used by eDirectory production applications, 
		/// it is there to help in re-generating the eDirectory database schema 
		/// by auxiliary applications like tests or a console application 
		/// responsible for the creation of the database schema.
		/// </remarks>
		private SchemaExport SchemaExportInstance;

		public SchemaExport SchemaExport
		{
			get
			{
				if (SchemaExportInstance != null)
					return SchemaExportInstance;
				SchemaExportInstance = new SchemaExport(NhConfiguration);
				return SchemaExportInstance;
			}
		}

		public string ConfigurationFileName { get; set; }
	}
}
