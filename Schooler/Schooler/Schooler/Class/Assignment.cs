using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schooler.Class
{
    class Assignment
    {
        private string name;
        private DateTime deadline;
        private List<File> fileList;
        private List<Comment> commentList;
        protected int idx;
        //       protected IDao Dao;

        public bool settingAssignment(string name, DateTime date)
        {
            return true;
        }
    }
}
