using System;
using System.Collections.Generic;

namespace Hangfire.Dashboard
{
    public class NoAuthFilter : IAuthorizationFilter
    {
        public bool Authorize(IDictionary<string, object> owinEnvironment)
        {
            return true;
        }
    }
}
