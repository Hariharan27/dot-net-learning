using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TrainingMentorship.Application.DTOs;
using TrainingMentorship.Application.DTOs.Auth;
using TrainingMentorship.MVC.Models;

namespace TrainingMentorship.MVC.Services;

public class AuthApiService
{
    private readonly HttpClient _client;

    public AuthApiService(HttpClient client)
    {
        _client = client;
    }

    public async Task<LoginResponseDto?> LoginAsync(string email, string password)
    {
        var payload = new LoginRequestDto
        {
            Email = email,
            Password = password
        };

        var json = JsonConvert.SerializeObject(payload);

        var response = await _client.PostAsync(
            "api/Auth/login",
            new StringContent(json, Encoding.UTF8, "application/json")
        );

        if (!response.IsSuccessStatusCode)
            return null;

        var jsonString = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<LoginResponseDto>(jsonString);
    }


    // REGISTER ---------------------
    public async Task<bool> RegisterAsync(RegisterViewModel model)
    {
        var payload = new
        {
            fullName = model.FullName,
            email = model.Email,
            password = model.Password,
            role = model.Role
        };

        var json = JsonConvert.SerializeObject(payload);

        var response = await _client.PostAsync(
            "api/Auth/register",
            new StringContent(json, Encoding.UTF8, "application/json")
        );

        return response.IsSuccessStatusCode;
    }

}


