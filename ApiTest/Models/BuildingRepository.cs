using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiTest.Models
{
    public class BuildingRepository : IBuildingRepository
    {
        private List<Building> buildings = new List<Building>();
        private int _nextId = 1;

        public BuildingRepository()
        {
           Add(new Building { Address = "마포구", FloorArea = 12532 });
           Add(new Building { Address = "강남구", FloorArea = 12532 });
           Add(new Building { Address = "중랑구", FloorArea = 12532 });

        }


        public Building Add(Building item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            item.Id = _nextId++;
            buildings.Add(item);
            return item;
        }

        public Building Get(int id)
        {
            return buildings.Find(b => b.Id == id);
        }

        public IEnumerable<Building> GetAll()
        {
            return buildings;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Building item)
        {
            if(item == null)
            throw new ArgumentNullException("item");
            int index = buildings.FindIndex(b => b.Id == item.Id);
            if (index == -1)
                return false;
            buildings.RemoveAt(index);
            buildings.Add(item);
            return true;
        }
    }
}