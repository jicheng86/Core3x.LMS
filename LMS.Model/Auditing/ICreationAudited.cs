using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Model.Auditing
{
    interface ICreationAudited : IHasCreationTime
    {
        public long CreatorUserId { get; set; }
    }
}
