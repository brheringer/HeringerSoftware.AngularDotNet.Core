using System;

namespace MetalSoft.Core.Security
{
	public class MetalSoftSecurityException : Exception
	{
		public MetalSoftSecurityException() { }
		public MetalSoftSecurityException(string message) : base(message) { }
		public MetalSoftSecurityException(string message, Exception inner) : base(message, inner) { }
	}
}
