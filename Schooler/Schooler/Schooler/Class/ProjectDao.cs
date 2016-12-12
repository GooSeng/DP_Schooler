using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Schooler.Class
{
    class ProjectDao : AssignmentDao
    {
        public ProjectDao(int idx) : base(idx)
        {
            thisUrl = "Project";
        }

        //-----------------------프로젝트 관련-----------------------//
        //---------------------------------------------------------//

        public List<Todo> GetTodo()
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var result = client.GetStringAsync("Todo/" + idx).Result;
                var ScheduleList = JsonConvert.DeserializeObject<List<Todo>>(result);
                return ScheduleList;
            }
        }

        public List<string> GetTeamUser()
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var result = client.GetStringAsync("Team/" + idx).Result;
                var ScheduleList = JsonConvert.DeserializeObject<List<string>>(result);
                return ScheduleList;
            }
        }

    }
}
