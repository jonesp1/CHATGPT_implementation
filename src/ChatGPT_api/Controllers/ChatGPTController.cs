using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;

namespace ChatGPT_api.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatGPTController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public ChatGPTController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    [Route("/chat")]
    public async Task<IActionResult> Chatgpt(string input)
    {
        string output = "";
        string mySecretValue = _configuration["ChatGPT_Api_key"];
        var openai = new OpenAIAPI(mySecretValue);
        CompletionRequest completionRequest = new CompletionRequest();
        completionRequest.Prompt = input;
        completionRequest.Model = OpenAI_API.Models.Model.DavinciText;

        var completions = openai.Completions.CreateCompletionAsync(completionRequest);

        foreach (var completion in completions.Result.Completions)
        {
            output += completion.Text;
        }
        return Ok(output);
    }

}