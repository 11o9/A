using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiTest.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Plot> Plots { get; set; }
    }
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAll();
        Project Get(int id);
        Project Add(Project item);
        void Remove(int id);
        bool Update(Project item);
    }
    public class ProjectRepository : IProjectRepository
    {
        private List<Project> projects = new List<Project>();
        private int _nextId = 1;
        public ProjectRepository()
        {
            Add(new Project() { Name = "마포구 일대" , Plots = new List<Plot> { new Plot() { Address = "마포필지1", Area = 12 }, new Plot() { Address = "마포필지2", Area = 34 }, new Plot() { Address = "마포필지3", Area = 56 } } });
            Add(new Project() { Name = "강남구 일대" , Plots = new List<Plot> { new Plot() { Address = "강남필지1", Area = 123 }, new Plot() { Address = "강남필지2", Area = 345 } } });
            Add(new Project() { Name = "중랑구 일대" , Plots = new List<Plot> { new Plot() { Address = "중랑필지1", Area = 12 }, new Plot() { Address = "중랑필지2", Area = 34 }, new Plot() { Address = "중랑필지3", Area = 56 }, new Plot() { Address = "중랑필지4", Area = 78 } } });
        }

        public IEnumerable<Project> GetAll()
        {
            return projects;
        }

        public Project Get(int id)
        {
            return projects.Find(p => p.Id == id);
        }

        public Project Add(Project item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            item.Id = _nextId++;
            projects.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            projects.RemoveAll(p => p.Id == id);
        }

        public bool Update(Project item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = projects.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            projects.RemoveAt(index);
            projects.Add(item);
            return true;
        }
    }

    public class Plot
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public double Area { get; set; }
    }

    public interface IPlotRepository
    {
        IEnumerable<Plot> GetAll(int projectId);
        Plot Get(int projectId,int id);
        Plot Add(Plot item);
        void Remove(int id);
        bool Update(Plot item);
    }
    public class PlotRepository : IPlotRepository
    {
        List<Plot> plots = new List<Plot>();
        private int _nextId = 1;
        public PlotRepository()
        {
            Add(new Plot() { Address = " 필지1" , Area = 50 });
            Add(new Plot() { Address = " 필지2" , Area = 90 });
            Add(new Plot() { Address = " 필지3" , Area = 120 });
        }

        public IEnumerable<Plot> GetAll(int i)
        {
            //i프로젝트에 속한 대지 찾아서,
            return plots;
        }

        public Plot Get(int i,int id)
        {
            //i프로젝트에 속한 대지 찾아서, id번째.
            return plots.Find(p => p.Id == id);
        }

        public Plot Add(Plot item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            item.Id = _nextId++;
            plots.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            plots.RemoveAll(p => p.Id == id);
        }

        public bool Update(Plot item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = plots.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            plots.RemoveAt(index);
            plots.Add(item);
            return true;
        }
    }

}