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
    public class MenusController : Controller
    {
        private readonly InvitacionesContext _context;
        private readonly IRepository _repository;
        private readonly ILogger<HomeController> _logger;
        public MenusController(InvitacionesContext context, IRepository repository, ILogger<HomeController> logger)
        {
            _context = context;
            _repository = repository;
            _logger = logger;   
        }
        //public MenusController()
        //{
        //    this._repository = new Repository(new InvitacionesContext());
        //}
        // GET: Menus
        public async Task<IActionResult> Index()
        {
            MenuModel m = new
                MenuModel();
            m.List = _repository.GetMenuList();
            //_logger.LogInformation("");
            ViewData["MenuMod"] = m;
              return _context.Menus != null ? 
                          View(await _context.Menus.ToListAsync()) :
                          Problem("Entity set 'invitacionesContext.Menus'  is null.");
        }

        // GET: Menus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.IdMenu == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Menus/Create
        public IActionResult Create()
        {
            //Repository r = new Repository(_context);
            //_logger.LogInformation("");
            MenuModel m = new
                MenuModel();
            try
            {
               
                m.List = _repository.GetMenuList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(m);
            }
         
            return View(m);
        }

        // POST: Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMenu,IdMenuSuperior,MenuTitulo,Link,Orden,Activo")] Menu menu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(menu);
                    //_logger.LogInformation(JsonSerializer.Serialize(menu));
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View(menu);
            }
           
            return View(menu);
        }

        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FindAsync(id);
           
            if (menu == null)
            {
                return NotFound();
            }
            MenuModel m = new MenuModel
            {
                Activo = menu.Activo,
                IdMenu = menu.IdMenu,
                IdMenuSuperior = menu.IdMenuSuperior,
                Link = menu.Link,
                List = _repository.GetMenuList(),
                MenuTitulo = menu.MenuTitulo,
                Orden = menu.Orden
            };
            //_logger.LogInformation(JsonSerializer.Serialize(m));
            return PartialView(m);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMenu,IdMenuSuperior,MenuTitulo,Link,Orden,Activo")] Menu menu)
        {
            try
            {
                if (id != menu.IdMenu)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(menu);
                        await _context.SaveChangesAsync();
                        //_logger.LogInformation(JsonSerializer.Serialize(menu));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!MenuExists(menu.IdMenu))
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
                return View(menu);
            }
            
            return View(menu);
        }

        // GET: Menus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .FirstOrDefaultAsync(m => m.IdMenu == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Menus/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Menus == null)
            {
                return Problem("Entity set 'invitacionesContext.Menus'  is null.");
            }
            var menu = await _context.Menus.FindAsync(id);
            if (menu != null)
            {
                _context.Menus.Remove(menu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenuExists(int id)
        {
          return (_context.Menus?.Any(e => e.IdMenu == id)).GetValueOrDefault();
        }
    }
}
