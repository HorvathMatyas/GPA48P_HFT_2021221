using GPA48P_HFT_2021221.Models;
using GPA48P_HFT_2021221.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;
using GPA48P_HFT_2021221.Endpoint.Services;

namespace GPA48P_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        IOwnerLogic ol;

        IHubContext<SignalRHub> hub;

        public OwnerController(IOwnerLogic ol, IHubContext<SignalRHub> hub)
        {
            this.ol = ol;
            this.hub = hub;
        }

        // GET: /owner
        [HttpGet]
        public IEnumerable<Owner> Get()
        {
            return ol.GetAll();
        }

        // GET /owner/ownerid
        [HttpGet("{ownerid}")]
        public Owner Get(int ownerId)
        {
            return ol.Read(ownerId);
        }

        // POST /owner
        [HttpPost]
        public void Post([FromBody] Owner value)
        {
            ol.Create(value);
            this.hub.Clients.All.SendAsync("OwnerCreated", value);
        }

        // PUT /owner
        [HttpPut]
        public void Put([FromBody] Owner value)
        {
            ol.Update(value);
            this.hub.Clients.All.SendAsync("OwnerUpdated", value);
        }

        // DELETE /owner/ownerid
        [HttpDelete("{ownerid}")]
        public void Delete(int ownerId)
        {
            var ownerToDelete = this.ol.Read(ownerId);
            ol.Delete(ownerId);
            this.hub.Clients.All.SendAsync("OwnerDeleted", ownerToDelete);
        }
    }
}
