using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Model.Auditing
{
    interface IModificationAudited : IHasModificationTime
    {
        public long? ModificationUserID { get; set; }
    }
}
