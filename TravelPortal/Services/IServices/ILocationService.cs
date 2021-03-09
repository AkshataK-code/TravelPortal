using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPortal.Repository.IRepository
{
    public interface ILocationService : IService<string>
    {
        string GetCity(string json);
    }
}
