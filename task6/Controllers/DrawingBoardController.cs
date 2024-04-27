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

}