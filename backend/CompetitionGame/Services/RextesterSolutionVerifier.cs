using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CompetitionGame.Models;

namespace CompetitionGame.Services
{
    public class RextesterSolutionVerifier : ISolutionVerifier
    {
        private readonly IHttpClientFactory _clientFactory;

        public RextesterSolutionVerifier(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<CodeVerificationResult> Verify(string code, string input, string expectedOutput)
        {
            var httpClient = _clientFactory.CreateClient();
            var compileRequestJson = JsonSerializer.Serialize(new
            {
                LanguageChoice = "1",
                Program = code,
                Input = input,
                CompilerArgs = input
            });

            try
            {
                var response = await httpClient.PostAsync(new Uri("https://rextester.com/rundotnet/api"), new StringContent(compileRequestJson, Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode)
                {
                    return new CodeVerificationResult
                    {
                        IsSuccessful = false,
                        Error = "Errors while making a request to an online compiler"
                    };
                }

                var stream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<RextesterResult>(stream);

                var output = Regex.Match(result.Result, @"^(?'output'(.|\s)*?)\r\n\r\nREXTESTER NOTICE").Groups["output"].Value;

                return new CodeVerificationResult
                {
                    IsSuccessful = output == expectedOutput,
                    Error = result.Errors
                };
            }
            catch
            {
                return new CodeVerificationResult
                {
                    IsSuccessful = false,
                    Error = "Couldn't send the code to an online compiler"
                };
            }
        }

        class RextesterResult
        {
            public string Result { get; set; }
            public string Errors { get; set; }
        }
    }
}
