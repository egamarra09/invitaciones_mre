using Invitaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Invitaciones.Shared.Data;
using System.Diagnostics;

namespace Invitaciones.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly InvitacionesContext _context;
		public HomeController(ILogger<HomeController> logger, InvitacionesContext context)
		{
			_logger = logger;
			_context = context;
		}

		//public IActionResult Index()
		//{
		//    List<MRE_Invitaciones.Data.Evento> eventos = _context.Eventos.ToList();
		//    return View(eventos);
		//}
		[HttpGet]
		[Route("/Confirmacion/{id?}")]
		public ActionResult Confirmacion(string id)
		{
			var e = _context.EventoInvitados.Where(q => q.Token == id).FirstOrDefault();
			ViewBag.token = id;
			//MRE_Invitaciones.Data.EventoInvitacione in = e
			return View(e);

		}
		[HttpGet]
		[Route("/Confirmacion2/{token?}")]
		public async Task<IActionResult> Confirmacion2(string token)
		{
			//var id = fc["IdInvitacion"];
			var e = _context.EventoInvitados.Where(q => q.Token == token).FirstOrDefault();
			ViewBag.token = token;

			e.Confirmado = true;
			e.FechaConfirmacion = DateTime.Now;
			_context.Update(e);
			await _context.SaveChangesAsync();
			return View(e);

		}

		[HttpGet]
		public ActionResult Index()
		{
			return View(new EventoViewModel());
		}

		[HttpGet]
		public ActionResult Evento(int id)
		{
			var e = _context.Eventos.Find(id);
			return View(e);
		}

		public JsonResult GetEvents(DateTime start, DateTime end)
		{
			var viewModel = new EventoViewModel();
			var events = new List<EventoViewModel>();
			start = DateTime.Today.AddDays(-14);
			end = DateTime.Today.AddDays(-11);
			var eventos = _context.Eventos.ToList();
			foreach (var e in eventos)
			{
				events.Add(new EventoViewModel()
				{
					id = e.IdEvento,
					title = e.Evento1,
					//start = e.FechaDesde.Value.ToString("yyyy-MM-dd"),
					//end = e.FechaHasta.Value.ToString("yyyy-MM-dd"),
					allDay = false,
					url = "/home/evento/" + e.IdEvento.ToString()
				});

				start = start.AddDays(7);
				end = end.AddDays(7);
			}


			return Json(events.ToArray());
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}