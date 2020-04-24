using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Model.Auditing
{
    interface IHasModificationTime
    {
        public DateTime? LastModificationTime { get; set; }
    }
}
