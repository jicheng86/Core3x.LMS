using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Model.Auditing
{
    /// <summary>
    /// 修改信息
    /// </summary>
    interface IModificationAudited : IHasModificationTime
    {
        public long? ModificationUserID { get; set; }
    }
}
