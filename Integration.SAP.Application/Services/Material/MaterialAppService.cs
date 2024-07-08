using Integration.SAP.Application.Services.Material.Dto;
using Integration.SAP.Application.Services.Material.Interface;
using Integration.SAP.Application.Services.ExternalService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Integration.SAP.Application.Services.Material
{
    public class MaterialAppService : IMaterialAppService
    {
        private readonly SapService _service;
        public MaterialAppService(SapService service)
        {
            _service = service;
        }

        public async Task AddMaterialList(List<MaterialDto> dto)
        {
            try
            {
                await _service.PostMaterialSapDataAsync(dto);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        public async Task<List<MaterialDto>> GetMaterialList()
        {
            List<MaterialDto> data = new List<MaterialDto>();

            try
            {
                var result = await _service.GetMaterialSapDataAsync();
                var content = await result.Content.ReadAsStringAsync();

                data = JsonConvert.DeserializeObject<List<MaterialDto>>(content);

                return data;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

        }
    }
}
