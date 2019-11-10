using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopChallenge.ServicesInjectors
{
    public static class JWTinjector
    {
        public static void InjectJWT(this IServiceCollection serviceCollection, AppSettings settings)
        {
            if (settings is null)
                throw new ArgumentNullException(nameof(settings));
            var sercret = settings.Secret;
            var key = Encoding.ASCII.GetBytes(sercret);
            serviceCollection.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public static void ConfigureJWT(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseAuthentication();
        }
    }
}
