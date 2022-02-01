using Docway.Nursing.API.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using Matrix.AppSample.Domain.Travels.Infrastructure;
using Matrix.AppSample.HttpApi.Models.Travels;

namespace Matrix.AppSample.HttpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public sealed class TravelsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly TravelsQueries _travelsQueries;

        public TravelsController(
            IMediator mediator,
            TravelsQueries travelsQueries)
        {
            _mediator = mediator;
            _travelsQueries = travelsQueries;
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] CreateTravelInputModel inputModel)
        {            
            var command = inputModel.CreateCommand();
            if (command.IsFailure)
                return BadRequest(new ErrorDTO(command.Error));
            
            var result = await _mediator.Send(command.Value);
            if (result.IsFailure)
                return BadRequest(new ErrorDTO(result.Error));
            return CreatedAtAction(nameof(Get), result.Value, result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> Query(int page, int take, string filter, CancellationToken cancellationToken)
        {
            page = page > 0 ? page : 1;
            take = take > 0 ? take : 20;

            var data = await _travelsQueries.GetTravelsListAsync(page, take, filter, cancellationToken);

            return new OkObjectResult(data);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var travel = await _travelsQueries.GetTravelDetailByIdAsync(id, cancellationToken);
            if (travel == null)
                return NotFound();
            return new OkObjectResult(travel);
        }
    }
}
