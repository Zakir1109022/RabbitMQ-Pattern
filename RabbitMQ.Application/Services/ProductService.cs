using RabbitMQ.Core;
using RabbitMQ.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabbitMQ.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        public ProductService(IRepository<Product> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _repository.GetAll();
        }
        public async Task<Product> GetByIdAsync(string id)
        {
            return await _repository.GetById(id);
        }
        public async Task CreateAsync(Product product)
        {
           await _repository.Create(product);
        }
        public async Task<bool> UpdateAsync(string id, Product product)
        {
           return await _repository.Update(product);

        }
        public async Task<bool> DeleteAsync(string id)
        {
           return await _repository.Delete(id);
        }
    }
}
