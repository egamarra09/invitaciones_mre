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
    public class EventoInvitadosController : Controller
    {
        private readonly InvitacionesContext _context;

        public EventoInvitadosController(InvitacionesContext context)
        {
            _context = context;
        }

        // GET: EventoInvitados
        public async Task<IActionResult> Index()
        {
            var invitacionesContext = _context.EventoInvitados.Include(e => e.IdEventoInvitacionNavigation).Include(e => e.IdInstitucionNavigation);
            return View(await invitacionesContext.ToListAsync());
        }

        // GET: EventoInvitados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EventoInvitados == null)
            {
                return NotFound();
            }

            var eventoParticipante = await _context.EventoInvitados
                .Include(e => e.IdEventoInvitacionNavigation)
                .Include(e => e.IdInstitucionNavigation)
                .FirstOrDefaultAsync(m => m.IdEventoParticipante == id);
            if (eventoParticipante == null)
            {
                return NotFound();
            }

            return View(eventoParticipante);
        }

        // GET: EventoInvitados/Create
        public IActionResult Create()
        {
            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "Evento1");
            ViewData["IdInstitucion"] = new SelectList(_context.Instituciones, "IdInstitucion", "Nombre");
            return View();
        }

        // POST: EventoInvitados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEventoParticipante,IdEvento,IdInstitucion,Nombre,Telefono,Email,Cargo")] EventoParticipante eventoParticipante)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventoParticipante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "Evento1", eventoParticipante.IdEvento);
            ViewData["IdInstitucion"] = new SelectList(_context.Instituciones, "IdInstitucion", "Nombre", eventoParticipante.IdInstitucion);
            return View(eventoParticipante);
        }

        // GET: EventoInvitados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EventoInvitados == null)
            {
                return NotFound();
            }

            var eventoParticipante = await _context.EventoInvitados.FindAsync(id);
            if (eventoParticipante == null)
            {
                return NotFound();
            }
            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "Evento1", eventoParticipante.IdEventoInvitacionNavigation.IdEvento);
            ViewData["IdInstitucion"] = new SelectList(_context.Instituciones, "IdInstitucion", "Nombre", eventoParticipante.IdInstitucion);
            return View(eventoParticipante);
        }

        // POST: EventoInvitados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEventoParticipante,IdEvento,IdInstitucion,Nombre,Telefono,Email,Cargo")] EventoParticipante eventoParticipante)
        {
            if (id != eventoParticipante.IdEventoParticipante)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventoParticipante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoParticipanteExists(eventoParticipante.IdEventoParticipante))
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
            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "Evento1", eventoParticipante.IdEvento);
            ViewData["IdInstitucion"] = new SelectList(_context.Instituciones, "IdInstitucion", "Nombre", eventoParticipante.IdInstitucion);
            return View(eventoParticipante);
        }

        // GET: EventoInvitados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EventoInvitados == null)
            {
                return NotFound();
            }

            var eventoParticipante = await _context.EventoInvitados
                .Include(e => e.IdEventoInvitacionNavigation)
                .Include(e => e.IdInstitucionNavigation)
                .FirstOrDefaultAsync(m => m.IdEventoParticipante == id);
            if (eventoParticipante == null)
            {
                return NotFound();
            }

            return View(eventoParticipante);
        }

        // POST: EventoInvitados/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EventoInvitados == null)
            {
                return Problem("Entity set 'InvitacionesContext.EventoInvitados'  is null.");
            }
            var eventoParticipante = await _context.EventoInvitados.FindAsync(id);
            if (eventoParticipante != null)
            {
                _context.EventoInvitados.Remove(eventoParticipante);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoParticipanteExists(int id)
        {
          return _context.EventoInvitados.Any(e => e.IdEventoParticipante == id);
        }
    }
}
