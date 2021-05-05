using HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Tests.Mocks
{
	public class MockUserResolver : UserResolver
	{
		public string GetUserName() => "bob";
	}
}
