using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schooler.Class
{
    class Todo
    {
		enum PROGRESSTYPE { BEFORESTARTING, PROCEEDING, END }

		public string name { get; set; }
        private DateTime uploadTime;
		private string uploadUser;
		private bool isEssential;
		private PROGRESSTYPE progress;
		private DateTime deadline;
		private string managerUser; // 안됌?
		private int idx;

    }
}
