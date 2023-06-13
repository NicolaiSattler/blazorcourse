using System.Net.Http.Json;
using MyBlazorCourse.Shared.Interface;
using MyBlazorCourse.Shared.Model;

namespace MyBlazorCourse.Client.Service;

public class PhotoService : IPhotoService
{
    private readonly HttpClient _client;

    public PhotoService(HttpClient client)
    {
        _client = client;
    }

    public async Task<Photo?> GetByIdAsync(int id)
    {
        return await _client.GetFromJsonAsync<Photo?>($"/api/photo/{id}");
    }

    public async Task<ICollection<Photo>?> GetAllAsync()
    {
        return await _client.GetFromJsonAsync<ICollection<Photo>?>("/api/photo");
    }

    public async Task<Photo?> AddAsync(Photo newPhoto)
    {
        var response = await _client.PostAsJsonAsync<Photo>("/api/photo", newPhoto);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Photo?>();
    }

    public async Task<Photo?> UpdateAsync(Photo changedPhoto)
    {
        var response = await _client.PutAsJsonAsync<Photo>($"/api/photo/{changedPhoto.Id}", changedPhoto);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Photo?>();
    }

    public async Task<Photo?> DeleteAsync(int id)
    {
        var response = await _client.DeleteAsync($"/api/photo/{id}");

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Photo?>();
    }
}