using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiTest.Models;
namespace ApiTest.Controllers
{
    //[Authorize]

    [RoutePrefix("api/projects/{projectId}/plots")]
    public class PlotsController : ApiController
    {
        public static readonly IPlotRepository repository = new PlotRepository();

        [Route("")]
        public IEnumerable<Plot> GetPlot(int projectId)
        {
            var plots = ProjectsController.repository.Get(projectId).Plots;
            return plots;
        }
        [Route("{id}")]
        public Plot GetPlot(int projectId ,int id)
        {
            Plot item = repository.Get(projectId,id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }
        [Route("{adress}")]
        public IEnumerable<Plot> GetPlotsByCategory(int projectId, string address)
        {
            return repository.GetAll(projectId).Where(
                p => string.Equals(p.Address, address, StringComparison.OrdinalIgnoreCase));
        }
        [Route("")]
        public HttpResponseMessage PostPlot(Plot item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<Plot>(HttpStatusCode.Created, item);

            string uri = Url.Link("Plot", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }
        [Route("{id}")]
        public void PutPlot(int id, Plot plot)
        {
            plot.Id = id;
            if (!repository.Update(plot))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
        [Route("{id}")]
        public void DeletePlot(int projectId,int id)
        {
            Plot item = repository.Get(projectId,id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(id);
        }
    }
}
