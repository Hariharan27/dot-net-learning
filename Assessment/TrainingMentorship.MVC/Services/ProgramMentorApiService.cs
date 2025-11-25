using System;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TrainingMentorship.MVC.Models;
using TrainingMentorship.Application.DTOs.Auth;
using System.Text.Json;
using TrainingMentorship.Application.DTOs.TrainingProgram;

namespace TrainingMentorship.MVC.Services;

public class ProgramMentorApiService
{
    private readonly HttpClient _client;

    public ProgramMentorApiService(HttpClient client)
    {
        _client = client;
    }

    public async Task<bool> AssignMentorAsync(int programId, int mentorId, string token)
    {
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var payload = new
        {
            ProgramId = programId,
            MentorId = mentorId
        };

        var response = await _client.PostAsync(
            "api/TrainingProgram/assign-mentor",
            new StringContent(
                JsonConvert.SerializeObject(payload),
                Encoding.UTF8,
                "application/json"
            )
        );

        return response.IsSuccessStatusCode;
    }


    public async Task<bool> AssignTraineeAsync(AssignTraineeViewModel model, string token)
    {
        var payload = new
        {
            programId = model.ProgramId,
            traineeId = model.TraineeId,
            mentorIds = model.MentorIds
        };

        var json = JsonConvert.SerializeObject(payload);

        var request = new HttpRequestMessage(HttpMethod.Post, "api/TrainingProgram/assign-trainee");
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.SendAsync(request);

        return response.IsSuccessStatusCode;
    }


    public async Task<List<UserDto>> GetAvailableTrainees(int programId, string token)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Get,
         
            $"/api/TrainingProgram/available-trainees/{programId}"
        );

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<UserDto>>(json)!;
    }


    public async Task<List<ProgramMentorDto>?> GetMentorsForProgramAsync(int programId, string token)
    {
        var req = new HttpRequestMessage(HttpMethod.Get, $"api/TrainingProgram/program-mentors/{programId}");
        req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var res = await _client.SendAsync(req);
        res.EnsureSuccessStatusCode();

        var json = await res.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<List<ProgramMentorDto>>(json)!;
    }



}

