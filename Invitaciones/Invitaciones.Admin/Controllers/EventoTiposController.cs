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
    public class EventoTiposController : Controller
    {
        private readonly InvitacionesContext _context;
        private readonly ILogger<EventoOrganizadoresController> _logger;
        public EventoTiposController(InvitacionesContext context, ILogger<EventoOrganizadoresController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: EventoTipos
        public async Task<IActionResult> Index()
        {
              return View(await _context.EventoTipos.ToListAsync());
        }

        // GET: EventoTipos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EventoTipos == null)
            {
                return NotFound();
            }

            var eventoTipo = await _context.EventoTipos
                .FirstOrDefaultAsync(m => m.IdTipo == id);
            if (eventoTipo == null)
            {
                return NotFound();
            }

            return View(eventoTipo);
        }

        // GET: EventoTipos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EventoTipos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipo,Tipo,Descripcion,Activo,FechaCreacion,UltimaModificacion")] EventoTipo eventoTipo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(eventoTipo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(eventoTipo);
            }
            return View(eventoTipo);
        }

        // GET: EventoTipos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EventoTipos == null)
            {
                return NotFound();
            }

            var eventoTipo = await _context.EventoTipos.FindAsync(id);
            if (eventoTipo == null)
            {
                return NotFound();
            }
            return PartialView(eventoTipo);
        }

        // POST: EventoTipos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipo,Tipo,Descripcion,Activo,FechaCreacion,UltimaModificacion")] EventoTipo eventoTipo)
        {
            try
            {
                if (id != eventoTipo.IdTipo)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(eventoTipo);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EventoTipoExists(eventoTipo.IdTipo))
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(eventoTipo);
            }
            
            return View(eventoTipo);
        }

        // GET: EventoTipos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EventoTipos == null)
            {
                return NotFound();
            }

            var eventoTipo = await _context.EventoTipos
                .FirstOrDefaultAsync(m => m.IdTipo == id);
            if (eventoTipo == null)
            {
                return NotFound();
            }

            return View(eventoTipo);
        }

        // POST: EventoTipos/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EventoTipos == null)
            {
                return Problem("Entity set 'InvitacionesContext.EventoTipos'  is null.");
            }
            var eventoTipo = await _context.EventoTipos.FindAsync(id);
            if (eventoTipo != null)
            {
                _context.EventoTipos.Remove(eventoTipo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoTipoExists(int id)
        {
          return _context.EventoTipos.Any(e => e.IdTipo == id);
        }
    }
}
