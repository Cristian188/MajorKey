using FluentValidation;
using MajorKey.Core.Models.DataTransfer;
using MajorKey.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MajorKey.Validation.Request
{
    public class UpdateRequestValidator : AbstractValidator<UpdateRequestDto>
	{
		public UpdateRequestValidator()
		{
			RuleFor(x => x.Id).NotEmpty();
			RuleFor(x => x.BuildingCode).Length(0, 10);
			RuleFor(x => x.Description).Length(0, 255);
			RuleFor(x => x.CurrentStatus).IsInEnum();
			RuleFor(x => x.LastModifiedBy).NotEmpty().Length(0, 255);
		}
	}
}
