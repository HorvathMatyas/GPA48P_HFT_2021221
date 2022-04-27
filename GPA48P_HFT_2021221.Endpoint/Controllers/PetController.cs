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
    public class PetController : ControllerBase
    {
        IPetLogic pl;

        IHubContext<SignalRHub> hub;

        public PetController(IPetLogic pl, IHubContext<SignalRHub> hub)
        {
            this.pl = pl;
            this.hub = hub;
        }

        // GET: /pet
        [HttpGet]
        public IEnumerable<Pet> Get()
        {
            return pl.GetAll();
        }

        // GET /pet/petid
        [HttpGet("{petid}")]
        public Pet Get(int petId)
        {
            return pl.Read(petId);
        }

        // POST /pet
        [HttpPost]
        public void Post([FromBody] Pet value)
        {
            pl.Create(value);
            this.hub.Clients.All.SendAsync("PetCreated", value);
        }

        // PUT /pet
        [HttpPut]
        public void Put([FromBody] Pet value)
        {
            pl.Update(value);
            this.hub.Clients.All.SendAsync("PetUpdated", value);
        }

        // DELETE /pet/petid
        [HttpDelete("{petid}")]
        public void Delete(int petId)
        {
            var petToDelete = this.pl.Read(petId);
            pl.Delete(petId);
            this.hub.Clients.All.SendAsync("PetDeleted", petToDelete);
        }
    }
}
