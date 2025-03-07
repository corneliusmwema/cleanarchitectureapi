using Application.Interfaces.RepositoryInterfaces;
using Domain.Entities;
using Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class VideoRepository : IVideoRepository
{
    private readonly VideoDBContext _context;


    public VideoRepository(VideoDBContext context)
    {
        _context = context;
    }

    public async Task<Video> CreateVideo(Video video)
    {
        var newVideo = await _context.Videos.AddAsync(video);

        await _context.SaveChangesAsync();

        return newVideo.Entity;
    }

    public async Task<bool> DeleteVideo(Guid id)
    {
        var video = await GetVideoById(id);

        return video != null;
    }

    public async Task<Video?> GetVideoById(Guid id)
    {
        var video = await _context.Videos.Include(g => g.User).FirstOrDefaultAsync(v => v.Id == id);

        return video;
    }


    public async Task<IEnumerable<Video>> GetVideos()
    {
        var videos = await _context.Videos.Include(g => g.User).ToListAsync();

        return videos;
    }

    public async Task<IEnumerable<Video>> GetVideosByUserId(Guid userId)
    {
        var videos = await _context.Videos.Where(v => v.UserId == userId).ToListAsync();

        return videos;
    }

    public async Task<Video> UpdateVideo(Video video)
    {
        var updatedVideo = _context.Videos.Update(video);

        await _context.SaveChangesAsync();

        return updatedVideo.Entity;
    }
}