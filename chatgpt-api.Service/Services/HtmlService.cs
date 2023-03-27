using HtmlAgilityPack;

namespace chatgpt_api.Service.Services;

public class HtmlService : IHtmlService
{
    public bool CheckURL(string url)
    {
        Uri validatedUri;

        if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out validatedUri)) //.NET URI validation.
        {
            //If true: validatedUri contains a valid Uri. Check for the scheme in addition.
            return (validatedUri.Scheme == Uri.UriSchemeHttp || validatedUri.Scheme == Uri.UriSchemeHttps);
        }
        return false;
    }
    public async Task<string> GetTextFromUrl(string url)
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage res = await client.GetAsync(url);
        HttpContent content = res.Content;

        string htmlContent = await content.ReadAsStringAsync();

        var doc = new HtmlDocument();
        doc.LoadHtml(htmlContent);

        // fetch data from headers
        var headings = doc.DocumentNode.Descendants("h1")
        .Select(node => node.InnerText)
        .ToList();

        // fetch data from paragraphs
        var paragraphs = doc.DocumentNode.Descendants("p")
        .Select(node => node.InnerText)
        .ToList();

        // put all together
        var text = string.Join(" ", headings) + string.Join(" ", paragraphs);

        return text;
    }
}
