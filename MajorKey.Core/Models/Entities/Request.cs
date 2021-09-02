using System;
using System.Collections.Generic;
using System.Text;

namespace MajorKey.Core.Models.Entities
{
    public class Request: BaseEntity
    {
        public string BuildingCode { get; set; }
        public string Description { get; set; }
        public CurrentStatus CurrentStatus { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
    }

    public enum CurrentStatus
    {
        NotApplicable,
        Created,
        InProgress,
        Complete,
        Canceled
    }

}
