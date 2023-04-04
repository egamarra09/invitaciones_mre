using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Invitaciones.Shared.Data;
using Invitaciones.Admin.Models;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using QRCoder;

namespace Invitaciones.Admin.Controllers
{
    [CustomAuthorizeAttribute]
    public class EventoInvitacionesController : Controller
    {
        private readonly InvitacionesContext _context;
        private readonly IWebHostEnvironment hostingEnv;
        private IConfiguration Configuration;
        private readonly ILogger<EventoInvitacionesController> _logger;
        public EventoInvitacionesController(InvitacionesContext context, IWebHostEnvironment hostingEnv, IConfiguration configuration, ILogger<EventoInvitacionesController> logger)
        {
            _context = context;
            this.hostingEnv = hostingEnv;
            Configuration = configuration;
            _logger = logger;   
        }

        // GET: EventoInvitaciones
        public async Task<IActionResult> Index()
        {
            //ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "Evento1");
            ViewData["Eventos"] = _context.Eventos.ToList();
            ViewData["IdInstitucion"] = new SelectList(_context.Instituciones, "IdInstitucion", "Nombre");
            return View(await _context.EventoInvitaciones.ToListAsync());
        }

        public async Task<IActionResult> Reporte(int id=0)
        {
            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "Evento1");
            //ViewData["Eventos"] = _context.Eventos.ToList();
            //ViewData["IdInstitucion"] = new SelectList(_context.Instituciones, "IdInstitucion", "Nombre");
            return View(await _context.EventoInvitaciones.Where(q=>q.IdInvitacion==id).FirstOrDefaultAsync());
        }

        // GET: EventoInvitaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EventoInvitaciones == null)
            {
                return NotFound();
            }

            var eventoInvitacione = await _context.EventoInvitaciones
                .FirstOrDefaultAsync(m => m.IdInvitacion == id);
            if (eventoInvitacione == null)
            {
                return NotFound();
            }

