﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConstructIT.Startup))]
namespace ConstructIT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
