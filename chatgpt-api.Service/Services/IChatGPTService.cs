using OpenAI_API.Completions;

namespace chatgpt_api.Service.Services
{
    public interface IChatGPTService
    {
        public string getTextResponse(string input);
        public string getCodeResponse(string input);
        public CompletionRequest useTextModel(int tokens);
        public CompletionRequest useCodeModel(int tokens);

    }
}
