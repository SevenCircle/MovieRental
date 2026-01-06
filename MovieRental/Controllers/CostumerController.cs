using Microsoft.AspNetCore.Mvc;
using MovieRental.Costumer;

namespace MovieRental.Controllers;

[ApiController]
[Route("[controller]")]
public class CostumerController : ControllerBase
{

    private readonly ICostumerFeatures _features;

    public CostumerController(ICostumerFeatures features)
    {
        _features = features;
    }


    [HttpPost]
    public IActionResult Post([FromBody] Costumer.Costumer costumer)
    {
	        return Ok(_features.Save(costumer));
    }

    [HttpGet]
    public IActionResult Get([FromQuery] int pageNumber=1, int pageSize=50)
    {
        return Ok(_features.GetAll(pageNumber,pageSize));
    }

}
