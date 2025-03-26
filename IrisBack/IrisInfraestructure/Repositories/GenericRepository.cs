using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IrisCore.DTOs;
using IrisCore.InterfacesRepositories;
using System.Linq.Expressions;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;

namespace IrisInfraestructure.Repositories
{
    public class GenericRepository<T>(DynamoDBContext _context) : IGenericRepository<T> where T : class
    {
        public async Task AddAsync(T entity)
        {
            await _context.SaveAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            var batchWrite = _context.CreateBatchWrite<T>();
            batchWrite.AddPutItems(entities);
            await batchWrite.ExecuteAsync();
        }

        public async Task<T?> FindAsync(string id)
        {
            return await _context.LoadAsync<T>(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.ScanAsync<T>(new List<ScanCondition>()).GetRemainingAsync();
        }


        public async Task RemoveAsync(T entity)
        {
            await _context.DeleteAsync(entity);
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            var batchWrite = _context.CreateBatchWrite<T>();
            batchWrite.AddDeleteItems(entities);
            await batchWrite.ExecuteAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await _context.SaveAsync(entity);
        }
    }
}
