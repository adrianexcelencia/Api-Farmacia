using Microsoft.AspNetCore.Mvc;
using APIformbuilder.Models;
using APIformbuilder.Service.Interface;

namespace APIformbuilder.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AutenticacionController : ControllerBase
	{
	
        private readonly IAutenticacionService _IAutenticacionService;
		public AutenticacionController(IConfiguration config, IAutenticacionService IAutenticacionService)
		{
           
            _IAutenticacionService = IAutenticacionService;
        }
		[HttpPost]
        [Route("validar")]
        public async Task<IActionResult> validar([FromBody] LogUsuario request)
        {
            var aservice = await  _IAutenticacionService.validar(request);
            if (aservice.mensaje == null)
            return Ok(aservice);
            return BadRequest(new { aservice.mensaje });
        }

        




    }
}
