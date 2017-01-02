using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MusicViewer.Startup))]
namespace MusicViewer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
