using System;
using System.Collections.Generic;

namespace HeringerSoftware.AngularDotNet.Core.DataTransferObjects
{
	public class Response
	{
		public bool HasException
		{
			get { return !string.IsNullOrEmpty(this.Exception); }
		}

		public string Exception { get; set; }

		public Response()
		{
		}
	}
}
