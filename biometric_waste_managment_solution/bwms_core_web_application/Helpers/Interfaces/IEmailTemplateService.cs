namespace bank_data_web_application.Helpers.Interfaces
{
	public interface IEmailTemplateService
	{
		Task<string> GenerateEmailBodyAsync<TModel>(string templateName, TModel model);
	}
}
