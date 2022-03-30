using Microsoft.AspNetCore.Mvc;
using eTickets.Data;
using System.Linq;
using eTickets.Data.Services;
using System.Threading.Tasks;
using eTickets.Models;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;
        }
        public  async Task<IActionResult> Index()
        {
            var data =await _service.GetAllAsync();
            return View(data);
        }


        //get: Actors/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }


        //Get: Actors/Details/1
        //[AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails =await _service.GetByIdAsync(id);

            if (actorDetails == null) return View("Not Found");
            return View(actorDetails);
        }

        //get: Actors/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null) return View("Not Found");
            return View(actorDetails);
        }


        [HttpPost]
        public async Task<IActionResult> Edit( int id, [Bind("Id,FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {

                return View(actor);
            }
            await _service.UpdateAsync(id,actor);
            return RedirectToAction(nameof(Index));
        }

        //get: Actors/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("Not Found");
            return View(actorDetails);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmd(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("Not Found");
            
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
