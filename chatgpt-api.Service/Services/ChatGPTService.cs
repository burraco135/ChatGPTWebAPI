using OpenAI_API;
using OpenAI_API.Completions;

namespace chatgpt_api.Service.Services
{
    public class ChatGPTService : IChatGPTService
    {
        private readonly string _openAiApiKey;

        public ChatGPTService()
        {
            _openAiApiKey = "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";
        }

        public string getTextResponse(string input)
        {
            var openai = new OpenAIAPI(_openAiApiKey);

            CompletionRequest request = useTextModel(500);
            request.Prompt = input;

            var completions = openai.Completions.CreateCompletionAsync(request);

            var response = "";

            foreach (var completion in completions.Result.Completions)
            {
                response += completion.Text;
            }

            return response;
        }

        public string getCodeResponse(string input)
        {
            var openai = new OpenAIAPI(_openAiApiKey);

            CompletionRequest request = useTextModel(300);
            request.Prompt = input;

            var completions = openai.Completions.CreateCompletionAsync(request);

            var response = "";

            foreach (var completion in completions.Result.Completions)
            {
                response += completion.Text;
            }

            return response;
        }

        public CompletionRequest useCodeModel(int tokens)
        {
            CompletionRequest request = new CompletionRequest();
            request.Model = OpenAI_API.Models.Model.DavinciCode;
            request.MaxTokens = tokens;
            
            return request;
        }

        public CompletionRequest useTextModel(int tokens)
        {
            CompletionRequest request = new CompletionRequest();
            request.Model = OpenAI_API.Models.Model.DavinciText;
            request.MaxTokens = tokens;

            return request;
        }
    }
}
