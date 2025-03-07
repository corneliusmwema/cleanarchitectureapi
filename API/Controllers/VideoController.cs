using API.Common;
using Application.Dtos;
using Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/v1")]
[ApiController]
public class VideoController : BaseApiController
{
    private readonly ILogger<VideoController> _logger;
    private readonly IVideoService _videoService;


    public VideoController(IVideoService videoService, ILogger<VideoController> logger)
    {
        _videoService = videoService;
        _logger = logger;
    }


    [HttpPost("videos")]
    [Authorize]
    public async Task<IActionResult> CreateVideoAsync(VideoDto video)
    {
        var result = await _videoService.CreateVideo(video, CurrentUser.UserId);

        return StatusCode(201, result);
    }


    [HttpGet("videos/{id}")]
    public async Task<IActionResult> GetVideoByIdAsync(Guid id)
    {
        var result = await _videoService.GetVideoById(id);

        return StatusCode(200, result);
    }


    [HttpGet("videos/user")]
    [Authorize]
    public async Task<IActionResult> GetVideosByUserIdAsync()
    {
        var result = await _videoService.GetVideosByUserId(CurrentUser.UserId);

        return StatusCode(200, result);
    }


    [HttpGet("videos")]
    public async Task<IActionResult> GetVideosAsync()
    {
        var result = await _videoService.GetVideos();

        return StatusCode(200, result);
    }


    [HttpPut("videos/{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateVideoAsync(VideoDto video, Guid id)
    {
        var result = await _videoService.UpdateVideo(video, id);

        return StatusCode(200, result);
    }
}