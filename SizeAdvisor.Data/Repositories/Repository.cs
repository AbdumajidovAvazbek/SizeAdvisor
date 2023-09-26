using Newtonsoft.Json;
using SizeAdvisor.Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json.Serialization;
using SizeAdvisor.Domain.Configurations;
using System.Threading.Tasks;
using SizeAdvisor.Domain.Commons;

namespace SizeAdvisor.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
    {
        private readonly string Path = DatabasePath.UserDb;
        public Repository()
        {
            var str = File.ReadAllText(Path);
            if (string.IsNullOrEmpty(str))
                File.WriteAllText(Path, "[]");
        }
        public async Task<bool> DeleteAsync(long Id)
        {
            var entities = await SelectAllAsync();
            var entity = entities.FirstOrDefault(e => e.Id == Id);
            entities.Remove(entity);
            var str = JsonConvert.SerializeObject(entities, Newtonsoft.Json.Formatting.Indented);
            await File.WriteAllTextAsync(Path, str);
            return true;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            string str = await File.ReadAllTextAsync(Path);
            List<TEntity> entities = JsonConvert.DeserializeObject<List<TEntity>>(str);
            entities.Add(entity);
            string result = JsonConvert.SerializeObject(entities, Newtonsoft.Json.Formatting.Indented);
            await File.WriteAllTextAsync(Path, result);
            return entity;
        }

        public async Task<List<TEntity>> SelectAllAsync()
        {
            var str = await File.ReadAllTextAsync(Path);
            var entities = JsonConvert.DeserializeObject<List<TEntity>>(str);
            return entities;
        }

        public async Task<TEntity> SelecttByIdAsync(long Id)
        {
            return (await SelectAllAsync()).FirstOrDefault(e => e.Id == Id);
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var entities = await SelectAllAsync();
            await File.WriteAllTextAsync(Path, "[]");

            foreach (var data in entities)
            {
                if (data.Id == entity.Id)
                {
                    await InsertAsync(entity);
                    continue;
                }
                await InsertAsync(data);
            }

            return entity;
        }
    }
}
