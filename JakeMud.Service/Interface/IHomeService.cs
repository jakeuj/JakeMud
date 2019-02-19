using JakeMud.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JakeMud.Service.Interface
{
    public interface IHomeService
    {
        void Init();
        List<Message> Input(string Input, string ConnectionId);
    }
}
