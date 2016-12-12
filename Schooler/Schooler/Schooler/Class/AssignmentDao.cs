using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Schooler.Class
{
    class AssignmentDao : IDao
    {
        protected int idx;
        protected HttpClient client;
        protected string baseUrl = "https://schoolerAPIServer.azurewebsites.net/api/";

        public AssignmentDao(int idx)
        {
            this.idx = idx;
        }

        //-----------------------파일 관련-----------------------//
        //---------------------------------------------------------//

        //public bool UploadFile(File file)
        //{
        //    // TODO implement here
        //    return False;
        //}


        public List<File> GetFileList()
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var result = client.GetStringAsync("RelationProjectAndFile/" + idx).Result;
                var ScheduleList = JsonConvert.DeserializeObject<List<File>>(result);
                return ScheduleList;
            }
        }

        //-----------------------코멘트 관련-----------------------//
        //---------------------------------------------------------//

        //public bool UploadComment(Comment comment)
        //{
        //    // TODO implement here
        //    return False;
        //}


        public List<Comment> GetCommentList()
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var result = client.GetStringAsync("Comment/" + idx).Result;
                var ScheduleList = JsonConvert.DeserializeObject<List<Comment>>(result);
                return ScheduleList;
            }
        }

        //public bool DeleteComment(int commentIdx)
        //{
        //    // TODO implement here
        //    return False;
        //}


        //-----------------------과제 관련-----------------------//
        //---------------------------------------------------------//
        public void DeleteAssigment()
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var result = client.DeleteAsync("Assignment/" + idx).Result;
            }
        }

        public void UpdateAssignment(Assignment sc)
        {
            StreamDataForProject sm = new StreamDataForProject { name = sc.getName(), DeadLine = sc.getDeadline() };
            using (client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(sm);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri(baseUrl);
                var r = client.PutAsync("Assignment/" + sc.getIdx(), content).Result;
            }
        }


    }
}
