using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace praxi_auth_creatio
{
    public interface ICreatioAuth
    {
        public CookieContainer GetAuthCookie();
    }
}
