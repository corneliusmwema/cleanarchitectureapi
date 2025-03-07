using Application.Dtos;
using Application.Interfaces.RepositoryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Domain.Entities;

namespace Application.Services;

public class VideoService : IVideoService
{
    private readonly IVideoRepository _videoRepository;

    public VideoService(IVideoRepository videoRepository)
    {
        _videoRepository = videoRepository;
    }

    public async Task<Video> CreateVideo(VideoDto video, Guid userId)
    {
        var newVideo = new Video
        {
            Title = video.Title,
            Thumbnail = video.Thumbnail,
            Prompt = video.Prompt,
            VideoUrl = video.VideoUrl,
            UserId = userId
        };

        return await _videoRepository.CreateVideo(newVideo);
    }

    public async Task<bool> DeleteVideo(Guid id)
    {
        return await _videoRepository.DeleteVideo(id);
    }

    public async Task<Video?> GetVideoById(Guid id)
    {
        var video = await _videoRepository.GetVideoById(id);

        if (video == null) throw new Exception("Video not found");

        return video;
    }

    public async Task<IEnumerable<Video>> GetVideos()
    {
        var videos = await _videoRepository.GetVideos();

        return videos;
    }

    public async Task<IEnumerable<Video>> GetVideosByUserId(Guid userId)
    {
        return await _videoRepository.GetVideosByUserId(userId);
    }

    public async Task<Video> UpdateVideo(VideoDto video, Guid id)
    {
        var videoToUpdate = await _videoRepository.GetVideoById(id);

        if (videoToUpdate == null) throw new Exception("Video not found");

        var newVideo = new Video
        {
            Id = id,
            Title = video.Title,
            Thumbnail = video.Thumbnail,
            Prompt = video.Prompt,
            VideoUrl = video.VideoUrl,
            UserId = videoToUpdate.UserId
        };
        return await _videoRepository.UpdateVideo(newVideo);
    }
}