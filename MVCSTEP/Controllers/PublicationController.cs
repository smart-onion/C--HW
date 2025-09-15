using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCSTEP.Data;
using MVCSTEP.Filters;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;
using MVCSTEP.Repositories;
using MVCSTEP.ViewModels;

namespace MVCSTEP.Controllers
{
    [Authorize]
    public class PublicationController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IFriend _friendRepository;
        private readonly IPublication _publicationRepository;

        public PublicationController(ApplicationContext context, UserManager<User> userManager,
            IFriend friendRepository, IPublication publicationRepository)
        {
            _context = context;
            _userManager = userManager;
            _friendRepository = friendRepository;
            _publicationRepository = publicationRepository;
        }


        // GET: Publication
        public async Task<IActionResult> Index()
        {
            var publications = await _publicationRepository.GetPublications();
            if (publications == null) return View(new List<PublicationViewModel>());

            var model = publications.Select(o => new PublicationViewModel
            {
                Id = o.Id,
                Title = o.Title,
                Description = o.Description,
                Creator = new UserViewModel
                {
                    Id = o.UserId,
                    Name = o.User.UserName,
                    Email = o.User.Email,
                },
                PublicationAccess = o.PublicationAccess,
                PublicationDate = o.PublicationDate,
            });

            return View(model);
        }

        // GET: Publication/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publication = await _publicationRepository.GetPublication(id.Value);
            if (publication == null)
            {
                return NotFound();
            }

            return View(publication);
        }

        // GET: Publication/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publication/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            PublicationViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return BadRequest();

            var publication = new Publication
            {
                Title = model.Title,
                Description = model.Description,
                PublicationAccess = model.PublicationAccess,
                UserId = userId
            };

            _context.Add(publication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));


            return View(model);
        }

        // GET: Publication/Edit/5
        [PublicationAccessAuthFilter("PublicationOwner")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publication = await _context.Publication.FindAsync(id);
            if (publication == null)
            {
                return NotFound();
            }

            return View(new PublicationViewModel
            {
                Id = id,
                Title = publication.Title,
                Description = publication.Description,
                PublicationAccess = publication.PublicationAccess
            });
        }

        // POST: Publication/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PublicationAccessAuthFilter("PublicationOwner")]
        public async Task<IActionResult> Edit(PublicationViewModel model)
        {
            var publication = await _context.Publication.FindAsync(model.Id);
            if (publication == null)
            {
                return NotFound();
            }

            try
            {
                publication.Title = model.Title;
                publication.Description = model.Description;
                publication.PublicationAccess = model.PublicationAccess;

                _context.Update(publication);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublicationExists(publication.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));

            return View(model);
        }

        // GET: Publication/Delete/5
        [PublicationAccessAuthFilter("PublicationOwner")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var publication = await _context.Publication
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publication == null)
            {
                return NotFound();
            }

            return View(publication);
        }

        // POST: Publication/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [PublicationAccessAuthFilter("PublicationOwner")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var publication = await _context.Publication.FindAsync(id);
            if (publication != null)
            {
                _context.Publication.Remove(publication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicationExists(int id)
        {
            return _context.Publication.Any(e => e.Id == id);
        }
    }
}