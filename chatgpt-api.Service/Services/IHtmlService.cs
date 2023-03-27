namespace chatgpt_api.Service.Services
{
    public interface IHtmlService
    {
        public bool CheckURL(string url);
        public Task<string> GetTextFromUrl(string url);
    }
}
