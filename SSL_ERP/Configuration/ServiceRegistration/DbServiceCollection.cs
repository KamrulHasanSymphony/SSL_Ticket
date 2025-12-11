using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;
using SSL.CS.SSL.Common.Core.Interfaces.Services.Company;
using SSL.CS.SSL.Common.Models;
using SSL.CS.SSL.Common.Services.CompanyInfo;
using SSL.Sample.Core.Interfaces.UnitOfWork;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services.CmnDocuments;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services.Product;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services.Purchase;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services.VendorGroup;
using SSL.Sample.SSL.Sample.Core.Interfaces.Services.VendorService;
using SSL.Sample.SSL.Sample.Services.Product;
using SSL.Sample.SSL.Sample.Services.Purchase;
using SSL.Sample.SSL.Sample.Services.Vendor;
using SSL.Sample.SSL.Sample.Services.VendorGroup;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Assignee;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Clients;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Collaboration;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Company;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.EntarnalNotes;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Product;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Task;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.TaskTime;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Ticket;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.TodayTaskSummary;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.Topics;
using SSL.Ticket.SSL.Ticket.Core.Interfaces.Services.UserProfile;
using SSL.Ticket.SSL.Ticket.Models.Tickets;
using SSL.Ticket.SSL.Ticket.Services.Assignee;
using SSL.Ticket.SSL.Ticket.Services.Clients;
using SSL.Ticket.SSL.Ticket.Services.Collaboration;
using SSL.Ticket.SSL.Ticket.Services.Company;
using SSL.Ticket.SSL.Ticket.Services.EnternalNote;
using SSL.Ticket.SSL.Ticket.Services.Task;
using SSL.Ticket.SSL.Ticket.Services.TaskTime;
using SSL.Ticket.SSL.Ticket.Services.Ticket;
using SSL.Ticket.SSL.Ticket.Services.TicketEnternalNotes;
using SSL.Ticket.SSL.Ticket.Services.TodayTaskSummary;
using SSL.Ticket.SSL.Ticket.Services.Topics;
using SSL.Ticket.SSL.Ticket.Services.UserProfile;
using SSL.Ticket.SSL.Ticket.UnitOfWork.SqlServer;
using SSL_ERP.Models;
using SSL_ERP.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SSL_ERP.Configuration.ServiceRegistration
{
    public static class CustomServiceCollection
    {
        public static IServiceCollection AddDbServices(this IServiceCollection services)
        {
            //services.Configure<KestrelServerOptions>(options =>
            //{
            //    options.Limits.MaxRequestBodySize = 10 * 1024 * 1024;
            //});

            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = long.MaxValue;
            });

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<SSL.CS.SSL.Common.Core.Interfaces.UnitOfWork.IUnitOfWork, SSL.CS.SSL.Common.UnitOfWork.SqlServer.UnitOfWorkSqlServer>();
            services.AddTransient<IUnitOfWork, SSL.Sample.UnitOfWork.SqlServer.UnitOfWorkSqlServer>();
            services.AddTransient<SSL.Ticket.SSL.Ticket.Core.Interfaces.UnitOfWork.IUnitOfWork, UnitOfWorkSqlServer>();
            services.AddScoped<ICompanyInfoService, CompanyInfoService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<CommonDataService>();
            services.AddScoped<SSL.Ticket.SSL.Ticket.Services.CommonDataService>();
            services.AddScoped<IVendorServices, VendorServices>();
            services.AddScoped<IVendorGroup, VendorGroupService>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IProductService, ProductServices>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<ICmnDocumentService, DocumentService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IEnternalNoteService, EnternalNoteService>();
            services.AddScoped<ICollaborationService, CollaborationService>();
            services.AddScoped<IAssigneeService, AssigneeService>();
            services.AddScoped<ITaskTimeService, TaskTimeService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<ITktEnternalNoteService, TktEnternalNoteService>();
            services.AddScoped<IClientsService, ClientsService>();
            services.AddScoped<ITopicsServices, TopicService>();
            services.AddScoped<ITodayTaskSummaryServices, TodayTaskSummaryService>();
            services.AddScoped<IProductServices, SSL.Ticket.SSL.Ticket.Services.Product.ProductService>();
            services.AddScoped<DbConfig, DbConfig>();
            services.AddScoped<SSL.Sample.SSL.Sample.Models.DbConfig, SSL.Sample.SSL.Sample.Models.DbConfig>();
            


            return services;
        }
        
        public static IServiceCollection AddAutoMapperServices(this IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }        

        public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
                {
                    config.Password.RequireDigit = false;
                    config.Password.RequireLowercase = false;
                    config.Password.RequireUppercase = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(
                    options =>
                    {
                        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme; 
                    }
                )
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // optional
                    options.AccessDeniedPath = PathString.FromUriComponent("/Home/Index");
                    options.LoginPath = PathString.FromUriComponent("/login");
                    options.LogoutPath = PathString.FromUriComponent("/home/logoutweb");
                    //options.SlidingExpiration = true; // set sliding expiration
                    //options.Cookie.MaxAge = TimeSpan.FromMinutes(30); // set max age of cookie
                    //options.Cookie.IsEssential = true; // mark cookie as essential
                    //options.Cookie.SameSite = SameSiteMode.Lax; // set SameSite mode

                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateAudience = true,
                        ValidIssuer = configuration["JwtIssuer"],
                        ValidAudience = configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            var multiSchemePolicy = new AuthorizationPolicyBuilder(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();

            services.AddAuthorization(options => options.DefaultPolicy = multiSchemePolicy);

            return services;
        }



    }
}
