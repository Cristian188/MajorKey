using MajorKey.Core.Contracts.Services;
using MajorKey.Core.Models.DataTransfer;
using MajorKey.Core.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MajorKey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestsController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
            var requests = await _requestService.GetAllRequestAsync();

            if(requests.Count() > 0)
            {
                return Ok(requests);
            }

            return NoContent();
        }
        
        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(long id)
        {
            var request = await _requestService.GetRequestAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return Ok(request);
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(long id, UpdateRequestDto request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            try
            {
                await _requestService.UpdateRequestAsync(request);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await RequestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // PUT: api/Requests/5/status/complete
        [HttpPut("{id}/status/{status}")]
        public async Task<IActionResult> PutRequestStatus(long id, CurrentStatus status)
        {
            var request = await _requestService.GetRequestAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            await _requestService.SetRequestStatusAsync(request, status);

            return Ok();
        }

        // POST: api/Requests
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(CreateRequestDto createRequestDto)
        {
            var request = await _requestService.CreateRequestAsync(createRequestDto);

            return CreatedAtAction(nameof(GetRequest), new { id = request.Id }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Request>> DeleteRequest(long id)
        {
            var request = await _requestService.GetRequestAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            await _requestService.DeleteRequestAsync(request);

            return NoContent();
        }

        private async Task<bool> RequestExists(long id)
        {
            return await _requestService.RequestExist(id);
        }
    }
}
