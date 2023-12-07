using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Lean.Fineform
{
    public class LeanContext : DbContext
    {

        public LeanContext()
           : base("SQLServer")
        {
            this.Database.CommandTimeout= 3600000;
            //this.Database.CommandTimeout = 3600000; //时间单位是毫秒



        }


        public DbSet<Adm_Config> Adm_Configs { get; set; }
        public DbSet<Adm_Institution> Adm_Institutions { get; set; }
        public DbSet<Adm_Corpkpi> Adm_Corpkpis { get; set; }
        public DbSet<Adm_Formula> Adm_Formulas { get; set; }
        public DbSet<Adm_Dept> Adm_Depts { get; set; }
        public DbSet<Adm_User> Adm_Users { get; set; }
       
        public DbSet<Adm_Role> Adm_Roles { get; set; }
        public DbSet<Adm_Title> Adm_Titles { get; set; }
        public DbSet<Adm_Online> Adm_Onlines { get; set; }
        public DbSet<Adm_Log> Adm_Logs { get; set; }
        public DbSet<Adm_Power> Adm_Powers { get; set; }
        public DbSet<Adm_Menu> Adm_Menus { get; set; }
        public DbSet<Adm_OperateLog> Adm_OperateLogs { get; set; }
        public DbSet<Adm_TheDate> Adm_TheDates { get; set; }
        public DbSet<Adm_TheDict> Adm_TheDicts { get; set; }

        //OA
        public DbSet<Oa_Contact> Oa_Contacts { get; set; }
        public DbSet<Oa_Notice> Oa_Notices { get; set; }

        public DbSet<Oa_Meeting> Oa_Meetings { get; set; }

        public DbSet<Oa_Meeting_Room> Oa_Meeting_Rooms { get; set; }
        public DbSet<Oa_Type_List> Oa_Type_Lists { get; set; }
        public DbSet<Oa_Vehicle> Oa_Vehicles { get; set; }
        public DbSet<Oa_Vehicle_Info> Oa_Vehicle_Infos { get; set; }
        public DbSet<Oa_Vehicle_Maintain> Oa_Vehicle_Maintains { get; set; }
        public DbSet<Oa_Service_Desk> Oa_Service_Desks { get; set; }
        public DbSet<Oa_Weekly> Oa_Weeklys { get; set; }
        public DbSet<Oa_Position_City> Oa_Position_Citys { get; set; }
        public DbSet<Oa_Position_County> Oa_Position_Countys { get; set; }
        public DbSet<Oa_Position_Provice> Oa_Position_Provices { get; set; }
        public DbSet<Oa_Position_Town> Oa_Position_Towns { get; set; }
        public DbSet<Oa_Position_Village> Oa_Position_Villages { get; set; }


        //BPM
        public DbSet<Bpm_FLow> Bpm_FLows { get; set; }
        public DbSet<Bpm_Flow_Archive> Bpm_Flow_Archivess { get; set; }
        public DbSet<Bpm_Flow_Button> Bpm_Flow_Buttonss { get; set; }
        public DbSet<Bpm_Flow_Comment> Bpm_Flow_Comments { get; set; }
        public DbSet<Bpm_Flow_Delegation> Bpm_Flow_Delegations { get; set; }
        public DbSet<Bpm_Flow_Form> Bpm_Flow_Forms { get; set; }
        public DbSet<Bpm_Flow_Task> Bpm_Flow_Tasks { get; set; }
        public DbSet<Bpm_Type> Bpm_Types { get; set; }
        //FM
        public DbSet<Fm_Form> Fm_Forms { get; set; }

        public DbSet<Fm_Type> Fm_Types { get; set; }
        //EM
        public DbSet<Em_Event> Em_Events { get; set; }
        public DbSet<Em_Event_Type> Em_Event_Types { get; set; }

        //FICO
        public DbSet<Fico_ExchangeRate> Fico_ExchangeRates{ get; set; }
        
        public DbSet<Fico_ProfitCenter> Fico_ProfitCenters { get; set; }
        public DbSet<Fico_Asset> Fico_Assets { get; set; }
        public DbSet<Fico_Expense> Fico_Expenses { get; set; }
        public DbSet<Fico_Overtime> Fico_Overtimes { get; set; }
        public DbSet<Fico_Staff> Fico_Staffs { get; set; }
        public DbSet<Fico_Period> Fico_Periods { get; set; }
        public DbSet<Fico_Title> Fico_Titles { get; set; }
        public DbSet<Fico_Counter_Signature> Fico_Counter_Signatures { get; set; }

        //成本分析（costing)
        public DbSet<Fico_Costing_BomCost> Fico_Costing_BomCosts { get; set; }
        public DbSet<Fico_Costing_FobData> Fico_Costing_FobDatas { get; set; }

       public DbSet<Fico_Costing_HistInventory> Fico_Costing_HistInventorys { get; set; }

  
        public DbSet<Fico_Costing_SalesInvoice> Fico_Costing_SalesInvoices { get; set; }
        public DbSet<Fico_Costing_ActualCost> Fico_Costing_ActualCosts { get; set; }
        public DbSet<Fico_Costing_DeptuseCost> Fico_Costing_DeptuseCosts { get; set; }


        //MM
        public DbSet<Mm_Material> Mm_Materials { get; set; }
        public DbSet<Yf_Billofmaterial> Yf_Billofmaterials { get; set; }
        public DbSet<Mm_PoResidue> Mm_PoResidues { get; set; }

        //PP

        public DbSet<Pp_Line> Pp_Lines { get; set; }
        public DbSet<Pp_Ec> Pp_Ecs { get; set; }
        public DbSet<Pp_EcSop> Pp_EcSops { get; set; }
        public DbSet<Pp_EcSub> Pp_EcSubs { get; set; }
        public DbSet<Pp_Liaison> Pp_Liaisons { get; set; }
        public DbSet<Pp_Tracking> Pp_Trackings { get; set; }
        public DbSet<Pp_TrackingCount> Pp_TrackingCounts { get; set; }
        public DbSet<Pp_TrackingOutput> Pp_TrackingOutputs { get; set; }
        public DbSet<Pp_Tracking_time> Pp_Tracking_times { get; set; }

        public DbSet<Pp_P1d_Output> Pp_P1d_Outputs { get; set; }
        public DbSet<Pp_P1d_OutputSub> Pp_P1d_OutputSubs { get; set; }
        public DbSet<Pp_P2d_Output> Pp_P2d_Outputs { get; set; }
        public DbSet<Pp_P2d_OutputSub> Pp_P2d_OutputSubs { get; set; }
        public DbSet<Pp_Kanban> Pp_Kanbans { get; set; }
        public DbSet<Pp_P1d_Defect> Pp_P1d_Defects { get; set; }
        public DbSet<Pp_P2d_Defect> Pp_P2d_Defects { get; set; }

        public DbSet<Pp_DefectTotal> Pp_DefectTotals { get; set; }
        public DbSet<Pp_Transport> Pp_Transports { get; set; }
        
        public DbSet<Pp_Efficiency> Pp_Efficiencys { get; set; }
        public DbSet<Pp_DefectCode> Pp_DefectCodes { get; set; }


        public DbSet<Pp_Duration> Pp_Durations { get; set; }
        public DbSet<Pp_Order> Pp_Orders { get; set; }
        public DbSet<Pp_SapEcn> Pp_SapEcns { get; set; }
        public DbSet<Pp_SapEcnSub> Pp_SapEcnSubs { get; set; }
        public DbSet<Pp_SapManhour> Pp_SapManhours { get; set; }
        public DbSet<Pp_SapMaterial> Pp_SapMaterials { get; set; }

        public DbSet<Pp_EcBalance> Pp_EcBalances { get; set; }
        public DbSet<Pp_EcCategory> Pp_EcCategorys { get; set; }
        public DbSet<Pp_Reason> Pp_Reasons { get; set; }
        public DbSet<Pp_Manhour> Pp_Manhours { get; set; }
        public DbSet<Pp_SapModelDest> Pp_SapModelDests { get; set; }
        public DbSet<Pp_SapOrder> Pp_SapOrders { get; set; }
        public DbSet<Pp_SapOrderSerial> Pp_SapOrderSerials { get; set; }
        //QM
        public DbSet<Qm_Outgoing> Qm_Outgoings { get; set; }
        public DbSet<Qm_Acceptancerate> Qm_Acceptancerates { get; set; }
        public DbSet<Qm_CheckAQL> Qm_CheckAQLs { get; set; }
        public DbSet<Qm_CheckType> Qm_CheckTypes { get; set; }
        public DbSet<Qm_Improvement> Qm_Improvements { get; set; }
        public DbSet<Qm_DocNumber> Qm_DocNumbers { get; set; }
        public DbSet<Qm_Operationdata> Qm_Operationdatas { get; set; }
        public DbSet<Qm_Reworkdata> Qm_Reworkdatas { get; set; }
        public DbSet<Qm_Unqualified> Qm_Unqualifieds { get; set; }
        public DbSet<Qm_Wastedata> Qm_Wastedatas { get; set; }
        public DbSet<Qm_Wagerate> Qm_Wagerates { get; set; }
        public DbSet<Qm_Complaint> Qm_Complaints { get; set; }

        //SD
        public DbSet<Sd_MrpData> Sd_MrpDatas { get; set; }
        public DbSet<Sd_Customer> Sd_Customers { get; set; }
        public DbSet<Sd_SoResidue> Sd_SoResidues { get; set; }
        public DbSet<Sd_PsiData> Sd_PsiDatas { get; set; }
        public DbSet<Sd_FcData> Sd_FcDatas { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //指定Decimal小数位数
            modelBuilder.Entity<Pp_Manhour>().Property(p => p.Prorate).HasPrecision(18, 5);
            modelBuilder.Entity<Mm_PoResidue>().Property(p => p.Bc_UnitPrice).HasPrecision(18, 5);
            modelBuilder.Entity<Fico_ExchangeRate>().Property(p => p.ER_Rate).HasPrecision(18, 5);
            //modelBuilder.Entity<Fico_Costing_Forecast>().Property(p => p.Bc_MovingAverage).HasPrecision(18, 5);
            modelBuilder.Entity<Fico_Costing_BomCost>().Property(p => p.Bc_MovingCost).HasPrecision(18, 5);
            modelBuilder.Entity<Sd_FcData>().Property(p => p.Bc_ExchangeRate).HasPrecision(18, 5);
            modelBuilder.Entity<Pp_Tracking>().Property(p => p.Pro_OperatingTime).HasPrecision(18, 5);
            modelBuilder.Entity<Pp_Tracking>().Property(p => p.Pro_StdDeviation).HasPrecision(18, 5);
            modelBuilder.Entity<Pp_Tracking_time>().Property(p => p.Pro_Tractime).HasPrecision(18, 5);

            modelBuilder.Entity<Adm_Role>()
                .HasMany(r => r.Users)
                .WithMany(u => u.Roles)
                .Map(x => x.ToTable("Adm_RoleUsers")
                    .MapLeftKey("RoleID")
                    .MapRightKey("UserID"));

            modelBuilder.Entity<Adm_Title>()
                .HasMany(t => t.Users)
                .WithMany(u => u.Titles)
                .Map(x => x.ToTable("Adm_TitleUsers")
                    .MapLeftKey("TitleID")
                    .MapRightKey("UserID"));

            modelBuilder.Entity<Adm_Dept>()
                .HasOptional(d => d.Parent)
                .WithMany(d => d.Children)
                .Map(x => x.MapKey("ParentID"));

            modelBuilder.Entity<Adm_Dept>()
                .HasMany(d => d.Users)
                .WithOptional(u => u.Dept)
                .Map(x => x.MapKey("DeptID"));

            modelBuilder.Entity<Adm_Online>()
                .HasRequired(o => o.User)
                .WithMany()
                .Map(x => x.MapKey("UserID"));

            modelBuilder.Entity<Adm_Menu>()
                .HasOptional(m => m.Parent)
                .WithMany(m => m.Children)
                .Map(x => x.MapKey("ParentID"));


            modelBuilder.Entity<Adm_Menu>()
                .HasOptional(m => m.ViewPower)
                .WithMany()
                .Map(x => x.MapKey("ViewPowerID"));

            modelBuilder.Entity<Adm_Role>()
                .HasMany(r => r.Powers)
                .WithMany(p => p.Roles)
                .Map(x => x.ToTable("Adm_RolePowers")
                    .MapLeftKey("RoleID")
                    .MapRightKey("PowerID"));

            //指定TEXT类型
            modelBuilder.Entity<Bpm_Flow_Form>().Property(p => p.Html).HasColumnType("text");
            modelBuilder.Entity<Bpm_Flow_Form>().Property(p => p.SubTableJson).HasColumnType("text");
            modelBuilder.Entity<Bpm_Flow_Form>().Property(p => p.EventsJson).HasColumnType("text");
        }

    }
}