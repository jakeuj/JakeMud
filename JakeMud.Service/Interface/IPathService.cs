using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JakeMud.Models;
using JakeMud.Service.Misc;

namespace JakeMud.Service.Interface
{
    interface IPathService : IRepositoryService<Path>
    {
        IEnumerable<Path>GetByMapID(int mapID);
    }
}
