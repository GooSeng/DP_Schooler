using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schooler.Class
{
    class Assignment
    {
        protected string name;
        protected DateTime deadline;
        protected List<File> fileList;
        protected List<Comment> commentList;
        protected int idx;
        protected IDao dao;

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
    }
}
