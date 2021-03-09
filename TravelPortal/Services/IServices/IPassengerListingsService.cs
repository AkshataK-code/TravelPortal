using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelPortal.Repository.IRepository
{
    public interface IPassengerListingsService : IService<string>
    {
        bool AddListings(string json);

        public string GetListingsForNumberOfPassengers(int numberOfPassengers);
    }
}
