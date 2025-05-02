using BAL.Services.Person;
using DAL.Repository.Person;
using Microsoft.Data.SqlClient;
using System.Data;
using BAL.Services.RelatedPerson;
using DAL.Repository.City;
using DAL.Repository.PhoneNumbers;
using DAL.Repository.RelatedPersons;
using DAL.UnitOfWork;

namespace PhyisicalPersonsApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbConnection>(sp =>
                new SqlConnection(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPersonRepository, SqlPersonsRepositoryDapper>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPhoneNumberRepository, SqlPhoneNumberRepositoryDapper>();
            services.AddScoped<IRelatedPersonRepository, SqlRelatedPersonRepositoryDapper>();
            services.AddScoped<ICityRepository, SqlCityRepositoryDapper>();
            services.AddScoped<IRelatedPersonService, RelatedPersonService>();

            return services;
        }
    }
}
