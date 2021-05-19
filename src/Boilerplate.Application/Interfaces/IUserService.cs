using Boilerplate.Domain.Entities;
using System;
using System.Threading.Tasks;


namespace Boilerplate.Application.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<User> Authenticate(string email, string password);
    }
}
