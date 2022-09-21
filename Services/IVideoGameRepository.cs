using DSF22CSCI3110Lab05.Models.Entities;

namespace DSF22CSCI3110Lab05.Services;

public interface IVideoGameRepository
{
    Task<ICollection<VideoGame>> ReadAllAsync();
    Task<VideoGame?> CreateAsync(VideoGame videoGame);
    Task<VideoGame?> ReadAsync(int id);
    Task UpdateAsync(int oldId, VideoGame videoGame);
    Task DeleteAsync(int id);
}
