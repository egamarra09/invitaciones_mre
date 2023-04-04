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
    public class UsuariosController : Controller
    {
        private readonly InvitacionesContext _context;
        private readonly ILogger<HomeController> _logger;
        public UsuariosController(InvitacionesContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            //_logger.LogInformation("");
            var invitacionesContext = _context.Usuarios.Include(u => u.IdUsuarioTipoNavigation);
            ViewData["IdUsuarioTipo"] = new SelectList(_context.UsuariosTipos, "IdUsuarioTipo", "UsuarioTipo");
            return View(await invitacionesContext.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdUsuarioTipoNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["IdUsuarioTipo"] = new SelectList(_context.UsuariosTipos, "IdUsuarioTipo", "UsuarioTipo");
            //_logger.LogInformation("");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,IdUsuarioTipo,Activo,Login,Nombres,Apellidos,Email,FechaCreacion,UltimaModificacion")] UsuarioModel usuario)
        {
            ViewData["IdUsuarioTipo"] = new SelectList(_context.UsuariosTipos, "IdUsuarioTipo", "IdUsuarioTipo", usuario.IdUsuarioTipo);
            try
            {
                if (ModelState.IsValid)
                {

                    _context.Add(new Usuario
                    {
                        Activo = usuario.Activo,
                        Apellidos = usuario.Apellidos,
                        Email = usuario.Email,
                        FechaCreacion = DateTime.Now,
                        IdUsuarioTipo = usuario.IdUsuarioTipo,
                        Login = usuario.Login,
                        Nombres = usuario.Nombres,
                        UltimaModificacion = DateTime.Now
                    });
                    //_logger.LogInformation(JsonSerializer.Serialize(usuario));
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(usuario);
            }
            
            
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["IdUsuarioTipo"] = new SelectList(_context.UsuariosTipos, "IdUsuarioTipo", "UsuarioTipo");
            //_logger.LogInformation(JsonSerializer.Serialize(usuario));
            return PartialView(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,IdUsuarioTipo,Activo,Login,Nombres,Apellidos,Email,FechaCreacion,UltimaModificacion")] UsuarioModel m)
        {
            ViewData["IdUsuarioTipo"] = new SelectList(_context.UsuariosTipos, "IdUsuarioTipo", "UsuarioTipo");
            Usuario usuario = _context.Usuarios.Find(id);
            try
            {
                if (id != m.IdUsuario)
                {
                    return NotFound();
                }
               
                if (ModelState.IsValid)
                {
                    try
                    {
                        usuario.Nombres = m.Nombres;
                        usuario.Apellidos = m.Apellidos;
                        usuario.Activo = m.Activo;
                        usuario.IdUsuarioTipo = m.IdUsuarioTipo;
                        usuario.Email = m.Email;
                        usuario.Login = m.Login;
                        _context.Update(usuario);
                        //_logger.LogInformation(JsonSerializer.Serialize(m));
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UsuarioExists(usuario.IdUsuario))
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
                return View(usuario);
            }
            
            
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdUsuarioTipoNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {

                if (_context.Usuarios == null)
                {
                    return Problem("Entity set 'invitacionesContext.Usuarios'  is null.");
                }
                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario != null)
                {
                    _context.Usuarios.Remove(usuario);
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

        private bool UsuarioExists(int id)
        {
          return (_context.Usuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }
    }
}
