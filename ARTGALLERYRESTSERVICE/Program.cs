
using ARTGALLERYRESTSERVICE.Models;
using ARTGALLERYRESTSERVICE.Models.Db;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ARTGALLERYRESTSERVICE
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
           
            //TO CONNECT ANGULAR TO REST API:
            //here 4200 is port where angular app is running.
            // CORS - Cross origin resource sharing

            builder.Services.AddCors(config =>
            {
                config.AddPolicy("policy2", configurePolicy =>
                {
                    configurePolicy.AllowAnyHeader();
                    configurePolicy.AllowAnyMethod();
                    configurePolicy.WithOrigins("http://localhost:4200");
                });
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:audience"],
                    ValidIssuer = builder.Configuration["Jwt:issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]!))
                };
            });

           builder.Services.AddAuthorization(config => {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
            });

            string connectionstring = "Server=localhost;database=ArtGallery;trusted_connection=yes;trustservercertificate=true";
            builder.Services.AddDbContext<ArtGalleryContext>(config => config.UseSqlServer(connectionstring));


            builder.Services.AddTransient(typeof(ArtGalleryService));

            builder.Services.AddTransient(typeof(SecurityService));
            builder.Services.AddTransient(typeof(DataService));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

           

            app.UseCors("policy2");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}