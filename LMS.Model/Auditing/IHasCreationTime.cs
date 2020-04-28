using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Model.Auditing
{
    /// <summary>
    /// 创建时间
    /// </summary>
    interface IHasCreationTime 
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
