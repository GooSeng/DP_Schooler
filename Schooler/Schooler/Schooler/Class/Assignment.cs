using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schooler.Class
{
    class Assignment
    {
        public string name { get; set; }
        protected DateTime deadline;
        protected List<File> fileList;
        protected List<Comment> commentList;
        protected int idx;
        protected IDao dao;

		public Assignment()
		{
			idx = -1;
			name = null;
			deadline = DateTime.Now;
		}

        public Assignment(int idx, string name, DateTime deadline)
        {
            this.idx = idx;
            this.name = name;
            this.deadline = deadline;

        }

        protected virtual void Init()
        {
            fileList = Dao().GetFileList();
            commentList = Dao().GetCommentList();
        }

        public bool settingAssignment(string name, DateTime date)
        {
            return true;
        }

        protected virtual AssignmentDao Dao()
        {
            dao = new AssignmentDao(idx);
            return (dao as AssignmentDao);
        }

        public string getName()
        {
            return name;
        }

        public DateTime getDeadline()
        {
            return deadline;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public void setDeadline(DateTime dead)
        {
            deadline = dead;
        }

        public int getIdx()
        {
            return idx;
        }
  
    }
}
