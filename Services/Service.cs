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
        internal static PagingModelAPI<T> PaginateEntity<T>(IGeneric<T> entityRepo, int size = 5, int page = 1)
        {
            var entityList = entityRepo.GetAll(true).Skip((size * page) - size).Take(size);

            PagingModelAPI<T> model = new PagingModelAPI<T>();
            model.PageSize = size;
            model.TotalElements = entityRepo.GetAll().Count();
            model.NumPage = page;
            model.Elements = entityList.ToList();

            return model;
        }
    }
}
