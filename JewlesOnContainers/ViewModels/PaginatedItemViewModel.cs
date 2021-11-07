using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductCatalogApi.Domain;

namespace ProductCatalogApi.ViewModels
{
    public class PaginatedItemViewModel

    {
        public IEnumerable<CatalogItem> Data { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }

    }
}
