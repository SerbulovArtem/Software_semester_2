using AutoMapper;
using ActionManager.BLL.Repositories.Concreate.DataBaseMCSQLActionManager;
using ActionManager.DAL.Data;
using ActionManager.DAL.Repositories.Abstract.DataBaseMCSQLActionManager;
using ActionManager.DTO;
using ActionManager.DAL.Repositories.Concreate.DataBaseMCSQLActionManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WEB_MVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;

namespace WEB_MVC.Controllers
{
    public class ActionController : Controller
    {
        private readonly IActionsRepository _actionsRepository;
        private readonly IMapper _mapper;

        public ActionController(IActionsRepository actionsRepository, IMapper mapper)
        {
            _actionsRepository = actionsRepository;
            _mapper = mapper;

        }

        public void GetProductUserLists()
        {
            var users = _actionsRepository.GetListOfUsers();
            var products = _actionsRepository.GetListOfProducts();
            IEnumerable<SelectListItem> GetUsers = users.Select(u => new SelectListItem
            {
                Value = u.UserId.ToString(),
                Text = u.UserName
            });

            IEnumerable<SelectListItem> GetProducts = products.Select(p => new SelectListItem
            {
                Value = p.ProductId.ToString(),
                Text = p.ProductName
            });
            ViewBag.Users = new SelectList(GetUsers, "Value", "Text");
            ViewBag.Products = new SelectList(GetProducts, "Value", "Text");
        }

        // GET: CartController
        public ActionResult Index()
        {
            List<Cart> carts = _cartBL.GetAllCarts();
            List<CartModel> cartModels = _mapper.Map<List<CartModel>>(carts);
            return View(cartModels);
        }

        // GET: CartController/Details/5
        public ActionResult Details(int id)
        {
            Cart cart = _cartBL.GetCartById(id);
            CartModel cartModel = _mapper.Map<CartModel>(cart);
            return View(cartModel);
        }

        // GET: CartController/Create
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            GetProductUserLists();
            return View();
        }

        // POST: CartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CartModel cartModel)
        {
            if (ModelState.IsValid)
            {
                Cart cart = _mapper.Map<Cart>(cartModel);
                _cartBL.AddCart(cart);
                return RedirectToAction("Index", "Cart");
            }


            return View(cartModel);
        }

        // GET: CartController/Edit/5
        [Authorize(Roles = "User")]
        public ActionResult Edit(int id)
        {
            Cart cart = _cartBL.GetCartById(id);
            CartModel cartModel = _mapper.Map<CartModel>(cart);
            List<Cart> carts = _cartBL.GetAllCarts();
            GetProductUserLists();
            return View(cartModel);

        }

        // POST: CartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CartModel cartModel)
        {
            if (ModelState.IsValid)
            {

                Cart cart = _mapper.Map<Cart>(cartModel);
                cart.CartId = id;
                _cartBL.UpdateCart(cart);
                return RedirectToAction(nameof(Index));
            }
           
            return View(cartModel);
        }

        // GET: CartController/Delete/5
        [Authorize(Roles = "User")]
        public ActionResult Delete(int id)
        {
            Cart cart = _cartBL.GetCartById(id);
            CartModel cartModel = _mapper.Map<CartModel>(cart);
            return View(cartModel);
        }

        // POST: CartController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            _cartBL.DeleteCart(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
