using System;
using System.Collections.Generic;
using System.Text;
using static LMS.Model.Enums.EnumCollection;

namespace LMS.Model
{
    /// <summary>
    /// 返回json数据实体类
    /// </summary>
    public class JsonData
    {
        /// <summary>
        /// 返回状态码Code
        /// </summary>
        public RespondStatusCode Code { get; set; }
        /// <summary>
        /// 描述信息Msg
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 数据体Data
        /// </summary>
        public object Data { get; set; }
    }
}
