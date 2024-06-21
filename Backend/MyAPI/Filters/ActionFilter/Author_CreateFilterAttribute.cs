using Core.Entities;
using DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace MyAPI.Filters.ActionFilter
{
    public class Author_CreateFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var dbContext= context.HttpContext.RequestServices.GetService<APIContext>();
            var author = context.ActionArguments["Author"]as Author;
            if (author == null)
            {
                context.ModelState.AddModelError("Author", "Author object is null");
                var details = new ValidationProblemDetails(context.ModelState)
                {
                    Status=StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
            }
            else
            {
                var existingAuthor = dbContext.Authors.FirstOrDefault(c=>c.Name == author.Name);
                if (existingAuthor != null)
                {
                    context.ModelState.AddModelError("Author", "Author already exist");
                    var details= new ValidationProblemDetails(context.ModelState)
                    { 
                        Status=StatusCodes.Status400BadRequest 
                    };
                    context.Result= new BadRequestObjectResult(details);
                }
            }
            base.OnActionExecuting(context);
        }
    
    }
}
