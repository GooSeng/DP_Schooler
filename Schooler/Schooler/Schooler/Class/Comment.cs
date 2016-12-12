using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schooler.Class
{
    class Comment
    {
        public int Idx { get; set; }

        // 프로젝트 인덱 
        public int? projectIdx { get; set; }

        // 작성유 
        public string uploadUserId { get; set; }

        // 코멘트내 
        public string comment { get; set; }
    }
}
