﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schooler.Class
{
    class Project : Assignment
    {
        public bool isTeam { get; set; }
        public List<string> teamList { get; set; }
        public List<Todo> todoList { get; set; }

		public Project()
			: base()
		{
		}

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

        public bool getIsTeam()
        {
            return isTeam;
        }
    }
}
