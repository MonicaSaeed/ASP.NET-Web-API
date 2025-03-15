using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StudentAPI.Models;
using System.Text.RegularExpressions;

namespace StudentAPI.Filters
{
    public class ValidateAddressAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var stud = context.ActionArguments["st"] as Student;
            var regex = new Regex("^(Cairo|Giza)$");
            if(stud is null || !regex.IsMatch(stud.Address))
            {
                context.ModelState.AddModelError("Address", "Address not covered 'Cairo|Giza' only");
                context.Result = new BadRequestObjectResult(context.ModelState);
            } 
            
        }
    }
}
