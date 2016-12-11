using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Metro.Startup))]
namespace Metro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
