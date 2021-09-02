using System;
using System.Collections.Generic;
using System.Text;

namespace MajorKey.Core.Models.DataTransfer
{
    public class CreateRequestDto: BaseDto
    {
        public string BuildingCode { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
    }
}
