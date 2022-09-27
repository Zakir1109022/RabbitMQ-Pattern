using MongoDB.Driver;
using RabbitMQ.Common.Configuration;
using RabbitMQ.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.Infrastructure
{
    public class Repository<TDocument> : IRepository<TDocument>
        where TDocument : IDocument
    {
        private readonly IMongoCollection<TDocument> _collection;

        public Repository(IMongoDbSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }

        public async Task<IEnumerable<TDocument>> GetAll()
        {
            return await _collection
                            .Find(p => true)
                            .ToListAsync();
        }

        public async Task<TDocument> GetById(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            var result = await _collection.Find(filter).SingleOrDefaultAsync();
            return result;
        }


        public async Task Create(TDocument document)
        {
            await _collection.InsertOneAsync(document);

        }

        public async Task<bool> Update(TDocument document)
        {
            var updateResult = await _collection
                                        .ReplaceOneAsync(filter: g => g.Id == document.Id, replacement: document);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            DeleteResult deleteResult = await _collection.DeleteOneAsync(filter);


            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}