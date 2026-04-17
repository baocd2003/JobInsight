using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace JobInsight.Auth;

public class GoogleAuthAppService : ApplicationService, IGoogleAuthAppService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public GoogleAuthAppService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    [AllowAnonymous]
    public async Task<GoogleLoginResultDto> LoginAsync(GoogleLoginInput input)
    {
        var selfUrl = _configuration["App:SelfUrl"]!;
        var http = _httpClientFactory.CreateClient("GoogleAuth");

        var formData = new Dictionary<string, string>
        {
            ["grant_type"] = "google_id_token",
            ["id_token"] = input.IdToken,
            ["client_id"] = "JobInsight_App",
            ["scope"] = "offline_access JobInsight"
        };

        var response = await http.PostAsync(
            $"{selfUrl}/connect/token",
            new FormUrlEncodedContent(formData)
        );

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new UserFriendlyException($"Google login failed: {content}");

        var json = JsonDocument.Parse(content).RootElement;

        return new GoogleLoginResultDto
        {
            AccessToken = json.GetProperty("access_token").GetString()!,
            TokenType = json.GetProperty("token_type").GetString()!,
            ExpiresIn = json.GetProperty("expires_in").GetInt32(),
            RefreshToken = json.TryGetProperty("refresh_token", out var rt) ? rt.GetString() : null
        };
    }
}
