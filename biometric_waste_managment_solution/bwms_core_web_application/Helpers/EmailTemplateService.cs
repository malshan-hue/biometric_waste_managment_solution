using bank_data_web_application.Helpers.Interfaces;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace bank_data_web_application.Helpers
{
	public class EmailTemplateService : IEmailTemplateService
	{
		private readonly ICompositeViewEngine _viewEngine;
		private readonly ITempDataDictionaryFactory _tempDataFactory;
		private readonly IHttpContextAccessor _contextAccessor;

		public EmailTemplateService
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

		public async Task<string> GenerateEmailBodyAsync<TModel>(string templateName, TModel model)
		{
			var viewPath = $"/Views/EmailTemplates/{templateName}.cshtml";
			var actionContext = new ActionContext(_contextAccessor.HttpContext, _contextAccessor.HttpContext.GetRouteData(), new ActionDescriptor());

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

				var viewContext = new ViewContext(actionContext, viewResult.View, viewData, _tempDataFactory.GetTempData(_contextAccessor.HttpContext), sw, new HtmlHelperOptions());

				await viewResult.View.RenderAsync(viewContext);
				return sw.ToString();
			}
		}
	}
}
