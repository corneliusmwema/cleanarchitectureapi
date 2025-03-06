using Domain.Entities;

namespace Application.Interfaces.RepositoryInterfaces
{
    public interface IVideoRepository
    {
        Task<Video?> GetVideoById(Guid id);

        Task<IEnumerable<Video>> GetVideos();

        Task<IEnumerable<Video>> GetVideosByUserId(Guid userId);

        Task<Video> CreateVideo(Video video);


        Task<Video> UpdateVideo(Video video);


        Task<bool> DeleteVideo(Guid id);


    }
}