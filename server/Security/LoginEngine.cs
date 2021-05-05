using System;
using MetalSoft.Core.Model;

namespace MetalSoft.Core.Security
{
	public interface LoginEngine
	{
		UserSession DoLogoff();
		UserSession DoLogin(User user, string password);
		void SetPasswordBySideEffect(User user, string newPassword);
		void ChangePasswordBySideEffect(User user, string oldPassword, string newPassword);
	}
}
