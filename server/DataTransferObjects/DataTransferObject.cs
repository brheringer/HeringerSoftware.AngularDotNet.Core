using System;

namespace HeringerSoftware.AngularDotNet.Core.DataTransferObjects
{
	public class DataTransferObject
		: ResponseEnvelop
	{
		private readonly Response _resposta = new Response();

		public Response Response
		{
			get { return _resposta; }
		}
	}
}
