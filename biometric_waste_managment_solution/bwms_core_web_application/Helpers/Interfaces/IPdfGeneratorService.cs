namespace bank_data_web_application.Helpers.Interfaces
{
	public interface IPdfGeneratorService
	{
		byte[] GeneratePdfFromHtml(string htmlContent);
		Task<string> RenderViewToStringAsync<TModel>(string templateName, TModel model);
	}
}
