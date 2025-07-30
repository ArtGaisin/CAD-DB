
namespace WebApi
{
    using DataAccess;
    using BusinessLogic;
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDataAccess();
            builder.Services.AddBuisnessLogic();
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
             var app = builder.Build();

            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.Run();
        }


    }
}
