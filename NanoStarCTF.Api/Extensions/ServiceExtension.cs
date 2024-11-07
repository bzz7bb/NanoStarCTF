using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NanoStarCTF.DataAccess.DbContexts;
using NanoStarCTF.DataAccess.Repositories;
using NanoStarCTF.Domain.Entitys.Commons;
using NanoStarCTF.Domain.Entitys.CTFs;
using NanoStarCTF.Domain.Entitys.Groups;
using NanoStarCTF.Domain.Entitys.Users;
using NanoStarCTF.Service.IService;
using NanoStarCTF.Service.Service;
using System.Text;

namespace NanoStarCTF.Api.Extensions
{
    public static class ServiceExtension
    {
        public static void AddDBConTextes(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                 options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void AddServices(this IServiceCollection collection)
        {

            //Repositorys
             collection.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
             collection.AddTransient<IGenericRepository<Group>, GenericRepository<Group>>();
             collection.AddTransient<IGenericRepository<Challenge>, GenericRepository<Challenge>>();
             collection.AddTransient<IGenericRepository<Hints>, GenericRepository<Hints>>();
             collection.AddTransient<IGenericRepository<Attachment>, GenericRepository<Attachment>>();
             collection.AddTransient<IGenericRepository<HomeAddress>, GenericRepository<HomeAddress>>();



            //Services
            collection.AddScoped<IAuthService, AuthService>();
            collection.AddTransient<IUserService, UserService>();
             collection.AddTransient<IGroupService, GroupService>();
             collection.AddTransient<ICTFService, CTFService>();
             collection.AddTransient<IHintsService, HintService>();
             collection.AddTransient<IAttachmentService, AttachmentService>();
             collection.AddTransient<IAddressService, AddressService>();
             collection.AddTransient<ICheckerService, CheckService>();


        }


        public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var Key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                };
            });


            services.AddControllers();


        }

        public static void AddSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(p =>
            {
                p.ResolveConflictingActions(ad => ad.First());
                p.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                });

                p.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }

    }
}
