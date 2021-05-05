using System;

namespace HeringerSoftware.AngularDotNet.Core.DataTransferObjects
{
	public class EntityDto 
		: DataTransferObject
	{
		public int Id { get; set; }
		public string Presentation { get; set; }
		public int Version { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public string CreationUser { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
		public string LastUpdateUser { get; set; }
		public virtual bool DeleteMe { get; set; }
	}
}