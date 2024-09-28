using FluentValidation;

namespace Services.Products.Update;
public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("�r�n ad� bo� olamaz.")
            .Length(3, 10).WithMessage("�r�n ad� 3 ile 10 karakter aras�nda olmal�d�r.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("�r�n fiyat� 0'dan b�y�k olmal�d�r.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("�r�n kategori de�eri 0'dan b�y�k olmal�d�r.");

        RuleFor(x => x.Stock)
            .InclusiveBetween(1, 100).WithMessage("Stok 1 ile 100 aras�nda olmal�d�r.");
    }
}
