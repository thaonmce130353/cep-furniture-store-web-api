using CQRS.MediatR.Practice.Features.Products.Command;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRS.MediatR.Practice.Validators
{
    public class CreateProductCommndValidator : AbstractValidator<CreateNewProduct.Command>
    {
        public CreateProductCommndValidator()
        {
            RuleFor(c => c.name).NotEmpty();
            RuleFor(c => c.price).GreaterThan(0);
        }
    }
}
