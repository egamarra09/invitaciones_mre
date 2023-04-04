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
    public class MenuUsuariosTiposController : Controller
    {
        private readonly InvitacionesContext _context;
        private readonly ILogger<HomeController> _logger;
        public MenuUsuariosTiposController(InvitacionesContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;   
        }

        // GET: MenuUsuariosTipos
        public async Task<IActionResult> Index()
        {
            ViewData["IdMenu"] = new SelectList(_context.Menus, "IdMenu", "MenuTitulo");
            ViewData["IdUsuarioTipo"] = new SelectList(_context.UsuariosTipos, "IdUsuarioTipo", "UsuarioTipo");
           // _logger.LogInformation("");
            var invitacionesContext = _context.MenuUsuariosTipos.Include(m => m.IdMenuNavigation).Include(m => m.IdUsuarioTipoNavigation);
            return View(await _context.MenuUsuariosTipos.ToListAsync());
        }

        // GET: MenuUsuariosTipos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MenuUsuariosTipos == null)
            {
                return NotFound();
            }

            var menuUsuariosTipo = await _context.MenuUsuariosTipos
                .Include(m => m.IdMenuNavigation)
                .Include(m => m.IdUsuarioTipoNavigation)
                .FirstOrDefaultAsync(m => m.IdMenuTipoUsuario == id);
            if (menuUsuariosTipo == null)
            {
                return NotFound();
            }

            return View(menuUsuariosTipo);
        }

        // GET: MenuUsuariosTipos/Create
        public IActionResult Create()
        {
            ViewData["IdMenu"] = new SelectList(_context.Menus, "IdMenu", "MenuTitulo");
            ViewData["IdUsuarioTipo"] = new SelectList(_context.UsuariosTipos, "IdUsuarioTipo", "UsuarioTipo");
            //_logger.LogInformation("");
            return View();
        }

        // POST: MenuUsuariosTipos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMenuTipoUsuario,IdMenu,IdUsuarioTipo")] MenuUsuariosTiposModel menuUsuariosTipo)
        {
            ViewData["IdMenu"] = new SelectList(_context.Menus.Where(q=>q.Activo), "IdMenu", "IdMenu", menuUsuariosTipo.IdMenu);
            ViewData["IdUsuarioTipo"] = new SelectList(_context.UsuariosTipos, "IdUsuarioTipo", "IdUsuarioTipo", menuUsuariosTipo.IdUsuarioTipo);
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(new MenuUsuariosTipo
                    {
                        IdMenu = menuUsuariosTipo.IdMenu,
                        IdUsuarioTipo = menuUsuariosTipo.IdUsuarioTipo,
                    });
                    await _context.SaveChangesAsync();
                   // _logger.LogInformation(JsonSerializer.Serialize(menuUsuariosTipo));
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(menuUsuariosTipo);
            }
            
            
            return View(menuUsuariosTipo);
        }

        // GET: MenuUsuariosTipos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MenuUsuariosTipos == null)
            {
                return NotFound();
            }

            var menuUsuariosTipo = await _context.MenuUsuariosTipos.FindAsync(id);
            if (menuUsuariosTipo == null)
            {
                return NotFound();
            }
            ViewData["IdMenu"] = new SelectList(_context.Menus, "IdMenu", "MenuTitulo");
            ViewData["IdUsuarioTipo"] = new SelectList(_context.UsuariosTipos, "IdUsuarioTipo", "UsuarioTipo");
           // _logger.LogInformation(JsonSerializer.Serialize(menuUsuariosTipo));
            return PartialView(menuUsuariosTipo);
        }

        // POST: MenuUsuariosTipos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMenuTipoUsuario,IdMenu,IdUsuarioTipo")] MenuUsuariosTipo menuUsuariosTipo)
        {
            ViewData["IdMenu"] = new SelectList(_context.Menus.Where(q => q.Activo), "IdMenu", "IdMenu", menuUsuariosTipo.IdMenu);
            ViewData["IdUsuarioTipo"] = new SelectList(_context.UsuariosTipos, "IdUsuarioTipo", "IdUsuarioTipo", menuUsuariosTipo.IdUsuarioTipo);
            try
            {
                if (id != menuUsuariosTipo.IdMenuTipoUsuario)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(menuUsuariosTipo);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!MenuUsuariosTipoExists(menuUsuariosTipo.IdMenuTipoUsuario))
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
                return View(menuUsuariosTipo);
            }
            
            
            //_logger.LogInformation(JsonSerializer.Serialize(menuUsuariosTipo));
            return View(menuUsuariosTipo);
        }

        // GET: MenuUsuariosTipos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MenuUsuariosTipos == null)
            {
                return NotFound();
            }

            var menuUsuariosTipo = await _context.MenuUsuariosTipos
                .Include(m => m.IdMenuNavigation)
                .Include(m => m.IdUsuarioTipoNavigation)
                .FirstOrDefaultAsync(m => m.IdMenuTipoUsuario == id);
            if (menuUsuariosTipo == null)
            {
                return NotFound();
            }

            return View(menuUsuariosTipo);
        }

        // POST: MenuUsuariosTipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MenuUsuariosTipos == null)
            {
                return Problem("Entity set 'InvitacionesContext.MenuUsuariosTipos'  is null.");
            }
            var menuUsuariosTipo = await _context.MenuUsuariosTipos.FindAsync(id);
            if (menuUsuariosTipo != null)
            {
                _context.MenuUsuariosTipos.Remove(menuUsuariosTipo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuUsuariosTipoExists(int id)
        {
          return (_context.MenuUsuariosTipos?.Any(e => e.IdMenuTipoUsuario == id)).GetValueOrDefault();
        }
    }
}
