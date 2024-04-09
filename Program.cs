using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using DBAPI.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GroqApiService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Load environment variables from .env file
var envFilePath = Path.Combine(app.Environment.ContentRootPath, ".env");
DotEnv.Load(envFilePath);
string apiKey = Environment.GetEnvironmentVariable("GROQ_API_KEY");
System.Console.WriteLine("EnvFilePath " + envFilePath);
System.Console.WriteLine("API Key " + apiKey);

// Example usage of the Groq API
JObject request = new()
{
    ["model"] = "mixtral-8x7b-32768",
    ["temperature"] = 0.5,
    ["stop"] = "TEMINATE",
    ["messages"] = new JArray
    {
        new JObject
        {
            ["role"] = "system",
            ["content"] = "You love Douglas adams books"
        },
        new JObject
        {
            ["role"] = "user",
            ["content"] = "What is the meaning of life?"
        }
    }
};

GroqApiClient client = new GroqApiClient(apiKey);
JObject result = await client.CreateChatCompletionAsync(request);
string response = result["choices"]?[0]?["message"]["content"]?.ToString() ?? "No response";

System.Console.WriteLine(response);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
