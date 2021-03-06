﻿using System.Threading.Tasks;
using HRAssistant.Web.Contracts.CityManagement;
using HRAssistant.Web.Infrastructure.CQRS;
using Microsoft.AspNetCore.Mvc;

namespace HRAssistant.Web.Controllers
{
    [Route("api/[controller]")]
    public class CityController : Controller
    {
        private readonly IBus _bus;

        public CityController(IBus bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public Task<SearchCitiesResult> Search([FromQuery] SearchCities request) => _bus.Execute(request);

        [HttpGet("{cityId}")]
        public Task<GetCityResult> Get([FromRoute] GetCity request) => _bus.Execute(request);

        [HttpPost]
        public Task<CreateCityResult> Create([FromBody] CreateCity request) => _bus.Request(request);

        [HttpPut]
        public Task Update([FromBody] UpdateCity request) => _bus.Request(request);
    }
}
