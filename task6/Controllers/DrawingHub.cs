using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using task6.Data;
using task6.Models;

namespace task6.Controllers;

using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class DrawingHub : Hub
{
    private readonly AppDbContext _dbContext;

    public DrawingHub(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Table>> GetTables()
    {
        return await _dbContext.Tables.ToListAsync();
    }

    
    public async Task LoadLastDrawing(string clientId, int tableId)
    {
        var lastDrawing = _dbContext.Drawings.Where(d => d.TableId == tableId);
        if (lastDrawing != null)
        {
            List<Line> drawings = new List<Line>();
            foreach (var draw in lastDrawing)
            {
                var drawingData = JsonConvert.DeserializeObject<List<Line>>(draw.DrawingData);
                foreach (var dLine in drawingData)
                {
                    drawings.Add( dLine);
                }
            }
  
            await Clients.Client(clientId).SendAsync("ReceiveDrawingUpdate", drawings);
        }
    }
    public async Task JoinGroup(int tableId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"Table-{tableId}");
    }

    public async Task LeaveGroup(int tableId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Table-{tableId}");
    }

    
    public async Task SendDrawingUpdate(int tableId, List<Line> drawingData)
    {
        var drawing = new Drawing
        {
            DrawingData = JsonConvert.SerializeObject(drawingData),
            CreatedAt = DateTime.UtcNow,
            TableId = tableId
            
        };
        _dbContext.Drawings.Add(drawing);
        await _dbContext.SaveChangesAsync();

        await Clients.Group($"Table-{tableId}").SendAsync("ReceiveDrawingUpdate", drawingData);
    }
    
    public async Task ClearDrawing(int tableId)
    {
        var drawings = _dbContext.Drawings.Where(d => d.TableId == tableId);
        _dbContext.Drawings.RemoveRange(drawings);
        await _dbContext.SaveChangesAsync();

        await Clients.Group($"Table-{tableId}").SendAsync("ReceiveClearDrawing");
    }

}

public class Line
{
    public int size { get; set; }
    public string color { get; set; }
    public bool isErased { get; set; }
    public Point from { get; set; }
    public Point to { get; set; }
}

public class Point
{
    public int x { get; set; }
    public int y { get; set; }
}