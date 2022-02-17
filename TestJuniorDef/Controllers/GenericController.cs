using apitest.Models;
using Microsoft.AspNetCore.Mvc;

namespace TestJuniorDef.Controllers
{
    public abstract class GenericController : Controller
    {
        internal void AppendModelError(string key, string msg)
        {
            ModelState.AddModelError(key, msg);
        }

        internal bool BrandValidation(Brand brand)
        {
            bool state = false;
            
            AccountValidation(brand.Account);

            if (brand.Products != null)
            {
                foreach (var prod in brand.Products)
                {
                    state = ProductValidation(prod);
                }
            }

            if (string.IsNullOrWhiteSpace(brand.BrandName) || brand.BrandName.Length > 100)
            {
                state = false;
                ModelState.AddModelError("brand.Name", $"The brand name is invalid.");
            }
            if (brand.AccountId < 1)
            {
                state = false;
                ModelState.AddModelError("brand.AccountId", $"The AccountId is invalid.");
            }
            if (string.IsNullOrWhiteSpace(brand.Description))
            {
                state = false;
                ModelState.AddModelError("brand.Description", $"Description is required.");
            }

            return state;
        }

        internal bool AccountValidation(Account account)
        {
            bool state = true;

            if (string.IsNullOrWhiteSpace(account.Email) || account.Email.Length > 319)
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

        internal bool InsertProductValidation(Product product)
        {
            bool state;

            state = ProductValidation(product);

            if (product.BrandId < 1)
            {
                state = false;
                ModelState.AddModelError("Product.BrandId", $"The product BrandId is invalid.");
            }

            return state;
        }

        internal bool ProductValidation(Product product)
        {
            bool state = true;

            if (string.IsNullOrWhiteSpace(product.Name) || product.Name.Length > 80)
            {
                state = false;
                ModelState.AddModelError("Product.Name", $"The product name is invalid.");
            }
            if (string.IsNullOrWhiteSpace(product.ShortDescription))
            {
                state = false;
                ModelState.AddModelError("Product.ShortDescription", $"Short description is required.");
            }
            if (string.IsNullOrWhiteSpace(product.Description))
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
