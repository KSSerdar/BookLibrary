using Microsoft.Extensions.DependencyInjection;
using Services.IService;
using Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace External.Dependency
{
    public static class DependencyModule
    {
        public static IServiceCollection AddDependencyModule(this IServiceCollection services)
        {
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICommentService, CommentService>();
          //  services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
