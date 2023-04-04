using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Invitaciones.Shared.Data;
using NLog;
using NLog.Web;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Invitaciones.Admin.Models;
using Microsoft.AspNetCore.Server.IISIntegration;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
	var builder = WebApplication.CreateBuilder(args);
	builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
	builder.Services.AddDbContext<InvitacionesContext>(options => { options.UseSqlServer("name=DefaultConnection"); options.UseLazyLoadingProxies(); });
	builder.Services.AddDatabaseDeveloperPageExceptionFilter();
	builder.Services.AddControllersWithViews();
	builder.Services.AddRazorPages();
	//builder.Services.AddControllers(config =>
	//{
	//	var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
	//	config.Filters.Add(new AuthorizeFilter(policy));
	//});
	builder.Services.AddScoped<IRepository, Repository>();
	builder.Logging.ClearProviders();
	builder.Host.UseNLog();
	builder.Services.AddDistributedMemoryCache();

	builder.Services.AddSession(options =>
	{
		options.IdleTimeout = TimeSpan.FromSeconds(1000000000);
		options.Cookie.HttpOnly = true;
		options.Cookie.IsEssential = true;
	});
	// builder.UseLazyLoadingProxies();
	//builder.Services.AddScoped<invi>
	var app = builder.Build();

	// Configure the HTTP request pipeline.
	if (app.Environment.IsDevelopment())
	{
		app.UseMigrationsEndPoint();
	}
	else
	{
		app.UseExceptionHandler("/Home/Error");
		// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
		app.UseHsts();
	}

	app.UseHttpsRedirection();
	app.UseStaticFiles();

	app.UseRouting();

	app.UseAuthentication();
	app.UseAuthorization();

	app.UseSession();
	app.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");
	app.MapRazorPages();

	app.Run();
}
catch (Exception exception)
{
	// NLog: catch setup errors
	logger.Error(exception, "Stopped program because of exception");
	throw;
}
finally
{
	// Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
	NLog.LogManager.Shutdown();
}

