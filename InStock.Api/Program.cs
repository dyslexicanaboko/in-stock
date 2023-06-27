using InStock.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ContainerConfig.Configure(builder.Host);

//https://stackoverflow.com/questions/70554844/asp-net-core-6-web-api-making-fields-required
//The controller was making non-nullable properties required without my permission
builder.Services
    .AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
    .AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ResponseMiddleware>();

app.Run();
