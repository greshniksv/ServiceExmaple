using Common.UserCommands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Common.UserQueries;
using Common.ViewModels;
using FluentValidation;

namespace BaseApiApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<UserController> _logger;
        private readonly IMediator mediator;

        public UserController(ILogger<UserController> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        [HttpPost()]
        public async Task<IActionResult> Get(CreateUserCommand model)
        {
	        try
	        {
		        int id = await mediator.Send(model);

		        return Ok(id);
	        }
	        catch (ValidationException e)
	        {
		        return BadRequest(e.Errors.ToDictionary(x => x.PropertyName, x => x.ErrorMessage));
	        }
        }

		[HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
	        try
	        {
		        UserModel userModel = await mediator.Send(new GetUserQuery(id));

		        return Ok(userModel);
			}
	        catch (ValidationException e)
	        {
		        return BadRequest(e.Errors.ToDictionary(x=>x.PropertyName, x=>x.ErrorMessage));
	        }
        }
    }
}