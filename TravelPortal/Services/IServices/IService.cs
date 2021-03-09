using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPortal.Repository.IRepository
{
    public interface IService<T> where T : class
    {
        Task<string> GetAsync(string url, string parameters);
    }
}
