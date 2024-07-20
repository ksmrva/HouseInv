using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace HouseInv.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        protected ActionResult Problem(List<Error> errors)
        {
            ActionResult result;
            if (errors.Any(err => err.Type == ErrorType.Unexpected))
            {
                result = Problem();
            }
            else if (errors.All(err => err.Type == ErrorType.Validation))
            {
                var modelStateDictionary = new ModelStateDictionary();
                foreach (var error in errors)
                {
                    modelStateDictionary.AddModelError(error.Code, error.Description);
                }
                result = ValidationProblem(modelStateDictionary);
            }
            else
            {
                var firstError = errors[0];
                var statusCode = firstError.Type switch
                {
                    ErrorType.NotFound => StatusCodes.Status404NotFound,
                    ErrorType.Validation => StatusCodes.Status400BadRequest,
                    ErrorType.Conflict => StatusCodes.Status409Conflict,
                    _ => StatusCodes.Status500InternalServerError
                };
                result = Problem(statusCode: statusCode, title: firstError.Description);
            }
            return result;
        }
    }
}