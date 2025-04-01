
using APIformbuilder.Service.Concrete;
using APIformbuilder.Service.Interface;


namespace APIformbuilder.Services
{
    static public class ServiceExtension
    {
        public static void ConfigureService(this IServiceCollection services)
        {
            
            services.AddScoped<IAutenticacionService, AutenticacionService>();
            

           
        }
    }
}
