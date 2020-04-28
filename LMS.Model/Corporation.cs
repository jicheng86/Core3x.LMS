using System;
using System.Collections.Generic;
using LMS.Model.Auditing;

namespace LMS.Model
{
    public class Corporation : Entity, IFullAudited
    {
        public Corporation()
        {
            //初始化部门集合
            this.Departments = new List<Department>();
        }
        public string CorporationAddress { get; set; }
        public virtual List<Department> Departments { get; set; }
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
