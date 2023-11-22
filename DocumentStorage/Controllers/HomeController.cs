using DocumentStorage.Helpers;
using DocumentStorage.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TXTextControl.DocumentServer;

namespace DocumentStorage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Template> templates = Storage.GetTemplates();
            return View(templates);
        }

        [HttpPost]
        public IActionResult Save([FromBody] Content data, string id)
        {
            Storage.AddTemplate(new MemoryStream(Convert.FromBase64String(data.Data)), id);
            return Ok();
        }

        public IActionResult Delete(string id)
        {
            // url encoded
            id = id.Replace("%2F", "/");

            Storage.DeleteTemplate(id);
            
            return RedirectToAction("Index");
        }

        public IActionResult Merge(string id)
        {
            // url encoded
            id = id.Replace("%2F", "/");

            var templateData = Storage.GetTemplate(id);

            using (TXTextControl.ServerTextControl tx = new TXTextControl.ServerTextControl())
            {
                tx.Create();
                tx.Load(Convert.FromBase64String(templateData), TXTextControl.BinaryStreamType.InternalUnicodeFormat);
                
                using (MailMerge mailMerge = new MailMerge())
                {
                    mailMerge.TextComponent = tx;
                    mailMerge.MergeJsonData(InvoiceData.GetDummyInvoiceData());
                }
                
                tx.Save(out byte[] data, TXTextControl.BinaryStreamType.AdobePDF);

                // return pdf document
                return File(data, "application/pdf", "document.pdf");
            }
        }

        public IActionResult Editor(string id = null)
        {
            EditorViewModel model = new EditorViewModel()
            {
                InvoiceData = InvoiceData.GetDummyInvoiceData(),
                DocumentId = id
            };

            if (id != null)
            {
                // url encoded
                id = id.Replace("%2F", "/");

                model.TemplateData = Storage.GetTemplate(id);
            }

			return View(model);
		}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
