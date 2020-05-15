using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Model
{
    /// <summary>
    /// 分页数据辅助类
    /// </summary>
    public class PageData<T> where T : class
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public List<T> Data { get; set; }
    }
}
