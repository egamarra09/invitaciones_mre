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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InvitacionesContext _context;
        
        private readonly IRepository _repository;
        //private Microsoft.Extensions.Hosting.IHostingEnvironment hostingEnv;
        private readonly IWebHostEnvironment hostingEnv;
        public HomeController(InvitacionesContext context, ILogger<HomeController> logger, IRepository repository, IWebHostEnvironment hostingEnv)
        {
            _context = context;
            _logger = logger;
            _repository = repository;
            this.hostingEnv = hostingEnv;

        }

        public IActionResult Index()
        {
            string userName = HttpContext.User.Identity.Name;
            var invitacionesContext = _context.Eventos.Include(e => e.IdCategoriaNavigation).Include(e => e.IdOrganizadorNavigation).Include(e => e.IdTipoNavigation).Include(e => e.EventoInvitaciones);
            int[] ev = new int[12];
            int[] ev2 = new int[12];
            try
            {
                //ViewData["IdCategoria"] = new SelectList(_context.EventoCategorias, "IdCategoria", "Nombre");
                //ViewData["IdOrganizador"] = new SelectList(_context.EventoOrganizadores, "IdOrganizador", "Nombre");
                //ViewData["IdTipo"] = new SelectList(_context.EventoTipos, "IdTipo", "Tipo");
                //ViewData["Formato"] = new SelectList(_repository.FormatoEventos(), "Value", "Text");
                //ViewData["zonaHoraria"] = new SelectList(_repository.zonaHoraria(), "Value", "Text");
                ViewData["eventos"] = invitacionesContext.OrderByDescending(o=>o.IdEvento).ToList();
                var evYear = invitacionesContext.Where(q => q.FechaInicio.HasValue).ToList();
                var evYear2 = evYear.Where(q => q.FechaInicio.Value.Year == DateTime.Now.Year-1).ToList();
                evYear = evYear.Where(q => q.FechaInicio.Value.Year == DateTime.Now.Year).ToList();
                for(int i=1; i <= ev.Length -1;i++)
                {
                    var c = evYear.Where(q => q.FechaInicio.Value.Month == i).Count();
                    ev[i] = c;
                }
                for (int i = 1; i <= ev2.Length - 1; i++)
                {
                    var c = evYear2.Where(q => q.FechaInicio.Value.Month == i).Count();
                    ev2[i] = c;
                }
                ViewData["eventos1"] = ev;
                ViewData["eventos2"] = ev2;
                //ViewData["eventos3"] = ev;
                //_logger.LogInformation("");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return View();
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public IActionResult Noauth()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}