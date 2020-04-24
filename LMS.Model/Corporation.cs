using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Microsoft.VisualBasic;

namespace LMS.Model
{
    public class Corporation
    {
        public Corporation()
        {
            this.Departments = new Collection<Department>();
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CorpName { get; set; }
        public string CorpAddress { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
    }
}
