using apitest.Models;
using System.Collections;
using System.Collections.Generic;

namespace TestJuniorDef.ModelAPI
{
    public class PagingModelAPI<T>
    {
        public int NumPage { get; set; }
        public int PageSize { get; set; }
        public int TotalElements { get; set; }
        public IEnumerable<T> Elements { get; set; }
    }
}
