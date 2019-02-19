using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JakeMud.Models;
using JakeMud.Service.Interface;

namespace JakeMud.Service.ModelService
{
    class PathService : GenericRepositoryService<Path>, IPathService
    {
        private Models.Interface.IRepository<Path> repository = new Models.Repository.GenericRepository<Path>();
        /// <summary>
        /// Gets the by cateogy.
        /// </summary>
        /// <param name="pathID">The category ID.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<Path> GetByMapID(int mapID)
        {
            return this.repository.GetAll().Where(x => x.PathID == mapID);
        }
    }
}
