using HeringerSoftware.AngularDotNet.Core.DataTransferObjects;
using HeringerSoftware.AngularDotNet.Core.Persistence;
using HeringerSoftware.AngularDotNet.Core.Persistence.EFPersistence;
using HeringerSoftware.AngularDotNet.Core.Model;
using HeringerSoftware.AngularDotNet.Core.WebAPI.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace HeringerSoftware.AngularDotNet.Core.WebAPI.Controllers
{
    public abstract class GenericController<TEntityDTO, TEntity> : TransactionalApiContoller
        where TEntityDTO : EntityDto, new()
        where TEntity : Entity, new()
    {
        public GenericController(IOptions<AppSettings> appSettings, DbContext dbContext, ILogger<TransactionalApiContoller> logger)
            : base(appSettings, dbContext, logger)
        {
        }

        [HttpGet]
		[Route("{id}")]
		public virtual IActionResult Get(int id)
        {
            return Ok(InvokeCommandInsideTransaction(() => GetInsideTransaction(id)));
		}

        private TEntityDTO GetInsideTransaction(int id)
        {
            var dao = GetDAO();
			var entidade = dao.LoadWithCompositions(id);
            return Wrap(entidade);
        }

        [HttpGet]
        [Route("all")]
        public virtual IActionResult GetAll()
        {
			//this.AppDbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; //deu pau no 3.1
            return Ok(InvokeCommandInsideTransaction(() => GetAllInsideTransaction()));
		}

        private EntitiesReferencesDto GetAllInsideTransaction()
        {
            var dao = GetDAO();
            var entidades = dao.LoadAll();
            return EntityWrapperInstance.WrapToReferences(entidades);
        }

		[HttpPost]
        public virtual IActionResult Update([FromBody]TEntityDTO dto)
        {
            return Ok(InvokeCommandInsideTransaction(() => UpdateInsideTransaction(dto)));
        }

        private TEntityDTO UpdateInsideTransaction(TEntityDTO dto)
		{
			if (dto == null)
				throw new ArgumentNullException(nameof(dto));
			var dao = GetDAO();

			TEntity entity;
			if (dto.Id > 0)
			{
				entity = dao.LoadWithCompositions(dto.Id);
			}
			else
			{
				entity = new TEntity();
				this.AppDbContext.Attach(entity);
			}
			WrapToUpdate(dto, entity);
			entity.Validate();
			entity = dao.Update(entity);

			this.AppDbContext.SaveChanges();

            //necessário pq os proxies não são resolvidos após serem atachados manualmente (ver EntityWrapper.CreateProxy)
            this.AppDbContext.ChangeTracker.Clear();
            entity = GetDAO().Load(entity.Id);

            AfterUpdate(entity);

            return Wrap(entity);
		}

		protected virtual void AfterUpdate(TEntity entity)
		{
		}

		[HttpDelete]
		[Route("{id}")]
        public virtual TEntityDTO Delete(int id)
        {
            return InvokeCommandInsideTransaction(() => DeleteInsideTransaction(id));
        }

        private TEntityDTO DeleteInsideTransaction(int id)
        {
            var dao = GetDAO();
            var entity = dao.Load(id);
            dao.Delete(entity);
			this.AppDbContext.SaveChanges();
            return new TEntityDTO();
		}

        protected virtual void WrapToUpdate(TEntityDTO fromDto, TEntity toEntity)
        {
            EntityWrapperInstance.CopyInto(fromDto, toEntity);
        }

        protected virtual TEntityDTO Wrap(TEntity e)
        {
            return EntityWrapperInstance.Wrap<TEntityDTO>(e);
        }

		protected abstract Repository<TEntity> GetDAO();
	}
}
