using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiTest.Models
{
    public interface IBuildingRepository
    {
        IEnumerable<Building> GetAll();
        Building Get(int id);
        Building Add(Building item);
        void Remove(int id);
        bool Update(Building item);
    }
}