using FluentValidation;

namespace Services.Products.Update;
public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ürün adý boþ olamaz.")
            .Length(3, 10).WithMessage("Ürün adý 3 ile 10 karakter arasýnda olmalýdýr.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Ürün fiyatý 0'dan büyük olmalýdýr.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("ürün kategori deðeri 0'dan büyük olmalýdýr.");

        RuleFor(x => x.Stock)
            .InclusiveBetween(1, 100).WithMessage("Stok 1 ile 100 arasýnda olmalýdýr.");
    }
}
