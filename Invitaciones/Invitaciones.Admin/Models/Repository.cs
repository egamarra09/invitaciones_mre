using Invitaciones.Shared.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Invitaciones.Admin.Models
{
	public interface IRepository
	{
		IEnumerable<SelectListItem> GetMenuList();
		List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> FormatoEventos();
		List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> zonaHoraria();
		List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> RepetirItems();
	}
	public class Repository : IRepository
	{
		private readonly InvitacionesContext _context;

		public Repository(InvitacionesContext context)
		{
			_context = context;
		}





		/// <summary>
		/// Lista Menu
		/// </summary>
		/// <returns>SelectList<Menu></Menu></returns>
		public IEnumerable<SelectListItem> GetMenuList()
		{
			IEnumerable<SelectListItem> items = _context.Menus.Where(q => q.Activo).OrderBy(o => o.MenuTitulo)
			.Select(c => new SelectListItem
			{
				Value = c.IdMenu.ToString(),
				Text = c.MenuTitulo
			});
			return items;
		}
		public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> FormatoEventos()
		{
			var formato = new List<SelectListItem>();
			formato.Add(new SelectListItem { Text = "Presencial", Value = "P" });
			formato.Add(new SelectListItem { Text = "Virtual", Value = "V" });
			formato.Add(new SelectListItem { Text = "Hibrido", Value = "H" });
			return formato;
		}
		public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> zonaHoraria()
		{
			var formato = new List<SelectListItem>();
			formato.Add(new SelectListItem { Text = "GMT -3", Value = "GMT -3" });
			formato.Add(new SelectListItem { Text = "GMT -4", Value = "GMT -4" });
			formato.Add(new SelectListItem { Text = "GMT -5", Value = "GMT -5" });
			return formato;
		}
		public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> RepetirItems()
		{
			var formato = new List<SelectListItem>();
			formato.Add(new SelectListItem { Text = "No", Value = "N" });
			formato.Add(new SelectListItem { Text = "Semanal", Value = "S" });
			formato.Add(new SelectListItem { Text = "Mensual", Value = "M" });
			formato.Add(new SelectListItem { Text = "Anual", Value = "A" });
			formato.Add(new SelectListItem { Text = "Personalizado", Value = "P" });
			return formato;
		}
	}
}
