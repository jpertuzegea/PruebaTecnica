//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Enero 2021</date>
//-----------------------------------------------------------------------
 
namespace Proyecto_DAO
{
    using Proyecto_DAO.Interfaces;
    using Proyecto_DAO.Repositories;
    using System; 

    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue;
        private readonly ContextDB PagaSoftContextDB;

        public UnitOfWork(ContextDB _PagaSoftContextDB)
        {
            this.PagaSoftContextDB = _PagaSoftContextDB;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new Repository<TEntity>(PagaSoftContextDB);
        }

        public bool SaveChanges()
        {
            return PagaSoftContextDB.SaveChanges() > 0;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    PagaSoftContextDB.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
