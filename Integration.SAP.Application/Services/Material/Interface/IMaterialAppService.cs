using Integration.SAP.Application.Services.Material.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.SAP.Application.Services.Material.Interface
{
    public interface IMaterialAppService
    {
        Task<List<MaterialDto>> GetMaterialList();
        Task AddMaterialList(List<MaterialDto> dto);
    }
}
