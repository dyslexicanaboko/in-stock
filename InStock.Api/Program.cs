using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace InStock.Api;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
    ContainerConfig.Configure(builder.Host);

//https://www.c-sharpcorner.com/article/how-to-implement-jwt-authentication-in-web-api-using-net-6-0-asp-net-core/
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(
        options =>
        {
          options.RequireHttpsMetadata = false;
          options.SaveToken = true;

          options.TokenValidationParameters = new TokenValidationParameters()
          {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
          };
        });

//https://stackoverflow.com/questions/70554844/asp-net-core-6-web-api-making-fields-required
//The controller was making non-nullable properties required without my permission
    builder.Services
      .AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
      .AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.Logger.LogInformation("InStock API is running");

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
      
      SetLooseCorsPolicyForDevelopmentPurposesOnly(app);
    }

    //Because Next.js sucks I am disabling https for now
    //app.UseHttpsRedirection();
    

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.UseMiddleware<ResponseMiddleware>(app.Logger);

    app.Run();
  }

  private static void SetLooseCorsPolicyForDevelopmentPurposesOnly(IApplicationBuilder app)
  {
    app.UseCors(
      policy =>
      {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
      });
  }
}