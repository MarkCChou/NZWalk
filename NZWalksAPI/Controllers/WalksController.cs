using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{

    // /api/wallks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository _walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            _walkRepository = walkRepository;
        }

        // CREATE walk
        // POST: /api/walks
        [HttpPost]

        public async Task<IActionResult> Create([FromBody] AddWAlkRequestDto addWAlkRequestDto)
        {

            //Map DTO to a Domain model
            var walkDomainModel = mapper.Map<Walk>(addWAlkRequestDto);
            await _walkRepository.CreateAsync(walkDomainModel);

            //Map Domain model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }

        // GET Walks
        // GET: /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> Getall([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walksDomainModel = await _walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            // Map Domain Model to DTO
            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        // Get Walk By ID
        // GET: /api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await _walkRepository.GetByIdAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        // Update Walk By Id
        // Put : /api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]


        public async Task<IActionResult> Update([FromRoute] Guid id,
            [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {

            // Map Dto to Domain Model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel = await _walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to Dto
            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }

        // Delete a Walk By Id
        // Delete: /api/walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await _walkRepository.DeleteAsync(id);

            if (deletedWalkDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO

            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));

        }

    }
}
