using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Invitaciones.Shared.Data;

namespace Invitaciones.Admin.Models
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly InvitacionesContext db;

        public MenuViewComponent(InvitacionesContext context) => db = context;

        //public async Task<IViewComponentResult> InvokeAsync()                                               )
        //{
        //    var tipo = HttpContext.Session.GetInt32("tipoUsuario") != null ? HttpContext.Session.GetInt32("tipoUsuario") : 1;

        //    var menu = db.MenuUsuariosTipos.Where(q => q.IdUsuarioTipo == tipo);
        //return ViewCo
        //}

        public async Task<IViewComponentResult> InvokeAsync(
                                             )
        {
            var tipo = HttpContext.Session.GetInt32("UsTipo") != null ? HttpContext.Session.GetInt32("UsTipo") : 0;
            var menu = db.MenuUsuariosTipos.Where(q => q.IdUsuarioTipo == tipo);
            //db.Entry(MenuUsuariosTipos).Reference(s=>s.).Load();
            //return ViewCo
            return View(menu);
        }
    }
}
