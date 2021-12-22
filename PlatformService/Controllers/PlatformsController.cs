using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public readonly IPlatformRepo _platformRepo;
        public readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo platformRepo, IMapper mapper)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine(" --> Getting Platforms");
            var platformItems = _platformRepo.GetAllPlatforms();
            var platforms = _mapper.Map<IEnumerable<PlatformReadDto>>(platformItems);
            return Ok(platforms);
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platformItem = _platformRepo.GetPlatformById(id);
            if(platformItem != null)
            {
                var platform = _mapper.Map<PlatformReadDto>(platformItem);
                return Ok(platform);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform (PlatformCreateDto dto)
        {
            var model = _mapper.Map<Platform>(dto);
            _platformRepo.CreatePlatform(model);
            bool result = _platformRepo.SaveChanges();
            if (result)
            {
                var platformReadDto = _mapper.Map<PlatformReadDto>(model);
                return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
            }
            return BadRequest();
        }
    }
}
