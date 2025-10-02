using CaseStudyApi.BusinessLogic.ViewModels.Product;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyApi.BusinessLogic.Validators.ProductValidator
{
    public class ProductValidator : AbstractValidator<AddProductVM>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .Length(5, 15).WithMessage("Name length must be between 5 and 15");

            RuleFor(p => p.Weight)
                .NotNull()
                .NotEmpty().WithMessage("Weight must be greater than 0")
                .Must(w => w > 0.0 && w < 10.0);

            RuleFor(p => p.Stock)
                .NotNull()
                .NotEmpty().WithMessage("Stock can not be empty or null")
                .Must(s => s >= 0);

            RuleFor(p => p.PopularityScore)
                .NotNull()
                .NotEmpty().WithMessage("Popularity score must be between 0 and 5")
                .Must(p => p > 0 && p <= 5);
        }
    }
}
