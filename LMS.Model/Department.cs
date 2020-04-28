using System;
using System.Collections.Generic;
using System.Text;
using LMS.Model.Auditing;

namespace LMS.Model
{
    /// <summary>
    /// 部门信息
    /// </summary>
    public class Department : Entity, IFullAudited
    {
        public Department()
        {
            Corporation = new Corporation();
        }

        /// <summary>
        /// 所属公司ID
        /// </summary>
        public int CorporationID { get; set; }

        public Corporation Corporation { get; set; }
        public long CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? ModificationUserID { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
