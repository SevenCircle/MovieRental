namespace MovieRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalController : ControllerBase
    {

        private readonly IRentalFeatures _features;

        public RentalController(IRentalFeatures features)
        {
            _features = features;
        }


        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Rental.Rental rental)
        {
            if (!await _features.ProcessPayment(rental.PaymentMethod, rental.Price))
            {
                return BadRequest("Payment failed");
            }

            return Ok(await _features.Save(rental));
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string costumerName)
        {
            return Ok(_features.GetRentalsByCustomerName(costumerName));
        }

    }
}
