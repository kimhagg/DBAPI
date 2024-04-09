// Credits https://github.com/jgravelle/GroqApiLibrary

using Newtonsoft.Json.Linq;

namespace GroqApiService
{
    public interface IGroqApiClient
    {
        Task<JObject> CreateChatCompletionAsync(JObject request);
    }
}