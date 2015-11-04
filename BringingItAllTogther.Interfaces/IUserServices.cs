using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BringingItAllTogether.Interfaces
{
    public interface IUserServices
    {
        long Authenticate(string userName, string password);
    }
}
