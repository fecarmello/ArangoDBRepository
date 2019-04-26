using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace App.ArangoDB.Repository
{
    using App.ArangoDB.Context;
    using global::ArangoDB.Client;

    public class RepositoryBase<T> where T : class
    {
        private readonly ContextBase _contextBase;
        private readonly ArangoDatabase db;
        private readonly IDocumentCollection<T> _document;

        public RepositoryBase(ContextBase contextBase)
        {
            _contextBase = contextBase;

            var networkCredential = new NetworkCredential(_contextBase.User, _contextBase.Password);

            var settings = new DatabaseSharedSetting
            {
                Url = _contextBase.Url,
                Database = _contextBase.DatabaseName,
                Credential = networkCredential,
                SystemDatabaseCredential = networkCredential,
            };

            db = new ArangoDatabase(settings);
            _document = db.Collection<T>();
        }

        public Task InsertAsync(T obj)
        {
            return Task.Run(async () =>
            {
                await _document.InsertAsync(obj);
            });
        }

        public Task UpdateAsync(T obj)
        {
            return Task.Run(async () => await db.UpdateAsync<T>(obj));
        }

        public Task<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> func)
        {
            return Task.Run(() => db.Query<T>().Where(func).FirstOrDefault());
        }

        public Task<List<T>> GetAllAsync()
        {
            return Task.Run(async () => await _document.All().ToListAsync());
        }

        public Task RemoveAsync(T obj)
        {
            return Task.Run(async () => await _document.RemoveAsync(obj));
        }

        public Task InsertMultipleAsync(List<T> objList)
        {
            return Task.Run(async () => await _document.InsertMultipleAsync(objList));
        }
    }
}