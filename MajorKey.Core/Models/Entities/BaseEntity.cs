using System;
using System.Collections.Generic;
using System.Text;

namespace MajorKey.Core.Models.Entities
{
    public class BaseEntity
    {
        public virtual long Id { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime LastModifiedDate { get; set; }
    }
}
