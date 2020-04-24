using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Model.Auditing
{
    interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
    }
}
