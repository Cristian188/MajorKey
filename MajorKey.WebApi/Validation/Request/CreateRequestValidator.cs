using FluentValidation;
using MajorKey.Core.Models.DataTransfer;

namespace MajorKey.Validation.Request
{
    public class CreateRequestValidator : AbstractValidator<CreateRequestDto>
	{
		public CreateRequestValidator()
		{
			RuleFor(x => x.BuildingCode).NotEmpty().Length(0, 10);
			RuleFor(x => x.Description).NotEmpty().Length(0, 255);
			RuleFor(x => x.CreatedBy).NotEmpty().Length(0, 255);
		}
	}
}
