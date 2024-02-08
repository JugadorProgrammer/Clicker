using ClickerServer.interfaces;
using ClickerServer.Services.DataBaseService;

namespace ClickerServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", _ =>
                    {
                        _.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });
            builder.Services.AddLogging();
            builder.Services.AddTransient<IDataBaseService, DataBaseService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
            app.UseCors("AllowAll");
            app.Run();
        }
    }
}
