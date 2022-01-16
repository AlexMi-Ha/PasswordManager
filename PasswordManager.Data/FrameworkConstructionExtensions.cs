using Dna;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PasswordManager.Core;

namespace PasswordManager.Data {
    public static class FrameworkConstructionExtensions {

        /// <summary>
        /// Extension Method for chaining
        /// </summary>
        /// <param name="construction"></param>
        /// <returns></returns>
        public static FrameworkConstruction UseClientDataStore(this FrameworkConstruction construction) {
            //Inject data store
            construction.Services.AddDbContext<ClientDataStoreDbContext>(options => {
                //setup connection string
                string s = "Data Source=pwMan.cspd";
                options.UseSqlite(s);
            });

            // Inject IClientDataStore for access to the database (scoped)
            construction.Services.AddScoped<IClientDataStore>(
                provider => new ClientDataStore(provider.GetService<ClientDataStoreDbContext>())
            );

            return construction;
        }

    }
}
