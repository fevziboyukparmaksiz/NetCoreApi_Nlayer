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
            .NotEmpty().WithMessage("�r�n ad� bo� olamaz.")
            .Length(3, 10).WithMessage("�r�n ad� 3 ile 10 karakter aras�nda olmal�d�r.");
            //.Must(MustUniqueProductName).WithMessage("�r�n ad� veritaban�nda bulunmaktad�r.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("�r�n fiyat� 0'dan b�y�k olmal�d�r.");

        RuleFor(x => x.Stock)
            .InclusiveBetween(1, 100).WithMessage("Stok 1 ile 100 aras�nda olmal�d�r.");

    }

    //senkron validasyon
    //private bool MustUniqueProductName(string name)
    //{
    //    return !_productRepository.Where(x => x.Name == name).Any();
    //}
}
