using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Model.Auditing
{
    interface IHasCreationTime 
    {
        public DateTime CreateTime { get; set; }
    }
}
