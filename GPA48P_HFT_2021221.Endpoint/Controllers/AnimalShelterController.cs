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
    public class AnimalShelterController : ControllerBase
    {
        IAnimalShelterLogic asl;

        IHubContext<SignalRHub> hub;

        public AnimalShelterController(IAnimalShelterLogic asl, IHubContext<SignalRHub> hub)
        {
            this.asl = asl;
            this.hub = hub;
        }

        // GET: /animalshelter
        [HttpGet]
        public IEnumerable<AnimalShelter> Get()
        {
            return asl.GetAll();
        }

        // GET /animalshelter/shelterid
        [HttpGet("{shelterid}")]
        public AnimalShelter Get(int shelterId)
        {
            return asl.Read(shelterId);
        }

        // POST /animalshelter
        [HttpPost]
        public void Post([FromBody] AnimalShelter value)
        {
            asl.Create(value);
            this.hub.Clients.All.SendAsync("AnimalShelterCreated", value);
        }

        // PUT /animalshelter
        [HttpPut]
        public void Put([FromBody] AnimalShelter value)
        {
            asl.Update(value);
            this.hub.Clients.All.SendAsync("AnimalShelterUpdated", value);
        }

        // DELETE /animalshelter/shelterid
        [HttpDelete("{shelterid}")]
        public void Delete(int shelterId)
        {
            var shelterToDelete = this.asl.Read(shelterId);
            asl.Delete(shelterId);
            this.hub.Clients.All.SendAsync("AnimalShelterDeleted", shelterToDelete);
        }
    }
}
