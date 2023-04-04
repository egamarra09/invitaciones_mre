
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Invitaciones.Shared.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Invitaciones.Admin.Models
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
	{
		public CustomAuthorizeAttribute()
		{

		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var user = context.HttpContext.User;
			var controllerName = "";
			var action = "";
			var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
			if (controllerActionDescriptor != null)
			{
				controllerName = controllerActionDescriptor.ControllerName.ToLower();
				action = controllerActionDescriptor.ActionName.ToLower();
			}
			//if (!user.Identity.IsAuthenticated)
			//{
			//	// it isn't needed to set unauthorized result 
			//	// as the base class already requires the user to be authenticated
			//	// this also makes redirect to a login page work properly
			//	// context.Result = new UnauthorizedResult();
			//	return;
			//}

			var usuario = user.Identity.Name.Split("\\")[1].Replace("dablo","edgar");
			var dbContext = context.HttpContext.RequestServices.GetService(typeof(InvitacionesContext)) as InvitacionesContext;
			var u = dbContext.Usuarios.Where(q => q.Login == usuario).FirstOrDefault();
			if (u == null)
			{
				context.HttpContext.Response.Redirect("/home2/");
			}
			else
			{
				context.HttpContext.Session.SetInt32("UsTipo", u.IdUsuarioTipo);
				context.HttpContext.Session.SetString("UsNom", u.Nombres + " " + u.Apellidos);
				if (u.IdUsuarioTipoNavigation.IdUsuarioTipo != -1)
				{
					if (controllerName != "home")
					{
						Menu m = dbContext.Menus.Where(q => q.Link.ToLower() == controllerName).FirstOrDefault();
						if (m == null)
						{
							context.HttpContext.Response.Redirect("/home2/");
						}
						else
						{
							MenuUsuariosTipo pe = dbContext.MenuUsuariosTipos.Where(q => q.IdMenu == m.IdMenu && q.IdUsuarioTipo == u.IdUsuarioTipo).FirstOrDefault();
							if (pe == null)
							{
								context.HttpContext.Response.Redirect("/home/noauth");
							}
						}
					}
				}
			}
		}
	}
}
