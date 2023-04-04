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
	public class EventosController : Controller
	{
		private readonly InvitacionesContext _context;
		private readonly ILogger<EventosController> _logger;
		private readonly IRepository _repository;
		//private Microsoft.Extensions.Hosting.IHostingEnvironment hostingEnv;
		private readonly IWebHostEnvironment hostingEnv;
		public EventosController(InvitacionesContext context, ILogger<EventosController> logger, IRepository repository, IWebHostEnvironment hostingEnv)
		{
			_context = context;
			_logger = logger;
			_repository = repository;
			this.hostingEnv = hostingEnv;

		}

		// GET: Eventoes
		public async Task<IActionResult> Index()
		{
			var invitacionesContext = _context.Eventos.Include(e => e.IdCategoriaNavigation).Include(e => e.IdOrganizadorNavigation).Include(e => e.IdTipoNavigation).OrderByDescending(o=>o.IdEvento);
			try
			{
				ViewData["IdCategoria"] = new SelectList(_context.EventoCategorias.Where(q=>q.Activo), "IdCategoria", "Nombre");
				ViewData["IdOrganizador"] = new SelectList(_context.EventoOrganizadores.Where(q => q.Activo), "IdOrganizador", "Nombre");
				ViewData["IdTipo"] = new SelectList(_context.EventoTipos.Where(q => q.Activo), "IdTipo", "Tipo");
				ViewData["Formato"] = new SelectList(_repository.FormatoEventos(), "Value", "Text");
				ViewData["zonaHoraria"] = new SelectList(_repository.zonaHoraria(), "Value", "Text");
				//_logger.LogInformation("");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return View(await invitacionesContext.ToListAsync());
			}
			return View(await invitacionesContext.ToListAsync());
		}

		// GET: Eventoes/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Eventos == null)
			{
				return NotFound();
			}

			var evento = await _context.Eventos
				.Include(e => e.IdCategoriaNavigation)
				.Include(e => e.IdOrganizadorNavigation)
				.FirstOrDefaultAsync(m => m.IdEvento == id);
			if (evento == null)
			{
				return NotFound();
			}

			return View(evento);
		}

		// GET: Eventoes/Create
		public IActionResult Create()
		{
			HttpContext.Session.Set<List<UploadItem>>("uploadItems", new List<UploadItem>());
			//var j= new List<UploadItem>();
			//TempData["uploadItems"] = j;
			//HttpContext.Session.Set<DateTime>(SessionKeyTime, currentTime);
			ViewData["IdCategoria"] = new SelectList(_context.EventoCategorias.Where(q => q.Activo), "IdCategoria", "Nombre");
			ViewData["IdOrganizador"] = new SelectList(_context.EventoOrganizadores.Where(q => q.Activo), "IdOrganizador", "Nombre");
			ViewData["IdTipo"] = new SelectList(_context.EventoTipos.Where(q => q.Activo), "IdTipo", "Tipo");
			ViewData["Formato"] = new SelectList(_repository.FormatoEventos(), "Value", "Text");
			ViewData["zonaHoraria"] = new SelectList(_repository.zonaHoraria(), "Value", "Text");
			ViewData["Repetir"] = new SelectList(_repository.RepetirItems(), "Value", "Text");
			// _logger.LogInformation("");
			return View();
		}
		public async Task<IActionResult> Upload()

		{



			return View();

		}
		[HttpPost]
		public async Task<IActionResult> Upload(IFormFile file)

		{
			var value = HttpContext.Session.Get<List<UploadItem>>("uploadItems");
			//archivo = new List<string>();
			var FileDic = "Files";

			string FilePath = Path.Combine(hostingEnv.WebRootPath, FileDic);

			if (!Directory.Exists(FilePath))

				Directory.CreateDirectory(FilePath);

			var fileName = file.FileName;

			var filePath = Path.Combine(FilePath, fileName);

			using (FileStream fs = System.IO.File.Create(filePath))

			{

				file.CopyTo(fs);

			}
			value.Add(new UploadItem { nombre = fileName });
			HttpContext.Session.Set<List<UploadItem>>("uploadItems", value);
			///HttpContext.Session.Clear();
			return RedirectToAction(nameof(Index));

		}
		// POST: Eventoes/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(/*[Bind("IdEvento,IdOrganizador,IdCategoria,Evento1,FechaDesde,FechaHasta,Latitud,Longitug,ZonaHoraria,Formato,LimitePersonas,HoraDesde,HoraHasta,Descripcion,Activo,FechaCreacion,UltimaModificacion")]*/ Evento evento, IFormFile Imagen, IFormCollection fc)
		{
			ViewData["IdCategoria"] = new SelectList(_context.EventoCategorias.Where(q => q.Activo), "IdCategoria", "Nombre");
			ViewData["IdOrganizador"] = new SelectList(_context.EventoOrganizadores.Where(q => q.Activo), "IdOrganizador", "Nombre");
			ViewData["IdTipo"] = new SelectList(_context.EventoTipos.Where(q => q.Activo), "IdTipo", "Tipo");
			ViewData["Formato"] = new SelectList(_repository.FormatoEventos(), "Value", "Text");
			ViewData["Repetir"] = new SelectList(_repository.RepetirItems(), "Value", "Text");
			ViewData["zonaHoraria"] = new SelectList(_repository.zonaHoraria(), "Value", "Text");
			try
			{

				if (ModelState.IsValid)
				{
					if (Imagen != null)
					{
						var FileDic = "Files";

						string FilePath = Path.Combine(hostingEnv.WebRootPath, FileDic);

						if (!Directory.Exists(FilePath))

							Directory.CreateDirectory(FilePath);

						var fileName = Imagen.FileName;

						var filePath = Path.Combine(FilePath, fileName);

						using (FileStream fs = System.IO.File.Create(filePath))

						{

							Imagen.CopyTo(fs);

						}
						evento.Imagen = fileName;
					}

					_context.Add(evento);

					await _context.SaveChangesAsync();
					var value = HttpContext.Session.Get<List<UploadItem>>("uploadItems");
					foreach (UploadItem item in value)
					{
						_context.Add(new EventoArchivo
						{
							Activo = true,
							IdEvento = evento.IdEvento,
							Tipo = "",
							Archivo = item.nombre
						});
					}
					if (!String.IsNullOrEmpty(fc["fdes"]))
					{
						for (int i = 0; i < fc["fdes"].Count; i++)
						{
							_context.Add(new EventoFecha
							{
								Activo = true,
								IdEvento = evento.IdEvento,
								FechaDesde = Convert.ToDateTime(fc["fdes"][i].ToString()),
								FechaHasta = Convert.ToDateTime(fc["fhas"][i].ToString()),
								HoraDesde = fc["hdes"][i].ToString(),
								HoraHasta = fc["hhas"][i].ToString(),
							});
						}
						if (_context.EventoFechas.Local != null)
						{
							var d = _context.EventoFechas.Local.OrderBy(q => q.FechaDesde).FirstOrDefault();
							var h = _context.EventoFechas.Local.OrderByDescending(q => q.FechaHasta).FirstOrDefault();
							if (d != null)
								evento.FechaInicio = d.FechaDesde;
							if (h != null)
								evento.FechaFin = h.FechaHasta;
						}
					}
					_context.Add(new EventoInvitacione
					{
						IdEvento = evento.IdEvento,
						Activo = true,
					});
					await _context.SaveChangesAsync();
					HttpContext.Session.Set<List<UploadItem>>("uploadItems", new List<UploadItem>());
					//_logger.LogInformation(JsonSerializer.Serialize(evento));
					return RedirectToAction(nameof(Index));
				}

			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return View(evento);
			}

			return View(evento);
		}

		// GET: Eventoes/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			HttpContext.Session.Set<List<UploadItem>>("uploadItems", new List<UploadItem>());
			if (id == null || _context.Eventos == null)
			{
				return NotFound();
			}

			var evento = await _context.Eventos.FindAsync(id);
			if (evento == null)
			{
				return NotFound();
			}
			ViewData["IdCategoria"] = new SelectList(_context.EventoCategorias, "IdCategoria", "Nombre");
			ViewData["IdOrganizador"] = new SelectList(_context.EventoOrganizadores, "IdOrganizador", "Nombre");
			ViewData["IdTipo"] = new SelectList(_context.EventoTipos, "IdTipo", "Tipo");
			ViewData["Formato"] = new SelectList(_repository.FormatoEventos(), "Value", "Text");
			ViewData["Repetir"] = new SelectList(_repository.RepetirItems(), "Value", "Text");
			ViewData["zonaHoraria"] = new SelectList(_repository.zonaHoraria(), "Value", "Text");
			//_logger.LogInformation("");
			return PartialView(evento);
		}

		// POST: Eventoes/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, /*[Bind("IdEvento,IdOrganizador,IdCategoria,Evento1,FechaDesde,FechaHasta,Latitud,Longitug,ZonaHoraria,Formato,LimitePersonas,HoraDesde,HoraHasta,Descripcion,Activo,FechaCreacion,UltimaModificacion")]*/ Evento evento, IFormFile Imagen, IFormCollection fc)
		{
			ViewData["IdCategoria"] = new SelectList(_context.EventoCategorias.Where(q => q.Activo), "IdCategoria", "Nombre");
			ViewData["IdOrganizador"] = new SelectList(_context.EventoOrganizadores.Where(q => q.Activo), "IdOrganizador", "Nombre");
			ViewData["IdTipo"] = new SelectList(_context.EventoTipos.Where(q => q.Activo), "IdTipo", "Tipo");
			ViewData["Formato"] = new SelectList(_repository.FormatoEventos(), "Value", "Text");
			ViewData["Repetir"] = new SelectList(_repository.RepetirItems(), "Value", "Text");
			ViewData["zonaHoraria"] = new SelectList(_repository.zonaHoraria(), "Value", "Text");
			try
			{
				if (id != evento.IdEvento)
				{
					return NotFound();
				}

				if (ModelState.IsValid)
				{
					try
					{
						if (Imagen != null)
						{
							var FileDic = "Files";

							string FilePath = Path.Combine(hostingEnv.WebRootPath, FileDic);

							if (!Directory.Exists(FilePath))

								Directory.CreateDirectory(FilePath);

							var fileName = Imagen.FileName;

							var filePath = Path.Combine(FilePath, fileName);

							using (FileStream fs = System.IO.File.Create(filePath))

							{

								Imagen.CopyTo(fs);

							}
							evento.Imagen = fileName;
						}
						_context.Update(evento);
						await _context.SaveChangesAsync();
						var value = HttpContext.Session.Get<List<UploadItem>>("uploadItems");
						foreach (UploadItem item in value)
						{
							_context.Add(new EventoArchivo
							{
								Activo = true,
								IdEvento = evento.IdEvento,
								Tipo = "",
								Archivo = item.nombre
							});
						}
						var ef = _context.EventoFechas.Where(q => q.IdEvento == evento.IdEvento).ToList();
						_context.EventoFechas.RemoveRange(ef);
						await _context.SaveChangesAsync();
						if (!String.IsNullOrEmpty(fc["fdes"]))
						{
							for (int i = 0; i < fc["fdes"].Count; i++)
							{
								_context.Add(new EventoFecha
								{
									Activo = true,
									IdEvento = evento.IdEvento,
									FechaDesde = Convert.ToDateTime(fc["fdes"][i].ToString()),
									FechaHasta = Convert.ToDateTime(fc["fhas"][i].ToString()),
									HoraDesde = fc["hdes"][i].ToString(),
									HoraHasta = fc["hhas"][i].ToString(),
								});
							}
						}

						await _context.SaveChangesAsync();
						HttpContext.Session.Set<List<UploadItem>>("uploadItems", new List<UploadItem>());
						//_logger.LogInformation(JsonSerializer.Serialize(evento));
					}
					catch (DbUpdateConcurrencyException)
					{
						if (!EventoExists(evento.IdEvento))
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
				return View(evento);
			}


			return View(evento);
		}

		// GET: Eventoes/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Eventos == null)
			{
				return NotFound();
			}

			var evento = await _context.Eventos
				.Include(e => e.IdCategoriaNavigation)
				.Include(e => e.IdOrganizadorNavigation)
				.FirstOrDefaultAsync(m => m.IdEvento == id);
			if (evento == null)
			{
				return NotFound();
			}

			return View(evento);
		}

		// POST: Eventoes/Delete/5
		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			try
			{

				if (_context.Eventos == null)
				{
					return Problem("Entity set 'InvitacionesContext.Eventos'  is null.");
				}
				var evento = await _context.Eventos.FindAsync(id);
				if (evento != null)
				{
					_context.Eventos.Remove(evento);
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

		private bool EventoExists(int id)
		{
			return (_context.Eventos?.Any(e => e.IdEvento == id)).GetValueOrDefault();
		}
	}
	public class UploadItem
	{
		public string nombre { get; set; }

	}
}
