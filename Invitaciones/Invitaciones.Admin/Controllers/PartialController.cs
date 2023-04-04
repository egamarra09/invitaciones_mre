using Microsoft.AspNetCore.Mvc;
using Invitaciones.Shared.Data;
using Invitaciones.Admin.Models;

namespace Invitaciones.Admin.Controllers
{
    public class PartialController : Controller
    {
        private readonly InvitacionesContext _context;
        private readonly IRepository _repository;
        private readonly ILogger<PartialController> _logger;
        public PartialController(InvitacionesContext context, IRepository repository, ILogger<PartialController> logger)
        {
            _context = context;
            _repository = repository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult MainMenu()
        {
            var tipo = HttpContext.Session.GetInt32("tipoUsuario") != null ? HttpContext.Session.GetInt32("tipoUsuario") : 1 ;

            var menu = _context.MenuUsuariosTipos.Where(q => q.IdUsuarioTipo == tipo);
            ////Get the menuItems collection from somewhere
            //if (menuItems != null || menuItems.Count > 0)
            //{
            //    return View(menuItems);
            //}
            return PartialView(menu);
        }
    }
}
