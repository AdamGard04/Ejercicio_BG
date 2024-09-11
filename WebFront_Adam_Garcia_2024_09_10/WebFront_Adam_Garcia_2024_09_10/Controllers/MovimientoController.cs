using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebFront_Adam_Garcia_2024_09_10.ModelsView;

namespace WebFront_Adam_Garcia_2024_09_10.Controllers
{
    [Route("/Movimiento")]
    public class MovimientoController : Controller
    {
        Uri baseAddres = new Uri("http://localhost:5271");
        private readonly HttpClient _client;

        public MovimientoController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddres;
        }
        [HttpGet]
        public async Task<IActionResult> ListaMovimiento()
        {
            List<Movimiento> vistaMovimiento = new List<Movimiento>();
            HttpResponseMessage response = await _client.GetAsync("/api/Movimientoes/GetMovimientos");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                vistaMovimiento = JsonConvert.DeserializeObject<List<Movimiento>>(data);
            }

            return View(vistaMovimiento);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
