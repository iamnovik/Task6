using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using task6.Data;
using task6.Models;

namespace task6.Controllers;

public class DrawingBoardController : Controller
{
    private readonly AppDbContext _dbContext;

    public DrawingBoardController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("{tableId}")]
    public async Task<IActionResult> Index(int tableId)
    {
        
        return View(tableId);
    }

    [HttpPost]
    public async Task<IActionResult> SaveDrawing(int tableId, List<Line> drawingData)
    {
        var drawing = new Drawing
        {
            TableId = tableId,
            DrawingData = JsonConvert.SerializeObject(drawingData),
            CreatedAt = DateTime.UtcNow
        };
        _dbContext.Drawings.Add(drawing);
        await _dbContext.SaveChangesAsync();

        return Ok();
    }
}