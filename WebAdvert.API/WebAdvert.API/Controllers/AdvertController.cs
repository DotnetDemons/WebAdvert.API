using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAdvert.API.Services;

namespace WebAdvert.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertStorageService _storageService; 

        public AdvertController( IAdvertStorageService storageService )
        {
            _storageService = storageService;
        }

        [Route("[Action]")]
        [HttpPost]
        public async Task<IActionResult> AddAdvert(AdvertModel model)
        {
            try
            {
                return StatusCode(200, new CreateAdvertResponse { Id = await _storageService.Add(model) });
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public async Task<IActionResult> Confirm(ConfirmAdvertModel model)
        {
           await _storageService.Confirm(model);
            return StatusCode(200);
        }

        [Route("[Action]")]
        [HttpGet]
        public async Task<IActionResult> GetAdvert(string Id)
        {
            try
            {
                return StatusCode(200,  _storageService.Read(Id));
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        
    }
}
