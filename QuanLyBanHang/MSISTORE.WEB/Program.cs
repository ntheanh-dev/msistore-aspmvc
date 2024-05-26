using AutoMapper;
using BLL;
using BLL.Token;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBLLServices(); // Đảm bảo rằng phương thức này đăng ký các dịch vụ BLL cần thiết
builder.Services.AddMvc();
builder.Services.AddControllersWithViews();

//session
builder.Services.AddSession(options =>
{
    options.Cookie.IsEssential = true; // Đảm bảo rằng cookie session được xem là bắt buộc
    options.Cookie.HttpOnly = true; // Không cho phép script JavaScript truy cập cookie session
    options.Cookie.SameSite = SameSiteMode.Strict; // Xác định cách thức cookie session được gửi đến các trang web khác
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Sử dụng HTTPS để gửi cookie session
    options.IdleTimeout = TimeSpan.FromSeconds(300); // Đặt thời gian timeout cho session là 300 giây (5 phút)
});

//Cors 
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Cấu hình JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<MapService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


//Cloudinary

Account cloudinaryCredentials = new Account(
    builder.Configuration["Cloudinary:CloudName"],
    builder.Configuration["Cloudinary:ApiKey"],
    builder.Configuration["Cloudinary:ApiSecret"]);

Cloudinary cloudinary = new Cloudinary(cloudinaryCredentials);
builder.Services.AddSingleton(cloudinary);


var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);

//Sử dụng xác thực và ủy quyền
app.UseAuthentication();
app.UseRouting();
app.UseSession();
app.UseAuthorization();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "Admin",
      pattern: "{area:exists}/{controller}/{action}/"
    );
});

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();


app.MapControllers();
app.Run();