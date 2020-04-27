using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Model
{
    /// <summary>
    /// 部门信息
    /// </summary>
    public class Department : Entity
    {
        public Department()
        {
            Corporation = new Corporation();
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 所属公司ID
        /// </summary>
        public int CorporationID { get; set; }

        public Corporation Corporation { get; set; }
    }
}
