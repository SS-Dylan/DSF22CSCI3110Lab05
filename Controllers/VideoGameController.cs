using DSF22CSCI3110Lab05.Models.Entities;
using DSF22CSCI3110Lab05.Services;
using Microsoft.AspNetCore.Mvc;

namespace DSF22CSCI3110Lab05.Controllers;

public class VideoGameController : Controller
{
    private readonly IVideoGameRepository _gameRepo;

    public VideoGameController(IVideoGameRepository gameRepo)
    {
        _gameRepo = gameRepo;
    }

    public async Task<IActionResult> Index()
    {
        var games = await _gameRepo.ReadAllAsync();
        return View(games);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(VideoGame videoGame)
    {
        if(ModelState.IsValid)
        {
            await _gameRepo.CreateAsync(videoGame);
            return RedirectToAction("Index");
        }
        return View(videoGame);
    }

    public async Task<IActionResult> Details(int id)
    {
        var game = await _gameRepo.ReadAsync(id);
        if (game == null)
        {
            return RedirectToAction("Index");
        }
        return View(game);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var game = await _gameRepo.ReadAsync(id);
        if (game == null)
        {
            return RedirectToAction("Index");
        }
        return View(game);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(VideoGame videoGame)
    {
        if (ModelState.IsValid)
        {
            await _gameRepo.UpdateAsync(videoGame.Id, videoGame);
            return RedirectToAction("Index");
        }
        return View(videoGame);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var videoGame = await _gameRepo.ReadAsync(id);
        if (videoGame == null)
        {
            return RedirectToAction("Index");
        }
        return View(videoGame);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _gameRepo.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}

