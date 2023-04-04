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
    public class UsuariosTipoController : Controller
    {
        private readonly InvitacionesContext _context;
        private readonly ILogger<HomeController> _logger;
        public UsuariosTipoController(InvitacionesContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: UsuariosTipoes
        public async Task<IActionResult> Index()
        {
           // _logger.LogInformation("");
            return _context.UsuariosTipos != null ? 
                          View(await _context.UsuariosTipos.ToListAsync()) :
                          Problem("Entity set 'invitacionesContext.UsuariosTipos'  is null.");
        }

        // GET: UsuariosTipoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UsuariosTipos == null)
            {
                return NotFound();
            }

            var usuariosTipo = await _context.UsuariosTipos
                .FirstOrDefaultAsync(m => m.IdUsuarioTipo == id);
            if (usuariosTipo == null)
            {
                return NotFound();
            }

            return View(usuariosTipo);
        }

        // GET: UsuariosTipoes/Create
        public IActionResult Create()
        {
           //_logger.LogInformation("");
            return View();
        }

        // POST: UsuariosTipoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuarioTipo,UsuarioTipo")] UsuariosTipo usuariosTipo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(usuariosTipo);
                    await _context.SaveChangesAsync();
                    //_logger.LogInformation(JsonSerializer.Serialize(usuariosTipo));
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(usuariosTipo);
            }
            
            return View(usuariosTipo);
        }

        // GET: UsuariosTipoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UsuariosTipos == null)
            {
                return NotFound();
            }

            var usuariosTipo = await _context.UsuariosTipos.FindAsync(id);
            if (usuariosTipo == null)
            {
                return NotFound();
            }
            //_logger.LogInformation(JsonSerializer.Serialize(usuariosTipo));
            return PartialView(usuariosTipo);
        }

        // POST: UsuariosTipoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuarioTipo,UsuarioTipo")] UsuariosTipo usuariosTipo)
        {
            try
            {
                if (id != usuariosTipo.IdUsuarioTipo)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(usuariosTipo);
                        await _context.SaveChangesAsync();
                        // _logger.LogInformation(JsonSerializer.Serialize(usuariosTipo));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UsuariosTipoExists(usuariosTipo.IdUsuarioTipo))
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
                return View(usuariosTipo);
            }
           

            
            return View(usuariosTipo);
        }

        // GET: UsuariosTipoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UsuariosTipos == null)
            {
                return NotFound();
            }

            var usuariosTipo = await _context.UsuariosTipos
                .FirstOrDefaultAsync(m => m.IdUsuarioTipo == id);
            if (usuariosTipo == null)
            {
                return NotFound();
            }

            return View(usuariosTipo);
        }

        // POST: UsuariosTipoes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UsuariosTipos == null)
            {
                return Problem("Entity set 'invitacionesContext.UsuariosTipos'  is null.");
            }
            var usuariosTipo = await _context.UsuariosTipos.FindAsync(id);
            if (usuariosTipo != null)
            {
                _context.UsuariosTipos.Remove(usuariosTipo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuariosTipoExists(int id)
        {
          return (_context.UsuariosTipos?.Any(e => e.IdUsuarioTipo == id)).GetValueOrDefault();
        }
    }
}
