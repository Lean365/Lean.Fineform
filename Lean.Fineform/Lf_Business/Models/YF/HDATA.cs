//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LeanFine.Lf_Business.Models.YF
{
    using System;
    using System.Collections.Generic;
    
    public partial class HDATA
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> PRODUCTIONDATE { get; set; }
        public string WORKINGHOURS { get; set; }
        public string LOTNO { get; set; }
        public Nullable<int> LOTQTY { get; set; }
        public string MODEL { get; set; }
        public Nullable<double> ST { get; set; }
        public string TEAM_LINE { get; set; }
        public Nullable<int> DIRECTWORKER { get; set; }
        public Nullable<int> INDIRECTIONWORKER { get; set; }
    }
}
