using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibraryAPI.Authentication
{
    public static class AuthenticationConfig
    {
        public static WebApplicationBuilder ConfigureAuthentication(this WebApplicationBuilder builder)
        {
            var authenticationSettings = new AuthenticationSettings();
            builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
            builder.Services.AddSingleton(authenticationSettings);
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
                };
            });

            return builder;
        }
    }
}
