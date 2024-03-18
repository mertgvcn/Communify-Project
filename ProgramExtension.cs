using Microsoft.OpenApi.Models;

namespace LethalCompany_Backend
{
    public static class ProgramExtension
    {
        public static void AddAPI(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            // Swagger support for authorization

            builder.Services.AddSwaggerGen();
        }
    }
}
