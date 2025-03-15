using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StudentAPI.Filters
{
    public class MyResultFilterAttribute:ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("X-Custom-Header", "This is a custom result filter");
            if (context.Result is ObjectResult res)
            {
                res.Value = new
                {
                    Success = true,
                    Data = res.Value,
                    Message = "Processed successfully"
                };
            }
        }
    }
}
