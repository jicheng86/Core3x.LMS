using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Model
{
    public class EntityBase<TPrimaryKey>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public TPrimaryKey ID { get; set; }
    }
}
