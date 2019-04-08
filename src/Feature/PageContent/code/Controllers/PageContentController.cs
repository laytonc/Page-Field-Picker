using System.Web.Mvc;
using SierraBadger.Feature.PageContent.Models;
using Sitecore.Mvc.Presentation;

namespace SierraBadger.Feature.PageContent.Controllers
{
    public class PageContentController : Controller
    {
        public ActionResult PageFieldPicker()
        {
            var renderingContext = RenderingContext.Current;
            var viewModel = new PageFieldPickerRendering();
            viewModel.Initialize(renderingContext.Rendering);

            return View(viewModel);
        }
    }
}