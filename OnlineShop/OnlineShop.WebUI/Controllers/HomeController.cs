using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Core.Contarcts;

using OnlineShop.Core;
using OnlineShop.Core.ViewModels;
using OnlineShop.Core.Models;
using OnlineShop.WebUI.Controllers;

namespace Myshop.WebUI.Controllers
{

    public class HomeController : Controller
    {
        IRepository<Product> context;
        IRepository<Category> productCategories;
        IRepository<Comment> usercomments;


        public HomeController(IRepository<Product> productContext, IRepository<Category> productCategoryContext, IRepository<Comment> commentsContext)
        {
            context = productContext;
            productCategories = productCategoryContext;
            usercomments = commentsContext;
        }
        //public ActionResult Index()
        //{
        //    List<Category> categories = productCategories.Collection().ToList();
        //    return View(categories);
        //}
        public ActionResult Index(string searchString)
        {
            var cat = from c in productCategories.Collection()
                           select c;




            if (!String.IsNullOrEmpty(searchString))
            {
                cat=cat.Where(s => s.CategoryName.Contains(searchString));
            }



            return View(cat.ToList());
        }
        //public ActionResult Sort(string categoryName)
        //{
        //    List<Product> products = context.Collection().Where(c => c.Category == categoryName).ToList();

        //    return View(products);


        //}
        public ActionResult Sort(string categoryName = null)
        {

            List<Product> products = context.Collection().Where(c => c.Category == categoryName).ToList(); ;
            List<Category> categories = productCategories.Collection().ToList();
            List <Comment> comments = usercomments.Collection().ToList();
            if (categories == null)
            {
                products = context.Collection().ToList();
            }
            else
            {
                products = context.Collection().Where(p => p.Category == categoryName).ToList();
            }
            ProductAdminViewModel viewmodel = new ProductAdminViewModel();
            viewmodel.products= products;
            viewmodel.productCategories= categories;
            viewmodel.comments = comments;

            return View(viewmodel);
            //List<Product> products = context.Collection().Where(c => c.Category == categoryName).ToList();

            //ProductAdminViewModel viewModel = new ProductAdminViewModel();
            //viewModel.product = new Product();
            //viewModel.comments = new Comment();
            //viewModel.productCategories = productCategories.Collection();
            //return View(viewModel);
            ////if (products == null)
            //{
            //    return HttpNotFound();
            //}
            //Product productId = context.Find(Id);
            //if (productId == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.ProductId = Id;
            //List<Comment> usercomments = comments.Collection().Where(c => c.ProductId == Id).ToList();
            //ViewBag.Comments = usercomments;
            //var ratings = comments.Collection().Where(c => c.ProductId == Id).ToList();
            //ViewBag.Ratings = ratings;
            //if (ratings.Count() > 0)
            //{
            //    var ratingSum = ratings.Sum(d => d.Rating);
            //    ViewBag.RatingSum = ratingSum;
            //    var ratingCount = ratings.Count();
            //    ViewBag.RatingCount = ratingCount;
            //}
            //else
            //{
            //    ViewBag.RatingSum = 0;
            //    ViewBag.RatingCount = 0;
            //}





        }
        //public ActionResult Search(string searchString)
        //{
        //    var cat = from c in productCategories.Collection()
        //                   select c;




        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        cat = cat.Where(s => s.CategoryName.Contains(searchString));
        //    }



        //return View(cat.ToList());
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FormCollection form)
        {

            ProductAdminViewModel viewmodel = new ProductAdminViewModel();
            viewmodel.product= new Product();
            
            var comment = form["Comment"].ToString();
            //var Id = form["I viewmodel.productd"].ToString();
            var rating = int.Parse(form["Rating"]);

            Comment userComment = new Comment()
            {
                ProductId = viewmodel.product.Id,
                CommentDescritption = comment,
                Rating = rating,
                CommentedOn = DateTime.Now
            };
            usercomments.Insert(userComment);
            usercomments.Commit();



            return RedirectToAction("ShowRating", "Home");
        }
        public ActionResult ShowRating()

        {


            return View();

        }

        public ActionResult Details(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ProductId = Id;
            //List<Comment> usercomments = comments.Collection().Where(c => c.ProductId == Id).ToList();
            //ViewBag.Comments = comments;
            //var ratings = comments.Collection().Where(c => c.ProductId == Id).ToList();
            //if (ratings.Count() > 0)
            //{
            //    var ratingSum = ratings.Sum(d => d.Rating);
            //    ViewBag.RatingSum = ratingSum;
            //    var ratingCount = ratings.Count();
            //    ViewBag.RatingCount = ratingCount;
            //}
            //else
            //{
            //    ViewBag.RatingSum = 0;
            //    ViewBag.RatingCount = 0;
            //}


            return View(product);
            //else
            //{
            //    return View(product);
            //}
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}