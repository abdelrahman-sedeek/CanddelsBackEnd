using CanddelsBackEnd.Contexts;
using CanddelsBackEnd.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CanddelsBackEnd.Infastrcuture;
using CanddelsBackEnd.Repositories.GenericRepo;
using CanddelsBackEnd.Repositories.PorductRepo;
using CanddelsBackEnd.Dtos;
using CanddelsBackEnd.Helper;
using CanddelsBackEnd.Repositories.CartRepo;
using CanddelsBackEnd.Repositories.OrderRepo;
using CanddelsBackEnd.Repositories.ShippingDetailsRepo;
using Microsoft.Extensions.Caching.Memory;
using CanddelsBackEnd.Repositories.ScentsRepo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<CandelContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));

builder.Services.Configure<AdminCredentials>(builder.Configuration.GetSection("AdminCredentials"));

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
        };
            
    });

builder.Services.AddCors(options =>
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowCredentials().AllowAnyHeader().AllowAnyMethod();
    })
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers().AddNewtonsoftJson(op=>op.SerializerSettings.ReferenceLoopHandling=
Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddDbContext<CandelContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));
builder.Services.AddScoped<ICartRepository,CartRepository>();
builder.Services.AddScoped<IScentRepository, ScentRepository>();
builder.Services.AddScoped<IproductRepository, ProductRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IShippingDetailsRepo, ShippingDetailsRepo>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddSingleton<JwtTokenService>();
builder.Services.AddScoped<AdminCredentialsSeeder>();
builder.Services.AddScoped<AdminCredentialsManager>();
builder.Services.AddScoped<IPasswordHasher<AdminCredentials>, PasswordHasher<AdminCredentials>>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<CartHelper>();
builder.Services.AddHostedService<CartCleanupService>();
builder.Services.AddScoped<FileUploadService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

using(var scopee = app.Services.CreateScope())
{
    var seeder = scopee.ServiceProvider.GetRequiredService<AdminCredentialsSeeder>();
    seeder.SeedAdminPassword("admin@2310212Mm");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCookiePolicy();
app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();
var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<CandelContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

try
{
    await context.Database.MigrateAsync();
    await StoreContextSeed.SeedAync(context);
}
catch (System.Exception ex)
{
    logger.LogError(ex, "error occured during migration");
}

app.Run();
