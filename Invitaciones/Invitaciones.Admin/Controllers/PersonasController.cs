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
    public class PersonasController : Controller
    {
        private readonly InvitacionesContext _context;
        private readonly ILogger<PersonasController> _logger;
        public PersonasController(InvitacionesContext context, ILogger<PersonasController> logger)
        {
            _context = context;
            _logger = logger;   
        }

        // GET: Personas
        public async Task<IActionResult> Index()
        {
            ViewData["IdInstitucion"] = new SelectList(_context.Instituciones, "IdInstitucion", "Nombre");
            ViewData["IdGrupo"] = new SelectList(_context.Grupos, "IdGrupo", "Nombre");
            return View(await _context.Personas.ToListAsync());
        }

        // GET: Personas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .FirstOrDefaultAsync(m => m.IdInvitado == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // GET: Personas/Create
        public IActionResult Create()
        {
            ViewData["IdInstitucion"] = new SelectList(_context.Instituciones, "IdInstitucion", "Nombre");
            ViewData["IdGrupo"] = new SelectList(_context.Grupos, "IdGrupo", "Nombre");
            return View();
        }

        // POST: Personas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("IdInvitado,IdGrupo,IdInstitucion,Nombre,Telefono,Celular,Email,Cargo")]*/ Persona persona)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _context.Add(persona);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

            }
            
            return View(persona);
        }

        // GET: Personas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }
            ViewData["IdInstitucion"] = new SelectList(_context.Instituciones, "IdInstitucion", "Nombre", persona.IdInstitucion);
            ViewData["IdGrupo"] = new SelectList(_context.Grupos.Where(q=>q.IdInstitucion==persona.IdInstitucion), "IdGrupo", "Nombre");
            return PartialView(persona);
        }

        // POST: Personas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, /*[Bind("IdInvitado,IdGrupo,IdInstitucion,Nombre,Telefono,Celular,Email,Cargo")]*/ Persona persona)
        {
       
            if (id != persona.IdInvitado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(persona);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonaExists(persona.IdInvitado))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                ViewData["IdInstitucion"] = new SelectList(_context.Instituciones, "IdInstitucion", "Nombre",persona.IdInstitucion);
                return RedirectToAction(nameof(Index));
            }
            return PartialView(persona);
        }

        // GET: Personas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Personas == null)
            {
                return NotFound();
            }

            var persona = await _context.Personas
                .FirstOrDefaultAsync(m => m.IdInvitado == id);
            if (persona == null)
            {
                return NotFound();
            }

            return View(persona);
        }

        // POST: Personas/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Personas == null)
            {
                return Problem("Entity set 'InvitacionesContext.Personas'  is null.");
            }
            var persona = await _context.Personas.FindAsync(id);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonaExists(int id)
        {
          return _context.Personas.Any(e => e.IdInvitado == id);
        }
    }
}
