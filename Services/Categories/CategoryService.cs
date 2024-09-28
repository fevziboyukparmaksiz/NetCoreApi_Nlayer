using AutoMapper;
using Repositories.Categories;
using Services.Categories.Create;
using Services.Categories.Update;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Categories.Dto;

namespace Services.Categories;
public class CategoryService(
    ICategoryRepository categoryRepository, 
    IUnitOfWork unitOfWork,
    IMapper mapper) 
    : ICategoryService
{
    public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProductsAsync(int categoryId)
    {
        var category = await categoryRepository.GetCategoryWithProductsAsync(categoryId);

        if (category is null)
        {
            return ServiceResult<CategoryWithProductsDto>.Fail("Category Not Found",HttpStatusCode.NotFound);
        }

        var categoryAsDto = mapper.Map<CategoryWithProductsDto>(category);

        return ServiceResult<CategoryWithProductsDto>.Success(categoryAsDto);

    }

    public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync()
    {
        var category = await categoryRepository.GetCategoryWithProducts().ToListAsync();

        var categoryAsDto = mapper.Map<List<CategoryWithProductsDto>>(category);

        return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryAsDto);
    }

    public async Task<ServiceResult<List<CategoryDto>>> GetAllAsync()
    {
        var category = await categoryRepository.GetAll().ToListAsync();

        var categoryAsDto = mapper.Map<List<CategoryDto>>(category);

        return ServiceResult<List<CategoryDto>>.Success(categoryAsDto);

    }

    public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
    {
        var category = await categoryRepository.GetById(id);

        if (category is null)
        {
            return ServiceResult<CategoryDto>.Fail("Category Not Found",HttpStatusCode.NotFound);
        }

        var categoryAsDto = mapper.Map<CategoryDto>(category);

        return ServiceResult<CategoryDto>.Success(categoryAsDto);
    }

    public async Task<ServiceResult<CreateCategoryResponse>> CreateAsync(CreateCategoryRequest request)
    {
        var anyCategory = await categoryRepository.Where(x => x.Name == request.Name).AnyAsync();

        if (anyCategory)
        {
            return ServiceResult<CreateCategoryResponse>.Fail("categori ismi veritabanýnda bulunmaktadýr.",
                HttpStatusCode.NotFound);
        }

        var newCategory = mapper.Map<Category>(request);

        await categoryRepository.AddAsync(newCategory);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(newCategory.Id), $"api/categories/{newCategory.Id}");
    }


    public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request)
    {
        var isCategoryNameExist =
            await categoryRepository.Where(x => x.Name == request.Name && x.Id != id).AnyAsync();

        if (isCategoryNameExist)
        {
            return ServiceResult.Fail("kategori ismi veritabanýnda bulunmaktadýr.");
        }

        var category = mapper.Map<Category>(request);
        category.Id = id;

        categoryRepository.Update(category);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }


    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var category = await categoryRepository.GetById(id);

        categoryRepository.Delete(category!);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}
