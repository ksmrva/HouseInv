using Microsoft.AspNetCore.Mvc;

namespace HouseInv.Controllers
{
    public class ErrorsController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}