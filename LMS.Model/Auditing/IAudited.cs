using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Model.Auditing
{
    interface IAudited : ICreationAudited, IModificationAudited
    {
    }
}
