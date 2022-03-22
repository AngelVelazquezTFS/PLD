using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PLD.Startup))]
namespace PLD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
