using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Core;
using OnlineShop.Core.Contarcts;
using OnlineShop.Core.Models;
using OnlineShop.Core.ViewModels;

namespace OnlineShop.WebUI.Controllers
{
    public class CheckOutController : Controller
    {
        IRepository<CheckOutInformation> context;
        IBasketService basketService;
        public CheckOutController(IRepository<CheckOutInformation> checkOutContext, IBasketService BasketService)
        {
            context = checkOutContext;
            this.basketService = BasketService;
        }

        // GET: CheckOut
        public ActionResult Index(string Id)
        {
            CheckOutInformation model = context.Find(Id);
            CheckOutInformationViewModel viewModel = new CheckOutInformationViewModel();
         
            if (model == null)
            {
                return HttpNotFound();
            }
            else
            {
                viewModel.checkOutInformation = model;
            }
            viewModel.basketItemViewModels = basketService.GetBasketItems(this.HttpContext);
            return View(viewModel);

        }

        // GET: CheckOut/Create
        public ActionResult Create()
        {
            CheckOutInformationViewModel viewModel= new CheckOutInformationViewModel();
          viewModel.checkOutInformation = new CheckOutInformation();
            viewModel.basketItemViewModels =basketService.GetBasketItems(this.HttpContext);

            return View(viewModel);
        }

        // POST: CheckOut/Create
        [HttpPost]
        public ActionResult Create(CheckOutInformation checkOutInformation)
        {
            if (!ModelState.IsValid)
            {
                return View(checkOutInformation);
            }
            else
            {
                context.Insert(checkOutInformation);
                context.Commit();
 
                return RedirectToAction("Index",new { ID= checkOutInformation.Id } );

            }
        }

    }
}
