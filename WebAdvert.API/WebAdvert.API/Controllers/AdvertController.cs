using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAdvert.API.Services;
using Amazon.SimpleNotificationService;
using Microsoft.Extensions.Configuration;
using AdvertAPI.Models.Messages;
using Newtonsoft.Json;

namespace WebAdvert.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertStorageService _storageService;
        private readonly IConfiguration _configuration;

        public AdvertController( IAdvertStorageService storageService, IConfiguration configuration )
        {
            _storageService = storageService;
            _configuration = configuration;
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

        [Route("[Action]")]
        [HttpPut]
        public async Task<IActionResult> Confirm(ConfirmAdvertModel model)
        {
           await _storageService.Confirm(model);
            await RaiseAdvertConfirmedModel(model);
            return StatusCode(200);
        }

        private async Task RaiseAdvertConfirmedModel(ConfirmAdvertModel model)
        {
            var dbModel = await _storageService.GetById(model.Id);
            var topic = _configuration.GetValue<string>("TopicARN");
            AdvertConfirmedMessage advertConfirmedMessage = new AdvertConfirmedMessage() { Id = model.Id, Title = dbModel.Title };
            var jsonString = JsonConvert.SerializeObject(advertConfirmedMessage);
            using(var client = new  AmazonSimpleNotificationServiceClient())
            {
                await client.PublishAsync(topic, jsonString);
            }
        }
    }
}
