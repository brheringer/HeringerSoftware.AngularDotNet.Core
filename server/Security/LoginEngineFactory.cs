using System;
using System.Collections.Generic;

namespace MetalSoft.Core.Security
{
    public interface LoginEngineFactory
    {
		string Secret { get; set; }
		Dictionary<string, string> Parameters { get; set; }

		LoginEngine Create();
    }
}
