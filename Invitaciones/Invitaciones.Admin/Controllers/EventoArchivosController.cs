using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Invitaciones.Shared.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Invitaciones.Admin.Models;

namespace Invitaciones.Admin.Controllers
{
    [CustomAuthorizeAttribute]
    public class EventoArchivosController : Controller
    {
        private readonly InvitacionesContext _context;

        public EventoArchivosController(InvitacionesContext context)
        {
            _context = context;
        }

        // GET: EventoArchivos
        public async Task<IActionResult> Index()
        {
            var invitacionesContext = _context.EventoArchivos.Include(e => e.IdEventoNavigation);
            return View(await invitacionesContext.ToListAsync());
        }

        // GET: EventoArchivos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EventoArchivos == null)
            {
                return NotFound();
            }

            var eventoArchivo = await _context.EventoArchivos
                .Include(e => e.IdEventoNavigation)
                .FirstOrDefaultAsync(m => m.IdEventoArchivo == id);
            if (eventoArchivo == null)
            {
                return NotFound();
            }

            return View(eventoArchivo);
        }

        // GET: EventoArchivos/Create
        public IActionResult Create()
        {
            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "Evento1");
            return View();
        }

        // POST: EventoArchivos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEventoArchivo,IdEvento,Archivo,Tipo,Url,Activo")] EventoArchivo eventoArchivo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventoArchivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "Evento1", eventoArchivo.IdEvento);
            return View(eventoArchivo);
        }

        // GET: EventoArchivos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EventoArchivos == null)
            {
                return NotFound();
            }

            var eventoArchivo = await _context.EventoArchivos.FindAsync(id);
            if (eventoArchivo == null)
            {
                return NotFound();
            }
            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "Evento1", eventoArchivo.IdEvento);
            return View(eventoArchivo);
        }

        // POST: EventoArchivos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEventoArchivo,IdEvento,Archivo,Tipo,Url,Activo")] EventoArchivo eventoArchivo)
        {
            if (id != eventoArchivo.IdEventoArchivo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventoArchivo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoArchivoExists(eventoArchivo.IdEventoArchivo))
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
            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "Evento1", eventoArchivo.IdEvento);
            return View(eventoArchivo);
        }

        // GET: EventoArchivos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EventoArchivos == null)
            {
                return NotFound();
            }

            var eventoArchivo = await _context.EventoArchivos
                .Include(e => e.IdEventoNavigation)
                .FirstOrDefaultAsync(m => m.IdEventoArchivo == id);
            if (eventoArchivo == null)
            {
                return NotFound();
            }

            return View(eventoArchivo);
        }

        // POST: EventoArchivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EventoArchivos == null)
            {
                return Problem("Entity set 'InvitacionesContext.EventoArchivos'  is null.");
            }
            var eventoArchivo = await _context.EventoArchivos.FindAsync(id);
            if (eventoArchivo != null)
            {
                _context.EventoArchivos.Remove(eventoArchivo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoArchivoExists(int id)
        {
          return _context.EventoArchivos.Any(e => e.IdEventoArchivo == id);
        }
    }
}
