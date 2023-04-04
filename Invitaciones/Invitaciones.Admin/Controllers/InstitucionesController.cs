using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Invitaciones.Shared.Data;
using Invitaciones.Admin.Models;

namespace Invitaciones.Admin.Controllers
{
    [CustomAuthorizeAttribute]
    public class InstitucionesController : Controller
    {
        private readonly InvitacionesContext _context;

        public InstitucionesController(InvitacionesContext context)
        {
            _context = context;
        }

        // GET: Instituciones
        public async Task<IActionResult> Index()
        {
              return View(await _context.Instituciones.ToListAsync());
        }

        // GET: Instituciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Instituciones == null)
            {
                return NotFound();
            }

            var institucione = await _context.Instituciones
                .FirstOrDefaultAsync(m => m.IdInstitucion == id);
            if (institucione == null)
            {
                return NotFound();
            }

            return View(institucione);
        }

        // GET: Instituciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Instituciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInstitucion,Nombre,Activo")] Institucione institucione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(institucione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(institucione);
        }

        // GET: Instituciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Instituciones == null)
            {
                return NotFound();
            }

            var institucione = await _context.Instituciones.FindAsync(id);
            if (institucione == null)
            {
                return NotFound();
            }
            return PartialView(institucione);
        }

        // POST: Instituciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInstitucion,Nombre,Activo")] Institucione institucione)
        {
            if (id != institucione.IdInstitucion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(institucione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InstitucioneExists(institucione.IdInstitucion))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(institucione);
        }

        // GET: Instituciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Instituciones == null)
            {
                return NotFound();
            }

            var institucione = await _context.Instituciones
                .FirstOrDefaultAsync(m => m.IdInstitucion == id);
            if (institucione == null)
            {
                return NotFound();
            }

            return View(institucione);
        }

        // POST: Instituciones/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Instituciones == null)
            {
                return Problem("Entity set 'InvitacionesContext.Instituciones'  is null.");
            }
            var institucione = await _context.Instituciones.FindAsync(id);
            if (institucione != null)
            {
                _context.Instituciones.Remove(institucione);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InstitucioneExists(int id)
        {
          return _context.Instituciones.Any(e => e.IdInstitucion == id);
        }
    }
}
