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
    public class EventoCategoriasController : Controller
    {
        private readonly InvitacionesContext _context;
        private readonly ILogger<EventoCategoriasController> _logger;
        public EventoCategoriasController(InvitacionesContext context, ILogger<EventoCategoriasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: EventoCategorias
        public async Task<IActionResult> Index()
        {
            //_logger.LogInformation("");
            return _context.EventoCategorias != null ? 
                          View(await _context.EventoCategorias.ToListAsync()) :
                          Problem("Entity set 'InvitacionesContext.EventoCategorias'  is null.");
        }

        // GET: EventoCategorias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EventoCategorias == null)
            {
                return NotFound();
            }

            var eventoCategoria = await _context.EventoCategorias
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (eventoCategoria == null)
            {
                return NotFound();
            }

            return View(eventoCategoria);
        }

        // GET: EventoCategorias/Create
        public IActionResult Create()
        {
            //_logger.LogInformation("");
            return View();
        }

        // POST: EventoCategorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCategoria,Nombre,Activo,FechaCreacion,UltimaModificacion")] EventoCategoria eventoCategoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(eventoCategoria);
                    await _context.SaveChangesAsync();
                    //_logger.LogInformation(JsonSerializer.Serialize(eventoCategoria));
                    return RedirectToAction(nameof(Index));
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(eventoCategoria);
            }
            return View(eventoCategoria);
        }

        // GET: EventoCategorias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //_logger.LogInformation("");
            if (id == null || _context.EventoCategorias == null)
            {
                return NotFound();
            }

            var eventoCategoria = await _context.EventoCategorias.FindAsync(id);
            if (eventoCategoria == null)
            {
                return NotFound();
            }
            return PartialView(eventoCategoria);
        }

        // POST: EventoCategorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCategoria,Nombre,Activo,FechaCreacion,UltimaModificacion")] EventoCategoria eventoCategoria)
        {
            try
            {
                if (id != eventoCategoria.IdCategoria)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(eventoCategoria);
                        await _context.SaveChangesAsync();
                       // _logger.LogInformation(JsonSerializer.Serialize(eventoCategoria));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!EventoCategoriaExists(eventoCategoria.IdCategoria))
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
                return View(eventoCategoria);
            }
            
            return View(eventoCategoria);
        }

        // GET: EventoCategorias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EventoCategorias == null)
            {
                return NotFound();
            }

            var eventoCategoria = await _context.EventoCategorias
                .FirstOrDefaultAsync(m => m.IdCategoria == id);
            if (eventoCategoria == null)
            {
                return NotFound();
            }

            return View(eventoCategoria);
        }

        // POST: EventoCategorias/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EventoCategorias == null)
            {
                return Problem("Entity set 'InvitacionesContext.EventoCategorias'  is null.");
            }
            var eventoCategoria = await _context.EventoCategorias.FindAsync(id);
            if (eventoCategoria != null)
            {
                _context.EventoCategorias.Remove(eventoCategoria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoCategoriaExists(int id)
        {
          return (_context.EventoCategorias?.Any(e => e.IdCategoria == id)).GetValueOrDefault();
        }
    }
}
