using ApiTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiTest.Controllers
{
    public class BuildingsController : ApiController
    {
        static readonly IBuildingRepository repository = new BuildingRepository();
        public IEnumerable<Building> GetBuilding()
        {
            return repository.GetAll();
        }

        public Building GetBuilding(int id)
        {
            Building item = repository.Get(id);
           if(item==null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return item;
        }

        public IEnumerable<Building> GetProductsByAddress(string address)
        {
            return repository.GetAll().Where(
                b => string.Equals(b.Address, address, StringComparison.OrdinalIgnoreCase));

        }

        public HttpResponseMessage PostBuilding(Building item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<Building>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutBuilding(int id, Building building)
        {
            building.Id = id;
            if (!repository.Update(building))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteBuilding(int id)
        {
            Building item = repository.Get(id);
            if (item == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            repository.Remove(id);
        }
    }
}
