using System;
using System.Collections.Generic;
using System.Text;
using LMS.Model.Auditing;

namespace LMS.Model
{
    /// <summary>
    /// 员工信息
    /// </summary>
    public class Employee : EntityBase<int>, IFullAudited
    {


        /// <summary>
        /// 用户名称
        /// </summary>
        public string ENmae { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int EGender { get; set; }

        public bool IsActive { get; set; }
        public long CreatorUserId { get; set; }
        public DateTime CreateTime { get; set; }
        public long? ModificationUserID { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
