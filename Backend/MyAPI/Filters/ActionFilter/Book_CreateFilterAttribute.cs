using Core.Data;
using Core.Entities;
using DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyAPI.Filters.ActionFilter
{
    public class Book_CreateFilterAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var dbContext = context.HttpContext.RequestServices.GetService<APIContext>();
            var author = context.ActionArguments["Book"] as NewBook;
            if (author == null)
            {
                context.ModelState.AddModelError("Book", "Book object is null");
                var details = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(details);
            }
            else
            {
                var existingAuthor = dbContext.Books.FirstOrDefault(c => c.Name == author.Name);
                if (existingAuthor != null)
                {
                    context.ModelState.AddModelError("Book", "Book already exist");
                    var details = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(details);
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
