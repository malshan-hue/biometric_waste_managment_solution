using bank_data_web_application.Helpers.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace bank_data_web_application.Helpers
{
	public class PdfGeneratorService : IPdfGeneratorService
	{
		private readonly ICompositeViewEngine _viewEngine;
		private readonly ITempDataDictionaryFactory _tempDataFactory;
		private readonly IHttpContextAccessor _contextAccessor;

		public PdfGeneratorService
		(
			ICompositeViewEngine viewEngine,
			ITempDataDictionaryFactory tempDataFactory,
			IHttpContextAccessor contextAccessor
		)
		{
			_viewEngine = viewEngine;
			_tempDataFactory = tempDataFactory;
			_contextAccessor = contextAccessor;
		}

		// Generate PDF from HTML content
		public byte[] GeneratePdfFromHtml(string htmlContent)
		{
			using (var memoryStream = new MemoryStream())
			{
				var document = new Document(PageSize.A4, 50, 50, 50, 50);
				PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
				document.Open();

				using (var stringReader = new StringReader(htmlContent))
				{
					XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, stringReader);
				}

				document.Close();
				return memoryStream.ToArray();
			}
		}

		// Render Razor view to string
		public async Task<string> RenderViewToStringAsync<TModel>(string templateName, TModel model)
		{
			var viewPath = $"/Views/PdfTemplates/{templateName}.cshtml";
			var httpContext = _contextAccessor.HttpContext;

			var actionContext = new ActionContext( httpContext, httpContext.GetRouteData(), new ActionDescriptor());

			var viewData = new ViewDataDictionary<TModel>(new EmptyModelMetadataProvider(), new ModelStateDictionary())
			{
				Model = model
			};

			using (var sw = new StringWriter())
			{
				var viewResult = _viewEngine.GetView(viewPath, viewPath, false);

				if (!viewResult.Success)
				{
					throw new FileNotFoundException($"The view '{viewPath}' was not found.");
				}

				var viewContext = new ViewContext(
					actionContext,
					viewResult.View,
					viewData,
					_tempDataFactory.GetTempData(httpContext),
					sw,
					new HtmlHelperOptions()
				);

				await viewResult.View.RenderAsync(viewContext);
				return sw.ToString();
			}
		}
	}
}
