using MajorKey.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MajorKey.Core.Models.DataTransfer
{
    public class UpdateRequestDto: BaseDto
    {
        public string BuildingCode { get; set; }
        public string Description { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
