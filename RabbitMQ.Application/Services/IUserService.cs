using RabbitMQ.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Application.Services
{
   public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        Task<IEnumerable<User>> FilterByAsync(Expression<Func<User, bool>> filterExpression);
        Task CreateAsync(User use);
        Task<bool> UpdateAsync(User user);
    }
}
