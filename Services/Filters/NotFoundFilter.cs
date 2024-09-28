using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repositories;

namespace Services.Filters;
public class NotFoundFilter<T,TId>(IGenericRepository<T,TId> genericRepository) 
    : Attribute, IAsyncActionFilter 
    where T: BaseEntity<TId> 
    where TId:struct
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        //Action metottan önce
        var idValue = context.ActionArguments.TryGetValue("id",out var idAsObject) ? idAsObject :null;

        if (idAsObject is not TId id)
        {
            await next();
            return;
        }

        if (!await genericRepository.AnyAsync(id))
        {
            var entityName = typeof(T).Name;
            var actionName = context.ActionDescriptor.RouteValues["action"];
            

            var result = ServiceResult.Fail($"Data bulunamadý.({entityName}-{actionName})");

            context.Result = new NotFoundObjectResult(result);
            return;
        }

        await next();
    }
}
