using System.Collections.Generic;
using System.Linq;

namespace LeanFine
{
    public class DeptHelper
    {
        private static List<Adm_Dept> _depts;

        public static List<Adm_Dept> Adm_Depts
        {
            get
            {
                if (_depts == null)
                {
                    InitDepts();
                }
                return _depts;
            }
        }

        public static void Reload()
        {
            _depts = null;
        }

        private static void InitDepts()
        {
            _depts = new List<Adm_Dept>();

            List<Adm_Dept> dbDepts = PageBase.DB.Adm_Depts.OrderBy(d => d.SortIndex).ToList();

            ResolveDeptCollection(dbDepts, null, 0);
        }

        private static int ResolveDeptCollection(List<Adm_Dept> dbDepts, Adm_Dept parentDept, int level)
        {
            int count = 0;
            foreach (var dept in dbDepts.Where(d => d.Parent == parentDept))
            {
                count++;

                _depts.Add(dept);
                dept.TreeLevel = level;
                dept.IsTreeLeaf = true;
                dept.Enabled = true;

                level++;
                // 如果这个节点下没有子节点，则这是个终结节点
                int childCount = ResolveDeptCollection(dbDepts, dept, level);
                if (childCount != 0)
                {
                    dept.IsTreeLeaf = false;
                }
                level--;
            }

            return count;
        }
    }
}