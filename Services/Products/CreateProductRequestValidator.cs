using FluentValidation;
using Repositories.Products;

namespace Services.Products;
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    private readonly IProductRepository _productRepository;
    public CreateProductRequestValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ürün adý boþ olamaz.")
            .Length(3, 10).WithMessage("Ürün adý 3 ile 10 karakter arasýnda olmalýdýr.");
            //.Must(MustUniqueProductName).WithMessage("Ürün adý veritabanýnda bulunmaktadýr.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Ürün fiyatý 0'dan büyük olmalýdýr.");

        RuleFor(x => x.Stock)
            .InclusiveBetween(1, 100).WithMessage("Stok 1 ile 100 arasýnda olmalýdýr.");

    }

    //senkron validasyon
    //private bool MustUniqueProductName(string name)
    //{
    //    return !_productRepository.Where(x => x.Name == name).Any();
    //}
}
