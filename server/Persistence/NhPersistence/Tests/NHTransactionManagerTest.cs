using MetalSoft.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MetalSoft.Core.Persistence.NhPersistence.Tests
{
	[TestClass]
	public class NHTransactionManagerTest
		: BaseTest
	{
		//[TestMethod]
		//public void TestCommit()
		//{
		//	User user1 = CreateUser1();

		//	TransactionMngrInstance.BeginTransaction();
		//	try
		//	{
		//		TransactionMngrInstance.Factory.UserDAO.Update(user1); 
		//		TransactionMngrInstance.Commit();
		//	}
		//	catch
		//	{
		//		TransactionMngrInstance.Rollback();
		//		Assert.Fail("Should not rollback.");
		//	}

		//	ForceRestartOfNHSession();

		//	var user = TransactionMngrInstance.Factory.UserDAO.Load(1);
		//	Assert.AreEqual(1, user.AutoId, "Object should be persisted.");
		//}

		//[TestMethod]
		//public void TestRollback()
		//{
		//	User user1 = CreateUser1();
		//	User user2 = CreateUser2();
		//	user2.RealName = null;

		//	TransactionMngrInstance.BeginTransaction();
		//	try
		//	{
		//		TransactionMngrInstance.Factory.UserDAO.Update(user1); //ok
		//		TransactionMngrInstance.Factory.UserDAO.Update(user2); //RequiredPropertyException
		//		TransactionMngrInstance.Commit();
		//		Assert.Fail("The intent of the test is a failure in the second update.");
		//	}
		//	catch
		//	{
		//		TransactionMngrInstance.Rollback();
		//	}

		//	ForceRestartOfNHSession();

		//	var users = TransactionMngrInstance.Factory.UserDAO.Search(null, null, null, 0, 0);
		//	Assert.AreEqual(0, users.Count, "Object should not be persisted because of the rollback.");
		//}

		//private User CreateUser1()
		//{
		//	User user = new User();
		//	user.Banished = false;
		//	user.Email = "user1@mailinator.com";
		//	user.HashedPassword = "123456";
		//	user.RealName = "User One";
		//	user.Salt = "fakesalt";
		//	user.UserName = "asdf";
		//	return user;
		//}

		//private User CreateUser2()
		//{
		//	User user = new User();
		//	user.Banished = false;
		//	user.Email = "user2@mailinator.com";
		//	user.HashedPassword = "123456";
		//	user.RealName = "User Two";
		//	user.Salt = "fakesalt";
		//	user.UserName = "asdf";
		//	return user;
		//}

		//TODO concorrencia
		//TODO data e hora e usuario de criacao e update
	}
}
