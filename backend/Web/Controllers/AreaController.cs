using Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AreaController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IProvinceService _provinceService;
        private readonly IMapper _mapper;

        public AreaController(
            ICountryService countryService,
            IProvinceService provinceService,
            IMapper mapper)
        {
            _countryService = countryService;
            _provinceService = provinceService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetCountries", Name = "Returns countries")]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IEnumerable<AreaModel>> GetCountriesAsync()
        {
            var countries = await _countryService.GetCountriesAsync();

            return _mapper.Map<IEnumerable<AreaModel>>(countries);
        }

        [HttpGet]
        [Route("GetProvinces", Name = "Returns provinces by country")]
        [ResponseCache(VaryByQueryKeys = new string[] { "countryId" }, Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IEnumerable<AreaModel>> GetProvincesAsync(int countryId)
        {
            var provinces = await _provinceService.GetProvincesAsync(countryId);

            return _mapper.Map<IEnumerable<AreaModel>>(provinces);
        }
    }
}