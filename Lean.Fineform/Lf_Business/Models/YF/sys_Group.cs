//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fine.Lf_Business.Models.YF
{
    using System;
    using System.Collections.Generic;
    
    public partial class sys_Group
    {
        public int GroupID { get; set; }
        public string G_CName { get; set; }
        public int G_ParentID { get; set; }
        public int G_ShowOrder { get; set; }
        public Nullable<int> G_Level { get; set; }
        public Nullable<int> G_ChildCount { get; set; }
        public Nullable<byte> G_Delete { get; set; }
    }
}
