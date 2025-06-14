using DotNetEnv;
using GoKeep.Repository;
using GoKeep.Business;
//using GoKeepV2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Emit;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
Env.Load(); // Load environment variables from .env file

// Add services to the container. Configure CORS to allow all origins, methods, and headers

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNetlifyOrigin", policy =>
    {
        policy.WithOrigins("https://gokeep-proj.netlify.app")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Register the services
builder.Services.AddScoped<IUserRL, UserRL>();
builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<INotesRL, NotesRL>();
builder.Services.AddScoped<INotesBL, NotesBL>();
builder.Services.AddScoped<ILabelRL, LabelRL>();
builder.Services.AddScoped<ILabelBL, LabelBL>();
builder.Services.AddScoped<INoteLabelRL, NoteLabelRL>();
builder.Services.AddScoped<INoteLableBL, NoteLableBL>();


// Register the DbContext with PostgreSQL
string connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                          $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                          $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                          $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
                          $"Password={Environment.GetEnvironmentVariable("DB_PASS")}";
builder.Services.AddDbContext<DatabaseContext>(option => option.UseNpgsql(connectionString));

// Register the AuthService for JWT authentication
string? JWT_KEY = Environment.GetEnvironmentVariable("JWT_KEY");
if (string.IsNullOrEmpty(JWT_KEY))
{
    throw new InvalidOperationException("JWT_KEY environment variable is not set or is empty.");
}
string? JWT_ISSUER = Environment.GetEnvironmentVariable("JWT_ISSUER");
string? JWT_AUDIENCE = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
var key = Encoding.ASCII.GetBytes(JWT_KEY);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = JWT_ISSUER,
        ValidAudience = JWT_AUDIENCE,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

builder.Services.AddScoped<AuthService>();

//builder.Services.AddDbContext<TestDbContext2>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DbContext")));

builder.Services.AddControllers();
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

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
