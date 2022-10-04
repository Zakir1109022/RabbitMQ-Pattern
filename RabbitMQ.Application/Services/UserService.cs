using RabbitMQ.Core.Entities;
using RabbitMQ.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Application.Services
{
   public class UserService:IUserService
    {
        private readonly IRepository<User> _repository;
        public UserService(IRepository<User> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _repository.GetAll();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<User>> FilterByAsync(Expression<Func<User, bool>> filterExpression)
        {
            return await _repository.FilterBy(filterExpression);
        }

        public async Task CreateAsync(User user)
        {
            await _repository.Create(user);
        }

        public async Task<bool> UpdateAsync(User user)
        {
            return await _repository.Update(user);

        }
    }
}
