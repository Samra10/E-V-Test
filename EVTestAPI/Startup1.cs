using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("EVTesAPIConfig",typeof(EVTestAPI.Startup1))]

namespace EVTestAPI
{
    public partial class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
