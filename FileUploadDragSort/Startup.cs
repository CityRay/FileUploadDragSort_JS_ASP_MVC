using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FileUploadDragSort.Startup))]
namespace FileUploadDragSort
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
