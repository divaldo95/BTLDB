using BTLDB.Classes;
using BTLDB.Data;
using BTLDB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BTLDB.Models.Channeldto;

namespace BTLDB.Controllers;

[ApiController]
[Route("[controller]")]
public class BTLDBController : ControllerBase
{
    private readonly ILogger<BTLDBController> _logger;
    private readonly ExcelReader _excelReader;
    private readonly DataSaver _dataSaver;
    private readonly ApplicationDbContext _context;

    public BTLDBController(ILogger<BTLDBController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _excelReader = new ExcelReader();
        _dataSaver = new DataSaver(dbContext);
        _context = dbContext;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        try
        {
            // Save the uploaded file to a temporary location
            var tempFilePath = Path.GetTempFileName();
            using (var stream = new FileStream(tempFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Read and process the Excel file
            var sipmArrays = _excelReader.ReadExcel(tempFilePath);

            // Save the data to the database
            _dataSaver.SaveData(sipmArrays);

            // Optionally, delete the temporary file
            System.IO.File.Delete(tempFilePath);

            return Ok("File uploaded and data imported successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing file upload.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{sn}")]
    public async Task<IActionResult> GetBySN(string sn)
    {
        var sipmArray = await _context.SiPMArrays
            .Include(a => a.Channels)
            .FirstOrDefaultAsync(a => a.SN == sn);

        if (sipmArray == null)
        {
            return NotFound();
        }

        var sipmArrayDto = new SiPMArrayDto
        {
            TrayNo = sipmArray.TrayNo,
            PositionNo = sipmArray.PositionNo,
            SN = sipmArray.SN,
            TECResistance = sipmArray.TECResistance,
            MeanResistance = sipmArray.MeanResistance,
            StdDevResistance = sipmArray.StdDevResistance,
            RTD = sipmArray.RTD,
            Channels = sipmArray.Channels.Select(c => new ChannelDto
            {
                ChNo = c.ChNo,
                Vop = c.Vop,
                Id1 = c.Id1,
                Id2 = c.Id2,
                Is = c.Is,
                RqN = c.RqN
            }).ToList()
        };

        return Ok(sipmArrayDto);
    }
}



/*
[HttpGet]
public IEnumerable<WeatherForecast> Get()
{
    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    {
        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        TemperatureC = Random.Shared.Next(-20, 55),
        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    })
    .ToArray();
}
*/