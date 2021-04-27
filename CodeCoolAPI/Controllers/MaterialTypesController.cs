using System.Collections.Generic;
using System.Threading.Tasks;
using CodeCoolAPI.Dtos;
using CodeCoolAPI.Helpers;
using CodeCoolAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CodeCoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialTypesController : ControllerBase
    {
        private readonly IMaterialTypeService _materialTypeService;
        private readonly ILogger _logger;

        public MaterialTypesController(IMaterialTypeService materialTypeService, ILogger<MaterialTypesController> logger)
        {
            _materialTypeService = materialTypeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialTypeReadDto>>> Get()
        {
            var materialTypeReadDtoList = await _materialTypeService.ReadAllMaterialTypes();
            _logger.LogInformation(LogMessages.EntitiesFound);
            return Ok(materialTypeReadDtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialTypeReadDto>> Get(int id)
        {
            var readMaterialTypeById = await _materialTypeService.ReadMaterialTypeById(id);
            _logger.LogInformation(LogMessages.EntityFound);
            return Ok(readMaterialTypeById);
        }

        [HttpPost]
        public async Task<ActionResult> Post(MaterialTypeUpsertDto materialTypeUpsertDto)
        {
            var materialTypeReadDto = await _materialTypeService.CreateMaterialTypeReadDto(materialTypeUpsertDto);
            _logger.LogInformation(LogMessages.EntityCreated);
            return CreatedAtAction(nameof(Get), new {materialTypeReadDto.Id}, materialTypeReadDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, MaterialTypeUpsertDto materialTypeUpsertDto)
        {
            await _materialTypeService.UpdateMaterialType(id, materialTypeUpsertDto);
            _logger.LogInformation(LogMessages.EntityUpdated);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _materialTypeService.DeleteMaterialType(id);
            _logger.LogInformation(LogMessages.EntityDeleted);
            return NoContent();
        }
    }
}