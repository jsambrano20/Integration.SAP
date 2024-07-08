using Integration.SAP.Application.Services.Material.Dto;
using Integration.SAP.Application.Services.Material.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Integration.SAP.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialAppService _service;

        public MaterialController(IMaterialAppService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<MaterialDto>> GetAsync()
        {
            var data = await _service.GetMaterialList();
            return data;
        }

        [HttpPost]
        public async Task AddAsync([FromBody] List<MaterialDto> dtos)
        {
            await _service.AddMaterialList(dtos);
        }
    }
}
