using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Model.Enums
{
    public static class EnumCollection
    {
        public enum Gender
        {
            [Display(Name = "女性")]
            Female = 0,
            [Display(Name = "男性")]
            Male = 1,
            [Display(Name = "保密")]
            Secret = 2
        }
        #region 系统相关

        /// <summary>
        /// 响应返回数据状态code
        /// </summary>
        public enum RespondStatusCode
        {
            /// <summary>
            /// 请求失败
            /// </summary>
            [Description("请求失败")]
            Fail = 0,
            /// <summary>
            /// 请求成功
            /// </summary>
            [Description("请求成功")]
            Success = 1,
            /// <summary>
            /// 内部错误
            /// </summary>
            [Description("内部错误")]
            Error = 2,
            /// <summary>
            /// 请求非法
            /// </summary>
            [Description("请求非法")]
            Illega = 3
        }
        /// <summary>
        /// 日志等级
        /// </summary>
        public enum LogsLevel
        {
            /// <summary>
            /// Debug -调试级别
            /// </summary>
            [Description("Debug")]
            Debug = 0,

            /// <summary>
            /// Info -一般信息级别
            /// </summary>
            [Description("Info")]
            Info = 1,

            /// <summary>
            /// Warn -警告级别
            /// </summary>
            [Description("Warn")]
            Warn = 2,

            /// <summary>
            /// Error -一般错误级别
            /// </summary>
            [Description("Error")]
            Error = 3,

            /// <summary>
            /// Fatal -致命错误级别
            /// </summary>
            [Description("Fatal")]
            Fatal = 4
        }
        /// <summary>
        /// 日志模块需跟log4NetCore.xml配置一致
        /// </summary>
        public enum LogsModule
        {
            /// <summary>
            /// 一般日志模块
            /// </summary>
            [Description("一般日志模块")]
            InfoLogs = 0,

            /// <summary>
            /// 错误日志模块
            /// </summary>
            [Description("错误日志模块")]
            ErrorLogs = 1,

            /// <summary>
            ///调试日志模块
            /// </summary>
            [Description("调试日志模块")]
            DebugLogs = 2,

            /// <summary>
            /// 账号模块日志
            /// </summary>
            [Description("账号模块日志")]
            LoginLogs = 11,

            /// <summary>
            /// 业务模块日志
            /// </summary>
            [Description("业务模块日志")]
            Business = 21,

            /// <summary>
            /// 财务模块日志
            /// </summary>
            [Description("财务模块日志")]
            Finance = 31,

        }
        #endregion

        #region HTTP 请求相关
        /// <summary>
        /// HTTP请求返回码
        /// </summary>
        public enum ResponseStatus
        {
            /// <summary>
            /// 操作失败
            /// </summary>
            [Description("操作失败")]
            FAIL = 0,

            /// <summary>
            /// 操作成功
            /// </summary>
            [Description("操作成功")]
            SUCCESS = 1,

            /// <summary>
            /// 操作异常
            /// </summary>
            [Description("操作异常")]
            ERROR = 2,

            /// <summary>
            /// 参数丢失
            /// </summary>
            [Description("参数丢失")]
            ARGUMENTSLOSE = 3,

            /// <summary>
            /// 未知错误
            /// </summary>
            [Description("未知错误")]
            UNKNOWN = 99
        }
        #endregion
    }
}
