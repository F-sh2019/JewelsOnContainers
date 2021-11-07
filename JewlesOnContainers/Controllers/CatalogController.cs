using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductCatalogApi.Data;
using ProductCatalogApi.Domain;
using ProductCatalogApi.ViewModels;

namespace ProductCatalogApi.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {

        private readonly CatalogContext _Context;
        private readonly IConfiguration _Config;
        public CatalogController(CatalogContext context , IConfiguration configuration )
        {
            _Context = context;
            _Config = configuration;

        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CatalogBrands()
        {
            var brands = await _Context.CatalogBrands.ToListAsync();
            return Ok(brands);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CatalogTypes()
        {
            var Catalogtypes = await _Context.CatalogTypes.ToListAsync();
            return Ok(Catalogtypes);
        }

        [HttpGet("[action]/type/{CategoryTypeId}/brand/{catalogBrandId}")]
         public async Task<IActionResult> CatalogItems(
            int? catalogTypeId,
            int? catalogBrandId,
            [FromQuery] int PageIndex=0,
            [FromQuery]int PageSize=6)
        {

            var query = (IQueryable<CatalogItem>)_Context.CatalogItems;
           
            var itemCount =  _Context.CatalogItems.LongCountAsync();
            if (catalogTypeId.HasValue)
            {
                query = query.Where(c => c.CatalogTypeId == catalogTypeId);
            }
            if (catalogBrandId.HasValue)
            {
                query = query.Where(c => c.CatalogBrandId == catalogBrandId);
            }

            var Items = await query.OrderBy(i => i.Name)
                                            .Skip(PageSize * PageIndex)
                                            .Take(PageSize)
                                            .ToListAsync();
            Items = ChangePictureUrl(Items);

            var model = new PaginatedItemViewModel
            {
                Data = Items,
                PageIndex = PageIndex,
                PageSize = Items.Count
            };

            return Ok(Items);
    
        }

        private List<CatalogItem> ChangePictureUrl(List<CatalogItem> Items)
        {
            Items.ForEach(item => item.PictureUrl=item.PictureUrl.Replace("http://externalcatalogbaseurltobereplaced", _Config["ExternalCatalogUrl"]));
            return (Items);
        }
    }
}
