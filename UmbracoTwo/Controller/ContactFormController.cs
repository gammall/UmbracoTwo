using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Website.Controllers;
using UmbracoTwo.Models;

namespace UmbracoTwo.Controller;

[PluginController("Controller")]
public class ContactFormController : SurfaceController
{
    public ContactFormController(
        IUmbracoContextAccessor umbracoContextAccessor,
        IUmbracoDatabaseFactory databaseFactory,
        ServiceContext services,
        AppCaches appCaches,
        IProfilingLogger profilingLogger,
        IPublishedUrlProvider publishedUrlProvider)
        : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
    }

    [HttpPost]
    public IActionResult HandleSubmit(ContactFormModel form)
    {
        if (!ModelState.IsValid)
        {

            return CurrentUmbracoPage();
        }
        ViewData["name"] = form.Name;
        ViewData["email"] = form.Email;
        ViewData["phone"] = form.Phone;

        var userForm = new ContactFormModel
        {
            Name = form.Name,
            Email = form.Email,
            Phone = form.Phone
        };
        View(userForm);

        return RedirectToCurrentUmbracoPage();
    }
}