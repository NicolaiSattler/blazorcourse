using System.Diagnostics;
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
        try
        {
            return await _client.GetFromJsonAsync<ICollection<Photo>?>("/api/photo");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return new List<Photo>();
    }

    public async Task<Photo?> AddAsync(Photo newPhoto)
    {
        try
        {
            var response = await _client.PostAsJsonAsync("/api/photo", newPhoto);

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Photo?>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        return default;
    }

    public async Task<Photo?> UpdateAsync(Photo changedPhoto)
    {
        var response = await _client.PutAsJsonAsync($"/api/photo/{changedPhoto.Id}", changedPhoto);

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