using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrainingPortal.Startup))]
namespace TrainingPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
