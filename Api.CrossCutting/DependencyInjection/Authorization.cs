
using Api.Domain.Security;
using Api.Infrastructure.jwt;
using Api.Service.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using IAuthorizationService = Api.Domain.Interfaces.Services.IAuthorizationService;

namespace Api.CrossCutting.DependencyInjection
{
    public class AuthorizationDependencies
    {
        public static void Inject(IServiceCollection serviceCollection, TokenConfiguration? tokenConfiguration)
        {

            var signingConfiguration = new SigningConfiguration();

            serviceCollection.AddSingleton(signingConfiguration);
            serviceCollection.AddTransient<IAuthorizationService, AuthorizationService>();

            BearerAuthentication(serviceCollection, tokenConfiguration, signingConfiguration);

        }

        public static void BearerAuthentication(IServiceCollection serviceCollection, TokenConfiguration? tokenConfiguration, SigningConfiguration signingConfiguration)
        {
            serviceCollection.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfiguration.Key;
                paramsValidation.ValidAudience = tokenConfiguration.Audience;
                paramsValidation.ValidIssuer = tokenConfiguration.Issuer;
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });


            serviceCollection.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                     .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                     .RequireAuthenticatedUser().Build());
            });
        }

    }
}