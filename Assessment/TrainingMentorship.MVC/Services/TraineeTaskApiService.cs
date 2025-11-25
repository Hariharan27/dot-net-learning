using System;
using Newtonsoft.Json;
using TrainingMentorship.Application.DTOs.TrainingProgram;
using System.Net.Http.Headers;

namespace TrainingMentorship.MVC.Services;

public class TraineeTaskApiService
{
    private readonly HttpClient _client;

    public TraineeTaskApiService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<ProgramTraineeTaskDto>> GetTasksAsync(int programId, int traineeId, string token)
    {
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.GetAsync($"api/TrainingProgram/{programId}/trainee/{traineeId}/tasks");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<ProgramTraineeTaskDto>>(json)!;
    }

}

