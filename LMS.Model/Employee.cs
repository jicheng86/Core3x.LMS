using System;
using System.Collections.Generic;
using System.Text;
using LMS.Model.Auditing;
using static LMS.Model.Enums.EnumCollection;

namespace LMS.Model
{
    /// <summary>
    /// 员工信息
    /// </summary>
    public class Employee : Entity, IFullAudited
    {
        /// <summary>
        /// 员工性别
        /// </summary>
        public Gender EmployeeGender { get; set; }

        public int DepartmentID { get; set; }
        public Department Department { get; set; }

        public bool IsActive { get; set; }
        public long CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? ModificationUserID { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
