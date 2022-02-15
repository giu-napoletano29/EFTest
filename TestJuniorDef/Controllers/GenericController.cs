using apitest.Models;
using Microsoft.AspNetCore.Mvc;

namespace TestJuniorDef.Controllers
{
    public abstract class GenericController : Controller
    {
        internal bool BrandValidation(Brand brand)
        {
            bool state = true;

            if (brand.BrandName.Length < 1 || brand.BrandName.Length > 100)
            {
                state = false;
                ModelState.AddModelError("brand.Name", $"The brand name is invalid.");
            }
            if (brand.AccountId < 1)
            {
                state = false;
                ModelState.AddModelError("brand.AccountId", $"The AccountId is invalid.");
            }
            if (brand.Description.Length < 1)
            {
                state = false;
                ModelState.AddModelError("brand.Description", $"Description is required.");
            }

            state = AccountValidation(brand.Account);

            foreach (var prod in brand.Products)
            {
                state = ProductValidation(prod);
            }

            return state;
        }

        internal bool AccountValidation(Account account)
        {
            bool state = true;

            if (account.Email.Length < 1 || account.Email.Length > 319)
            {
                state = false;
                ModelState.AddModelError("account.Email", $"The account email is invalid.");
            }
            if (account.AccountType < 1 || account.AccountType > 2)
            {
                state = false;
                ModelState.AddModelError("account.AccountId", $"The account type is invalid.");
            }

            return state;
        }

        internal bool ProductValidation(Product product)
        {
            bool state = true;

            if (product.Name.Length < 1 || product.Name.Length > 80)
            {
                state = false;
                ModelState.AddModelError("Product.Name", $"The product name is invalid.");
            }
            if (product.BrandId < 0)
            {
                state = false;
                ModelState.AddModelError("Product.BrandId", $"The product BrandId is invalid.");
            }
            if (product.ShortDescription.Length < 1)
            {
                state = false;
                ModelState.AddModelError("Product.ShortDescription", $"Short description is required.");
            }
            if (product.Description.Length < 1)
            {
                state = false;
                ModelState.AddModelError("Product.Description", $"Description is required.");
            }
            if (product.Price < 0)
            {
                state = false;
                ModelState.AddModelError("Product.Price", $"Price is invalid.");
            }

            return state;
        }
    }
}
