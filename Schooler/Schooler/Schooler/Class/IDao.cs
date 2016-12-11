using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schooler.Class
{
    abstract class IDao
    {
        //-----------------------데이터 모델링을 위한 클래스-----------------------//
        protected class UserModel
        {
            public string Id { get; set; }

            public string password { get; set; }

            public bool deleted { get; set; }

        }

        protected class ProjectModel
        {
            public int Idx { get; set; }

            public string name { get; set; }

            public bool? isTeam { get; set; }

            public DateTime? DeadLine { get; set; }
        }

        protected class AssignmentModel
        {
            public int Idx { get; set; }

            public string name { get; set; }

            public DateTime? DeadLine { get; set; }
        }

        protected class ScheduleModel
        {
            public int Idx { get; set; }

            public string userId { get; set; }

            public string name { get; set; }

            public DateTime? day { get; set; }

            public string place { get; set; }
            
        }


        //-----------------------그대로 사용가능한 클래스-----------------------//
      

        

    }
}
