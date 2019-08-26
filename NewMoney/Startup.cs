using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewMoney.Startup))]
namespace NewMoney
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
