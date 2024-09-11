using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebFront_Adam_Garcia_2024_09_10.ModelsView;

namespace WebFront_Adam_Garcia_2024_09_10.Controllers
{
    [Route("/Asientos_Contables")]
    public class AsientoContableController1 : Controller
    {
        Uri baseAddres = new Uri("http://localhost:5271");
        private readonly HttpClient _client;

        public AsientoContableController1()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddres;
        }
        [HttpGet]
        public async Task<IActionResult> ListaAsientos()
        {
            List<AsientoContable> vistacliente = new List<AsientoContable>();
            HttpResponseMessage response = await _client.GetAsync("/api/AsientoContables/GetAsientoContables");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                vistacliente = JsonConvert.DeserializeObject<List<AsientoContable>>(data);
            }

            return View(vistacliente);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
