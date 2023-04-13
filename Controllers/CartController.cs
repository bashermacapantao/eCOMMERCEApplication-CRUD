using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCOMMERCEApplication.Models;

namespace eCOMMERCEApplication.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        ProductDBEntities db = new ProductDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddToCart(int ProductID) 
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                //Item item = new Item();
                //item.Product = db.tblProducts.Find(ProductID);
                //item.Quantity = 1;
                //cart.Add(item);

                //shortcut method
                cart.Add(new Item() { Product = db.tblProducts.Find(ProductID), Quantity = 1 });

                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                //no item in cart
                int index = IsInCart(ProductID);
                if (index !=-1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item()
                    {
                        Product = db.tblProducts.Find(ProductID), Quantity = 1
                    });
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromCart(int ProductID)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int index = IsInCart(ProductID);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

        //to know if the item is already in the cart
        public int IsInCart(int ProductID)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.prod_id == ProductID)
                {
                    return i;
                }
            }
            //return -1 if you dont find item in the cart
            return -1;
        }
    }
}