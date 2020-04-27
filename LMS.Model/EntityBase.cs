using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LMS.Model
{
    public class EntityBase<TPrimaryKey>
    {
        /// <summary>
        /// 实体主键ID
        /// </summary>
        public TPrimaryKey ID { get; set; }
        /// <summary>
        /// 实体名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remarks { get; set; }
    }
}
