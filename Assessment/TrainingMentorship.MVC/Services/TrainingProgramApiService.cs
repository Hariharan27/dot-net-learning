using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TrainingMentorship.Application.DTOs.Auth;
using TrainingMentorship.Application.DTOs.Program;
using TrainingMentorship.Application.DTOs.Task;
using TrainingMentorship.Application.DTOs.TrainingProgram;

namespace TrainingMentorship.MVC.Services;

public class TrainingProgramApiService
{
    private readonly HttpClient _client;

    public TrainingProgramApiService(HttpClient client)
    {
        _client = client;
    }

    private void AddJwt(string token)
    {
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
    }

    // ===========================
    // GET ALL PROGRAMS
    // ===========================
    public async Task<List<TrainingProgramListDto>> GetAllAsync(string token)
    {
        AddJwt(token);

        var response = await _client.GetAsync("api/TrainingProgram");

        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<List<TrainingProgramListDto>>(
            stream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        ) ?? new List<TrainingProgramListDto>();

    }



    // ===========================
    // GET ALL PROGRAMS
    // ===========================
    public async Task<List<TrainingProgramListDto>> GetAllProgramsByMentorIdAsync(string token,int mentorId)
    {
        AddJwt(token);

        var response = await _client.GetAsync($"api/TrainingProgram/mentors/{mentorId}/programs");

        response.EnsureSuccessStatusCode();

        var stream = await response.Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<List<TrainingProgramListDto>>(
            stream,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
        ) ?? new List<TrainingProgramListDto>();
    }


    // ===========================
    // CREATE PROGRAM
    // ===========================
    public async Task<int?> CreateAsync(CreateProgramDto dto, string token)
    {
        AddJwt(token);

        var json = JsonSerializer.Serialize(dto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _client.PostAsync("api/TrainingProgram", content);

        if (!response.IsSuccessStatusCode)
            return null;

        var jsonResponse = await response.Content.ReadAsStringAsync();

        var obj = JsonSerializer.Deserialize<CreateProgramResponseDto>(
            jsonResponse,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return obj?.ProgramId;
    }


    // GET DETAILS
    public async Task<TrainingProgramDetailsDto?> GetDetailsAsync(int programId, string token)
    {
        AddJwt(token);

        var response = await _client.GetAsync($"api/TrainingProgram/{programId}");

        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();


        var obj = JsonSerializer.Deserialize<TrainingProgramDetailsDto>(
           json,
           new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return obj;
    }


    public async Task<TrainingProgramDetailsDto?> GetProgramDetailsForMentorAsync(int programId, int mentorId,string token)
    {
        AddJwt(token);

        var response = await _client.GetAsync($"api/TrainingProgram/{programId}/mentor/{mentorId}");

        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();


        var obj = JsonSerializer.Deserialize<TrainingProgramDetailsDto>(
           json,
           new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return obj;
    }

    


    public async Task<List<ProgramTraineeDto>?> GetTraineesForMentorAsync(
    int programId, int mentorId, string token)
    {
        AddJwt(token);

        var response = await _client.GetAsync(
            $"api/TrainingProgram/{programId}/mentor/{mentorId}/trainees");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        var obj = JsonSerializer.Deserialize<List<ProgramTraineeDto>?>(
         json,
         new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return obj;
    }



    public async Task<List<UserDto>?> GetAvailableMentorsAsync(int programId, string token)
    {
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.GetAsync($"api/TrainingProgram/{programId}/available-mentors");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        var obj = JsonSerializer.Deserialize<List<UserDto>?>(
        json,
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return obj;
    }



    public async Task<List<TrainingProgramListDto>?> GetProgramsForTraineeAsync(
    string token, int traineeId)
    {
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.GetAsync(
            $"api/trainingprogram/trainee/{traineeId}");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        var obj = JsonSerializer.Deserialize<List<TrainingProgramListDto>?>(
        json,
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return obj;
    }


    public async Task<TrainingProgramDetailsDto?> GetProgramDetailsForTraineeAsync(
    string token, int programId, int traineeId)
    {
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.GetAsync(
            $"api/trainingprogram/{programId}/trainee/{traineeId}");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        var obj = JsonSerializer.Deserialize<TrainingProgramDetailsDto?>(
        json,
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return obj;
    }



    public async Task<bool> CreateTaskAsync(string token, CreateTaskDto dto)
    {
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.PostAsJsonAsync(
            $"api/trainingprogram/{dto.ProgramId}/tasks", dto);

        return response.IsSuccessStatusCode;
    }


    public async Task<bool> UpdateTaskStatusAsync(string token, UpdateTaskStatusDto dto)
    {
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.PutAsJsonAsync(
            "api/trainingprogram/task/update-status", dto);

        return response.IsSuccessStatusCode;
    }



}


