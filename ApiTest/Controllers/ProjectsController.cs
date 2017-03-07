using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiTest.Models;
namespace ApiTest.Controllers
{
    public class ProjectsController : ApiController
    {
        public static readonly IProjectRepository repository = new ProjectRepository();
        public IEnumerable<Project> GetProject()
        {
            return repository.GetAll();
        }
        public Project GetProject(int id)
        {
            Project item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public IEnumerable<Project> GetProjectsByCategory(string name)
        {
            return repository.GetAll().Where(
                p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public HttpResponseMessage PostProject(Project item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<Project>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutProject(int id, Project project)
        {
            project.Id = id;
            if (!repository.Update(project))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteProject(int id)
        {
            Project item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(id);
        }
    }
}
