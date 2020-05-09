﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "字段：{0}，不能为空！")]
        public Gender EmployeeGender { get; set; }
        [Required(ErrorMessage = "字段：{0}，不能为空！")]
        public int DepartmentID { get; set; }
        public Department Department { get; set; }

        /// <summary>
        /// 创建者ID
        /// </summary>
        [Required(ErrorMessage = "字段：{0}，不能为空！"), Display(Name = "创建者ID")]
        public long CreatorUserId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Required(ErrorMessage = "字段：{0}，不能为空！"), Display(Name = "创建时间")]
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 修改者ID
        /// </summary>
        [Display(Name = "修改者ID")]
        public long? ModificationUserID { get; set; }
        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        [Display(Name = "最后一次修改时间")]
        public DateTime? LastModificationTime { get; set; }
        /// <summary>
        /// 是否活跃状态
        /// </summary>
        [Required(ErrorMessage = "字段：{0}，不能为空！"), Display(Name = "是否活跃状态")]
        public bool IsActive { get; set; }
        /// <summary>
        /// 是否删除状态
        /// </summary>
        [Required(ErrorMessage = "字段：{0}，不能为空！"), Display(Name = "是否删除状态")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 删除操作者ID
        /// </summary>
        [Display(Name = "删除操作时间")]
        public long? DeleterUserId { get; set; }
        /// <summary>
        /// 删除操作时间
        /// </summary>
        [Display(Name = "删除操作时间")]
        public DateTime? DeletionTime { get; set; }
    }
}
