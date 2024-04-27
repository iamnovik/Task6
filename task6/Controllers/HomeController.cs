using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task6.Data;
using task6.Models;

namespace task6.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    private readonly AppDbContext _dbContext;

    public HomeController(ILogger<HomeController> logger,  AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    
    public async Task<IActionResult> DrawTable()
    {
        var tables = await _dbContext.Tables.ToListAsync();
        return View(tables);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTable(string tableName)
    {
        var table = new Table { Name = tableName };
        _dbContext.Tables.Add(table);
        await _dbContext.SaveChangesAsync();

        return RedirectToAction("DrawTable");
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}