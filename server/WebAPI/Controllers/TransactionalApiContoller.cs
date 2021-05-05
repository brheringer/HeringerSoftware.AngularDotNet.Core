using HeringerSoftware.AngularDotNet.Core.DataTransferObjects;
using HeringerSoftware.AngularDotNet.Core.Model;
using HeringerSoftware.AngularDotNet.Core.Persistence;
using HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence;
using HeringerSoftware.AngularDotNet.Core.WebAPI.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Controllers
{
	[Authorize]
	public abstract class TransactionalApiContoller
		: ControllerBase
	{
		protected AppSettings AppSettings { get; private set; }
		protected DbContext AppDbContext { get; private set; }
		protected ILogger<TransactionalApiContoller> Logger { get; private set; }

		protected EntityWrapper EntityWrapperInstance
		{
			get
			{
				if (this._entityWrapper == null)
					this._entityWrapper = new EntityWrapper(this.AppDbContext);
				return this._entityWrapper;
			}
		}
		private EntityWrapper _entityWrapper = null;

		public TransactionalApiContoller(IOptions<AppSettings> appSettings, DbContext dbContext, ILogger<TransactionalApiContoller> logger)
		{
			this.AppSettings = appSettings.Value;
			this.AppDbContext = dbContext;
			this.Logger = logger;
		}

		protected ResultType InvokeCommandInsideTransaction<ResultType>(Func<ResultType> command)
			where ResultType : class, ResponseEnvelop, new()
		{
			try
			{
				using (var transaction = this.AppDbContext.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
				{
					try
					{
						ResultType resultado = command();
						transaction.Commit();
						return resultado;
					}
					catch(Exception ex)
					{
						LogException(ex);
						transaction.Rollback();
						throw;
					}
				}
			}
			catch (Exception ex)
			{
				return RespondWithException<ResultType>(ex);
			}
		}

		protected ResultType InvokeCommandWithoutTransaction<ResultType>(Func<ResultType> command)
			where ResultType : class, ResponseEnvelop, new()
		{
			try
			{
				try
				{
					ResultType resultado = command();
					return resultado;
				}
				catch (Exception ex)
				{
					LogException(ex);
					throw;
				}
			}
			catch (Exception ex)
			{
				return RespondWithException<ResultType>(ex);
			}
		}

		private void LogException(Exception ex)
		{
			Logger.Log<object>(LogLevel.Error, 1, null, ex, (obj, e) => e?.ToString());
		}

		private ResultType RespondWithException<ResultType>(Exception ex) where ResultType : class, ResponseEnvelop, new()
		{
			var dto = new ResultType();
			dto.Response.Exception = this.AppSettings.ShowFullException
				? ex.ToString()
				: GetDeepDescription(ex);
			return dto;
		}

		private string GetDeepDescription(Exception ex)
		{
			string deepDescription = string.Empty;
			if (ex != null)
			{
				if (ex.InnerException != null)
					deepDescription = GetDeepDescription(ex.InnerException);
				deepDescription += ex.Message + System.Environment.NewLine;
			}
			return deepDescription;
		}

		protected T SolveProxy<T>(Repository<T> dao, EntityReferenceDto proxy)
			where T : Entity
		{
			return SolveProxy(dao, proxy, proxy?.Id);
		}

		protected T SolveProxy<T>(Repository<T> dao, T proxy)
			where T : Entity
		{
			return SolveProxy(dao, proxy, proxy?.Id);
		}

		private T SolveProxy<T>(Repository<T> dao, object proxy, int? id)
			where T : Entity
		{
			T entity = default(T);
			if (proxy != null && id > 0)
				entity = dao.Load(id.Value);
			return entity;
		}

		protected string GetUserId()
		{
			var subClaim = ((System.Security.Claims.ClaimsIdentity)User.Identity).Claims.FirstOrDefault(c => c.Type == "sub");
			return subClaim != null
				? subClaim.Value
				: string.Empty;
		}
	}
}