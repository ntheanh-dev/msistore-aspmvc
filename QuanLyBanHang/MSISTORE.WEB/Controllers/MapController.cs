using BLL;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MSISTORE.WEB.Controllers
{
    public class MapController : ControllerBase
    {
        private readonly MapService _mapService;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _bingMapsApiKey = "AnmtdlciSHCT7-QaOKIk_DNILKWHw4ehMIsCGTXHi-HTGuGaoQ4KfQppjtyYsh5P";

        public MapController(MapService mapService, IHttpClientFactory httpClientFactory)
        {
            _mapService = mapService;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("/api/{longitude}/{latitude}")]
        public async Task<ActionResult<IEnumerable<Location>>> GetFilteredAccommodations(double longitude, double latitude)
        {
            try
            {
                var locationsMap = _mapService.GetAll();
                var filteredAccommodations = new List<Location>();

                foreach (var location in locationsMap)
                {
                    using (var httpClient = _httpClientFactory.CreateClient())
                    {
                        Console.Write(location);
                        var response = await httpClient.GetAsync($"https://dev.virtualearth.net/REST/v1/Routes/Driving?o=json&wp.0={longitude},{latitude}&wp.1={location.Latitude},{location.Longitude}&key={_bingMapsApiKey}");
                        response.EnsureSuccessStatusCode();
                        var responseData = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<dynamic>(responseData);

                        if (data.resourceSets[0].resources[0].travelDistance < 10)
                        {
                            filteredAccommodations.Add(location);
                        }
                    }
                }

                return Ok(filteredAccommodations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
