﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeanFine
{
    public class Adm_Dept : Adm_ICustomTree, IKeyID, ICloneable
    {
        [Key]
        public int ID { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required]
        public int SortIndex { get; set; }

        [StringLength(500)]
        public string Remark { get; set; }

        public virtual Adm_Dept Parent { get; set; }
        public virtual ICollection<Adm_Dept> Children { get; set; }

        public virtual ICollection<Adm_User> Users { get; set; }

        /// <summary>
        /// 菜单在树形结构中的层级（从0开始）
        /// </summary>
        [NotMapped]
        public int TreeLevel { get; set; }

        /// <summary>
        /// 是否可用（默认true）,在模拟树的下拉列表中使用
        /// </summary>
        [NotMapped]
        public bool Enabled { get; set; }

        /// <summary>
        /// 是否叶子节点（默认true）
        /// </summary>
        [NotMapped]
        public bool IsTreeLeaf { get; set; }

        public object Clone()
        {
            Adm_Dept dept = new Adm_Dept
            {
                ID = ID,
                Name = Name,
                Remark = Remark,
                SortIndex = SortIndex,
                TreeLevel = TreeLevel,
                Enabled = Enabled,
                IsTreeLeaf = IsTreeLeaf
            };
            return dept;
        }
    }
}