            return View(eventoInvitacione);
        }

        // GET: EventoInvitaciones/Create
        public IActionResult Create()
        {
            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "Evento1");
            return View();
        }

        // POST: EventoInvitaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInvitacion,IdEvento")] EventoInvitacione eventoInvitacione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventoInvitacione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventoInvitacione);
        }

        // GET: EventoInvitaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "Evento1");
            //ViewData["Eventos"] = _context.Eventos.ToList();
            ViewData["IdInstitucion"] = new SelectList(_context.Instituciones, "IdInstitucion", "Nombre");
            if (id == null || _context.EventoInvitaciones == null)
            {
                return NotFound();
            }

            var eventoInvitacione = await _context.EventoInvitaciones.FindAsync(id);
            if (eventoInvitacione == null)
            {
                return NotFound();
            }
            eventoInvitacione.Motivo = (String.IsNullOrEmpty(eventoInvitacione.Motivo) ? eventoInvitacione.IdEventoNavigation.Evento1 : eventoInvitacione.Motivo);
            eventoInvitacione.Saludo = (String.IsNullOrEmpty(eventoInvitacione.Saludo) ? "se complace en invitar" : eventoInvitacione.Saludo);
            eventoInvitacione.Fecha = (String.IsNullOrEmpty(eventoInvitacione.Fecha) ? Convert.ToDateTime(eventoInvitacione.IdEventoNavigation.FechaInicio).ToLongDateString() + " , " + eventoInvitacione.IdEventoNavigation.HoraInicio + " hs" : eventoInvitacione.Fecha);
            eventoInvitacione.Lugar = (String.IsNullOrEmpty(eventoInvitacione.Lugar) ? eventoInvitacione.IdEventoNavigation.Lugar : eventoInvitacione.Lugar);
            eventoInvitacione.Direccion = (String.IsNullOrEmpty(eventoInvitacione.Direccion) ? eventoInvitacione.IdEventoNavigation.Direccion : eventoInvitacione.Direccion);

            return View(eventoInvitacione);
        }

        public async Task<IActionResult> Print(int? id, int Invitado)
        {

            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "Evento1");
           
            //ViewData["Eventos"] = _context.Eventos.ToList();
            ViewData["IdInstitucion"] = new SelectList(_context.Instituciones, "IdInstitucion", "Nombre");
            if (id == null || _context.EventoInvitaciones == null)
            {
                return NotFound();
            }

            var eventoInvitacione = await _context.EventoInvitaciones.FindAsync(id);
            if (eventoInvitacione == null)
            {
                return NotFound();
            }
            ViewData["Invitado"] = _context.EventoInvitados.Include(q=>q.IdPersonaNavigation).Where(q => q.IdPersona == Invitado).FirstOrDefault();
            ViewData["Front"] = this.Configuration.GetValue<string>("Front:Url");
            return PartialView(eventoInvitacione);
        }

        public async Task<IActionResult> Enviar(int? id)
        {

            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "Evento1");

            //ViewData["Eventos"] = _context.Eventos.ToList();
            ViewData["IdInstitucion"] = new SelectList(_context.Instituciones, "IdInstitucion", "Nombre");
            if (id == null || _context.EventoInvitaciones == null)
            {
                return NotFound();
            }

            var eventoInvitacione = await _context.EventoInvitaciones.FindAsync(id);
            if (eventoInvitacione == null)
            {
                return NotFound();
            }
            //ViewData["Invitado"] = eventoInvitacione.EventoInvitados.Where(q => q.IdPersona == Invitado).FirstOrDefault();
            return View(eventoInvitacione);
        }
        [HttpPost]
        public async Task<IActionResult> Enviar(IFormCollection fc)
        {


            var id = Convert.ToInt32(fc["IdInvitacion"]);
            try
            {
                var eventoInvitacione = await _context.EventoInvitaciones.FindAsync(id);
                if (eventoInvitacione == null)
                {
                    return NotFound();
                }
                eventoInvitacione.Enviado = true;
                eventoInvitacione.FechaEnvio = DateTime.Now;
                _context.Update(eventoInvitacione);
                await _context.SaveChangesAsync();
                var url = this.Configuration.GetValue<string>("Front:Url");
                foreach (EventoInvitado inv in eventoInvitacione.EventoInvitados)
                {
                    try
                    {

                        /**qr*/
                        //GeneratedBarcode barcode = QRCodeWriter.CreateQrCode(url + "/confirmacion?id=" + inv.Token, 200);
                        QRCodeData qrCodeData;

                        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                        {
                            qrCodeData = qrGenerator.CreateQrCode(url + "/confirmacion?id=" + inv.Token, QRCodeGenerator.ECCLevel.Q);
                            
                        }

                        // Image Format
                        var imgType = Base64QRCode.ImageType.Png;

                        var qrCode = new Base64QRCode(qrCodeData);
                        
                        //Base64 Format
                        string qrCodeImageAsBase64 = qrCode.GetGraphic(20, SixLabors.ImageSharp.Color.Black, SixLabors.ImageSharp.Color.White, true, imgType);
                        string qr = "data:image/png;base64," + qrCodeImageAsBase64;
                        /**qr*/

                        if (new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(inv.IdPersonaNavigation.Email))
                        {
                            string conf = "";
                            if (eventoInvitacione.TipoConf == "1")
                            {
                                conf = eventoInvitacione.TextConf;
                            }
                            else if(eventoInvitacione.TipoConf == "2")
                            {
                                conf = "<a href='" + @url + "/confirmacion?id=" + inv.Token + "'>enlace confirmación</a>";
                            }
                            else
                            {
                                conf = eventoInvitacione.TextConf + "<img style='width:150px; height:150px' src = '" + @qr + "' class='image-responsive' />";
                            }
                                Mail(new EmailModel
                            {
                                To = inv.IdPersonaNavigation.Email,
                                Subject = eventoInvitacione.Titulo,
                                Body = "<html><head> " +
                                "<style> .box { display:flex; justify-content:center; } </style> " +                               
                                "</head><body>  " +
                                "<div class='box'><table width='80%' > " +
                                "<tr><td style='text-align:center' colspan='2'><img src = '" + @url + "/archivos/logo_invitaciones.png' class='image-responsive' /></td></tr> " +
                                "<tr><td style='text-align:center' colspan='2'><h3>" + (eventoInvitacione.Remitente) + "</h3> </td></tr>  " +
                                "<tr><td style='text-align:center' colspan='2'><p>" + eventoInvitacione.Saludo + "</p> </td></tr> " +
                                "<tr><td style='text-align:center' colspan='2'><h3><p>"+ (inv.IdPersonaNavigation.Trato + ' ' + inv.IdPersonaNavigation.Nombre + ' ' + inv.TextoAdicional )+"</p></h3> </td></tr> " +
                                "<tr><td style = 'text-align:center' colspan ='2'><p> " + eventoInvitacione.Motivo + "</p><p>" + eventoInvitacione.Fecha + "</p> </td></tr> " +
                                "<tr><td style = 'text-align:left' ><p>" + eventoInvitacione.Lugar + "</p>" +
                                "<p> " + eventoInvitacione.Direccion + "</p>" +
                                 "<p> " + eventoInvitacione.Tenida + "</p> </td>" +
                                
                                "<td style='text-align:right'>"+conf+"  </td>" +
                                
                               
                                "</tr></table></div>" +
                                
                                "</body>" +
                                "</html>"

                            });
                        }
                         
                    }
                    catch(Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                    }
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }          
            //ViewData["Invitado"] = eventoInvitacione.EventoInvitados.Where(q => q.IdPersona == Invitado).FirstOrDefault();
            return RedirectToAction(nameof(Index));
        }
        private static byte[] bitmaptoArray(Bitmap bitmapimage)
        {
            using (MemoryStream mstream = new MemoryStream())
            {

                bitmapimage.Save(mstream, ImageFormat.Png);
                return mstream.ToArray();
            }

        }
        // POST: EventoInvitaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, /*[Bind("IdInvitacion,IdEvento")]*/ EventoInvitacione eventoInvitacione, IFormFile Archivo, IFormCollection fc)
        {
            if (id != eventoInvitacione.IdInvitacion)
            {
                return NotFound();
            }

            ViewData["IdEvento"] = new SelectList(_context.Eventos, "IdEvento", "Evento1");
            //ViewData["Eventos"] = _context.Eventos.ToList();
            ViewData["IdInstitucion"] = new SelectList(_context.Instituciones, "IdInstitucion", "Nombre");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.RemoveRange(_context.EventoInvitados.Where(q=>q.IdEventoInvitacion==id));
                    if (!String.IsNullOrEmpty(fc["invitado"]))
                    {
                        var inv = fc["invitado"].ToString().Split(",");
                        var aco = fc["adicional"].ToString().Split(",");
                        for (int i = 0; i< inv.Length;i++)
                        {
                            var acomp = aco.Length > i ? aco[i] : "";
                            _context.EventoInvitados.Add(new EventoInvitado
                            {
                                IdPersona = Convert.ToInt32(inv[i]),
                                IdEventoInvitacion = id,
                                TextoAdicional = acomp,
                                Token = id.ToString() + DateTime.Now.ToBinary().ToString(),
                                Confirmado = false
                            }); 
                        }
                    }
                    if (Archivo != null)
                    {
                        var FileDic = "Files";

                        string FilePath = Path.Combine(hostingEnv.WebRootPath, FileDic);

                        if (!Directory.Exists(FilePath))

                            Directory.CreateDirectory(FilePath);

                        var fileName = Archivo.FileName;

                        var filePath = Path.Combine(FilePath, fileName);

                        using (FileStream fs = System.IO.File.Create(filePath))

                        {

                            Archivo.CopyTo(fs);

                        }
                        eventoInvitacione.Archivo = fileName;
                    }
                    //await _context.SaveChangesAsync();

                    _context.Update(eventoInvitacione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoInvitacioneExists(eventoInvitacione.IdInvitacion))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);

                }
                //var m = await _context.EventoInvitaciones.Where(q=>q.IdInvitacion==id).Include(q=>q.EventoInvitados).FirstOrDefaultAsync();
                return RedirectToAction(nameof(Edit),id);
                //return View(m);
            }
            return View(eventoInvitacione);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InsertInvitado(EventoInvitado invitado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(invitado);
                    //_logger.LogInformation(JsonSerializer.Serialize(menu));
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, ex.Message);
                return View(invitado);
            }

            return View(invitado);
        }

        public JsonResult Personas(int id)
        {
            var list = _context.Personas.Where(q => q.IdInstitucion == id).Select(x => new { id = x.IdInvitado, nombre = x.Nombre, cargo=x.Cargo, institucion=x.IdInstitucionNavigation.Nombre, grupo=x.IdGrupoNavigation.Nombre});
            return Json(list);
        }

        public string Mail(EmailModel model)
        {
            string msj = "";
            string host = this.Configuration.GetValue<string>("Smtp:Server");
            int port = this.Configuration.GetValue<int>("Smtp:Port");
            string fromAddress = this.Configuration.GetValue<string>("Smtp:FromAddress");
            string userName = this.Configuration.GetValue<string>("Smtp:UserName");
            string password = this.Configuration.GetValue<string>("Smtp:Password");

            using (MailMessage mm = new MailMessage(fromAddress, model.To))
            {
                mm.Subject = model.Subject;
                mm.Body = model.Body;

                mm.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = host;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    NetworkCredential NetworkCred = new NetworkCredential(userName, password);
                   
                    smtp.Credentials = NetworkCred;
                    smtp.Port = port;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    smtp.Send(mm);
                }
            }
            return msj;
        }
        private static bool IsValid(string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }

        // GET: EventoInvitaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EventoInvitaciones == null)
            {
                return NotFound();
            }

            var eventoInvitacione = await _context.EventoInvitaciones
                .FirstOrDefaultAsync(m => m.IdInvitacion == id);
            if (eventoInvitacione == null)
            {
                return NotFound();
            }

            return View(eventoInvitacione);
        }

        // POST: EventoInvitaciones/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EventoInvitaciones == null)
            {
                return Problem("Entity set 'InvitacionesContext.EventoInvitaciones'  is null.");
            }
            var eventoInvitacione = await _context.EventoInvitaciones.FindAsync(id);
            if (eventoInvitacione != null)
            {
                _context.EventoInvitaciones.Remove(eventoInvitacione);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool EventoInvitacioneExists(int id)
        {
          return _context.EventoInvitaciones.Any(e => e.IdInvitacion == id);
        }
    }
}
