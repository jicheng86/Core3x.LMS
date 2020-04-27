using System.Collections.Generic;

namespace LMS.Model
{
    public class Corporation : Entity
    {
        public Corporation()
        {
            //初始化部门集合
            this.Departments = new List<Department>();
        }
        public string CorporationAddress { get; set; }
        public virtual List<Department> Departments { get; set; }
    }
}
