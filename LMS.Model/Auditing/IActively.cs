using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Model.Auditing
{
    interface IActively
    {
        public bool IsActive { get; set; }
    }
}
