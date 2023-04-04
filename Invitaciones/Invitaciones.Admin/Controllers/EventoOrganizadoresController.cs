using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Invitaciones.Shared.Data;
using Invitaciones.Admin.Models;

namespace Invitaciones.Admin.Controllers
{
    [CustomAuthorizeAttribute]
    public class EventoOrganizadoresController : Controller
    {
        private readonly InvitacionesContext _context;
        private readonly ILogger<EventoOrganizadoresController> _logger;

        public EventoOrganizadoresController(InvitacionesContext context, ILogger<EventoOrganizadoresController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: EventoOrganizadores
        public async Task<IActionResult> Index()
        {
            //_logger.LogInformation("");
            return _context.EventoOrganizadores != null ? 
                          View(await _context.EventoOrganizadores.ToListAsync()) :
                          Problem("Entity set 'InvitacionesContext.EventoOrganizadores'  is null.");
        }

        // GET: EventoOrganizadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EventoOrganizadores == null)
            {
                return NotFound();
            }

            var eventoOrganizadore = await _context.EventoOrganizadores
                .FirstOrDefaultAsync(m => m.IdOrganizador == id);
            if (eventoOrganizadore == null)
            {
                return NotFound();
            }

            return View(eventoOrganizadore);
        }

        // GET: EventoOrganizadores/Create
        public IActionResult Create()
        {
            //_logger.LogInformation("");
            return View();
        }

        // POST: EventoOrganizadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOrganizador,Nombre,Activo,FechaCreacion,UltimaModificacion")] EventoOrganizadore eventoOrganizadore)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(eventoOrganizadore);
                    await _context.SaveChangesAsync();
                    //_logger.LogInformation(JsonSerializer.Serialize(eventoOrganizadore));
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(eventoOrganizadore);
            }
         
            return View(eventoOrganizadore);
        }

        // GET: EventoOrganizadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //_logger.LogInformation("");
            if (id == null || _context.EventoOrganizadores == null)
            {
                return NotFound();
            }

            var eventoOrganizadore = await _context.EventoOrganizadores.FindAsync(id);
            if (eventoOrganizadore == null)
            {
                return NotFound();
            }
            return PartialView(eventoOrganizadore);
        }

        // POST: EventoOrganizadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOrganizador,Nombre,Activo,FechaCreacion,UltimaModificacion")] EventoOrganizadore eventoOrganizadore)
        {
            try
            {
                if (id != eventoOrganizadore.IdOrganizador)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(eventoOrganizadore);
                        await _context.SaveChangesAsync();
                       // _logger.LogInformation(JsonSerializer.Serialize(eventoOrganizadore));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EventoOrganizadoreExists(eventoOrganizadore.IdOrganizador))
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
                return View(eventoOrganizadore);
            }
            
            return View(eventoOrganizadore);
        }

        // GET: EventoOrganizadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EventoOrganizadores == null)
            {
                return NotFound();
            }

            var eventoOrganizadore = await _context.EventoOrganizadores
                .FirstOrDefaultAsync(m => m.IdOrganizador == id);
            if (eventoOrganizadore == null)
            {
                return NotFound();
            }

            return View(eventoOrganizadore);
        }

        // POST: EventoOrganizadores/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                if (_context.EventoOrganizadores == null)
                {
                    return Problem("Entity set 'InvitacionesContext.EventoOrganizadores'  is null.");
                }
                var eventoOrganizadore = await _context.EventoOrganizadores.FindAsync(id);
                if (eventoOrganizadore != null)
                {
                    _context.EventoOrganizadores.Remove(eventoOrganizadore);
                }

                await _context.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return RedirectToAction(nameof(Index));
            }

            
            return RedirectToAction(nameof(Index));
        }

        private bool EventoOrganizadoreExists(int id)
        {
          return (_context.EventoOrganizadores?.Any(e => e.IdOrganizador == id)).GetValueOrDefault();
        }
    }
}
