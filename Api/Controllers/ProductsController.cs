using Microsoft.AspNetCore.Mvc;
using Repositories.Products;
using Services.Filters;
using Services.Products;
using Services.Products.Create;
using Services.Products.Update;
using Services.Products.UpdateStock;

namespace Api.Controllers;

public class ProductsController(IProductService productService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => CreateActionResult(await productService.GetAllAsync());

    [HttpGet("{pageNumber:int}/{pageSize:int}")]
    public async Task<IActionResult> GetPagedAll(int pageNumber, int pageSize)
        => CreateActionResult(await productService.GetPagedAllAsync(pageNumber,pageSize));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
        => CreateActionResult(await productService.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        =>  CreateActionResult(await productService.CreateAsync(request));    

    [ServiceFilter(typeof(NotFoundFilter<Product,int>))]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductRequest request)
        =>  CreateActionResult(await productService.UpdateAsync(id,request));

    [HttpPatch("stock")]
    public async Task<IActionResult> UpdateStock(UpdateProductStockRequest request)
        => CreateActionResult(await productService.UpdateProductStockAsync(request));

    [ServiceFilter(typeof(NotFoundFilter<Product,int>))]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        =>  CreateActionResult(await productService.DeleteAsync(id));
}
