using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WhitePie.Models;
using WhitePie.Services;
using WhitePie.ViewModels;

namespace WhitePie.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        private readonly MomentsService _momentsService;
        public HomeController(MomentsService momentsService)
        {
            _momentsService = momentsService;
        }
        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel()
            {
                momentsViewModel = new List<MomentsViewModel>()
            };

            try
            {
                var moments = await _momentsService.GetAsync();

                foreach (var moment in moments)
                {
                    var momentViewModel = new MomentsViewModel();
                    momentViewModel.Id = moment.FileId;
                    momentViewModel.Extension = "jpg";
                    viewModel.momentsViewModel.Add(momentViewModel);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Image(string Id, string Extension)
        {
            var fileBytes = await _momentsService.GetMomentBytesAsync(Id);
            
            return File(fileBytes, $"image/{Extension}");
        }
    }
}