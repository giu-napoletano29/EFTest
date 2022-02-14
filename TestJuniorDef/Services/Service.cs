using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TestJuniorDef.ModelAPI;
using TestJuniorDef.Repositories.Interfaces;
using TestJuniorDef.Services.Interfaces;
[assembly: InternalsVisibleTo("TestJuniorDef.Services")]


namespace TestJuniorDef.Services
{
    internal static class Service
    {
        /// <summary>
        /// Return a given entity paginated with the specified page size and page number
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityRepo"></param>
        /// <param name="size"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        internal static PagingModelAPI<T> PaginateEntity<T>(IQueryable<T> entityRepo, int size = 5, int page = 1)
        {
            //var entityList = entityRepo.GetAll(true).Skip((size * page) - size).Take(size);
            var entityList = entityRepo.Skip((size * page) - size).Take(size);

            PagingModelAPI<T> model = new PagingModelAPI<T>();
            model.PageSize = size;
            //model.TotalElements = entityRepo.GetAll().Count();
            model.TotalElements = entityRepo.Count();
            model.NumPage = page;
            model.Elements = entityList.ToList();

            return model;
        }
    }
}
