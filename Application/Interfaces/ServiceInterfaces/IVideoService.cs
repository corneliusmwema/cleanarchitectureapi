


using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface IVideoService
    {
        Task<Video?> GetVideoById(Guid id);

        Task<IEnumerable<Video>> GetVideosByUserId(Guid userId);

        Task<IEnumerable<Video>> GetVideos();

        Task<Video> CreateVideo(VideoDto video, Guid userId);

        Task<Video> UpdateVideo(VideoDto video, Guid id);

        Task<bool> DeleteVideo(Guid id);
    }
}