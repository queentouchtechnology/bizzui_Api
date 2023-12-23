using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
// using dotenv.net;
using DotNetEnv;
using BizzuiApi.Models;
using BizzuiApi.Data;

var builder = WebApplication.CreateBuilder(args);
 // Enable CORS
builder.Services.AddCors(options =>
    {
        //WithMethods("GET","POST","PUT","DELETE")
        options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    });
// Add services to the container.
builder.Services.AddControllers();

Env.Load();
var ConnectionString = string.Empty;
if (builder.Environment.IsDevelopment()) {
    ConnectionString = Env.GetString("DEV_DB");
    Console.WriteLine("DevDB: {0}",ConnectionString);
} else if (builder.Environment.IsProduction()) {
    ConnectionString = Env.GetString("PROD_DB");
    Console.WriteLine("ProdDB: {0}",ConnectionString);
}

#pragma warning disable CS8604 // Possible null reference argument.
// builder.Services.AddDbContext<AppDbContext>(opt =>
//     opt.UseSqlServer(builder.Configuration.GetConnectionString(ConnectionString)));
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(ConnectionString));
#pragma warning restore CS8604 // Possible null reference argument.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Bizzui API",
        Description = "An ASP.NET Core Web API for managing Bizzui",
        TermsOfService = new Uri("https://bizzui.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Queentouch Technology",
            Url = new Uri("https://queentouchtechnology.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://queentouchtechnology.com/license")
        }
    });
});

var app = builder.Build();




// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.DefaultModelsExpandDepth(-1);
        options.RoutePrefix = "swagger";
    });
// }

app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
