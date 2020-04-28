using System;
using System.Collections.Generic;
using System.Text;
using LMS.Model.Auditing;

namespace LMS.Model
{
    public class Area : Entity, ICreationAudited, IModificationAudited, IActively
    {

        public int ParentId { get; set; }
        public long CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? ModificationUserID { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public bool IsActive { get; set; }
    }
}
