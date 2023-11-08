using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Fineform
{
    //操作日志表
    public class Adm_OperateLog : IKeyGUID
    {
        [Key]
        public Guid GUID { get; set; }
        [Required, StringLength(20)]
        public string OperateUserId { get; set; }//操作人员ID
        [Required, StringLength(20)]
        public string OperateUserName { get; set; }//操作人员名称
        [StringLength(20)]//部门
        public string OperateUserDept { get; set; }//操作人员部门
        [Required, StringLength(8)]
        public string OperateType { get; set; }//操作标记

        [Required,StringLength(20)]
        public string OperateModules { get; set; }//操作模块名称

        [Required, StringLength(40)]
        public string OperatePowers { get; set; }//操作权限名称

        [Required, StringLength(4000)]
        public string OperateNotes { get; set; }//操作记录
        [StringLength(15)]
        public string HostName { get; set; }//操作主机名
        [StringLength(15)]
        public string HostIpAddress { get; set; }//操作主机IP


        [StringLength(150)]
        public string HostBrowser { get; set; }//操作主机浏览器
        public DateTime OperateTime { get; set; }//操作时间


    }
}