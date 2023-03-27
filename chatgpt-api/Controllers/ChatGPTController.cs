using chatgpt_api.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace chatgpt_api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatGPTController : ControllerBase
    {
        readonly IChatGPTService _chatGPTService;
        readonly IHtmlService _htmlService;
        public ChatGPTController()
        {
            _chatGPTService = new ChatGPTService();
            _htmlService = new HtmlService();
        }

        [HttpGet]
        public async Task<IActionResult> getTextResponse(string input)
        {
            // no context response
            var answer = _chatGPTService.getTextResponse(input);
            return Ok(answer);
        }

        [HttpGet]
        public async Task<IActionResult> getResponseFromUrl(string url, string input)
        {
            if (_htmlService.CheckURL(url))
            {
                // fetch html from url
                var text = _htmlService.GetTextFromUrl(url);
                var response = "";

                if (text.Result.Length > 0)
                {
                    if (text.Result.Length > 3000)
                    {
                        // context response, less text with input
                        var information = text.Result.Substring(0, 3500) + " " + input;
                        response = _chatGPTService.getTextResponse(information);
                    } else
                    {
                        // context response, all text with input
                        var information = text + " " + input;
                        response = _chatGPTService.getTextResponse(information);
                    }
                    
                } else {
                    // no context response, no text, only input
                    response = _chatGPTService.getTextResponse(input);
                }
                
                return Ok(response);

            } else {

                // no context response
                var answer = _chatGPTService.getTextResponse(input);
                return Ok(answer);
            }

        }
    }
}
