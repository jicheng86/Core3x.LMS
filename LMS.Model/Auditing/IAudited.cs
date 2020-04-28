using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Model.Auditing
{
    /// <summary>
    /// 创建信息，修改信息
    /// </summary>
    interface IAudited : ICreationAudited, IModificationAudited
    {
    }
}
