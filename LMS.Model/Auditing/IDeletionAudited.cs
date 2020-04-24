using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Model.Auditing
{
    interface IDeletionAudited : ISoftDelete
    {
        long? DeleterUserId { get; set; }

        DateTime? DeletionTime { get; set; }
    }
}
