using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSL_ERP.Models;

namespace SSL.Core.Interfaces.Services.TestService
{
    public interface INewTestDetailsService : IBaseService<TestDetailVM>
    {
        string TestDetailMethod(string id);
    }
}
