using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            _mapper = mapper;
            _walkRepository = walkRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walksDomainModel = await _walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            return Ok(_mapper.Map<List<WalkDto>>(walksDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await _walkRepository.GetByIdAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDto>(walkDomainModel));
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddWAlkRequestDto addWAlkRequestDto)
        {

            var walkDomainModel = _mapper.Map<Walk>(addWAlkRequestDto);
            walkDomainModel = await _walkRepository.CreateAsync(walkDomainModel);
            var walkDto = _mapper.Map<WalkDto>(walkDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = walkDto.Id }, walkDto);

        }

        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkRequestDto updateWalkRequestDto)
        {

            var walkDomainModel = _mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel = await _walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<WalkDto>(walkDomainModel));

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var WalkDomainModel = await _walkRepository.DeleteAsync(id);

            if (WalkDomainModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<WalkDto>(WalkDomainModel));
        }

    }
}
