using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schooler.Class
{
    class Project : Assignment
    {
        private bool isTeam;
        private List<string> teamList;
        private List<Todo> todoList;

        public Project(int idx, string name, DateTime deadline, bool isTeam)
            :base(idx,name,deadline)
        {
            this.isTeam = isTeam;
        }

        public bool settingProject(string name, DateTime date)
        {
            return true;
        }
        public bool addMember(string id)
        {
            return true;
        }
    }
}
