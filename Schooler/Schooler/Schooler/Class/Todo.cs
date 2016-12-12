using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schooler.Class
{
    class Todo
    {
		// 인덱 
        public int Idx { get; set; }

        // 프로젝트 인덱 
        public int ProjectIdx { get; set; }

        // 관리유 
        public string ManageUserId { get; set; }

        // 작성시 
        public DateTime UploadTime { get; set; }

        // 이 
        public string Name { get; set; }

        // 내 
        public string Content { get; set; }

        // 마감시 
        public DateTime? DeadLine { get; set; }

        // 필수여 
        public bool? IsEssential { get; set; }

        // 진행 
        public string Progress { get; set; }

		
		public Todo(int _pIdx)
		{
			Idx = -1;
			ProjectIdx = _pIdx;

			Progress = null;
			IsEssential = true;
			DeadLine = DateTime.Now;
			Content = null;
			Name = null;
			UploadTime = DateTime.Now;
			ManageUserId = null;
		}
    }
}
