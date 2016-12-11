using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Schooler.Class
{
    class UserDao : IDao  
    {
        HttpClient client;
        string baseUrl = "https://schoolerAPIServer.azurewebsites.net/api/";
        static string LoginedUser = null;

        //-----------------------유저 로그인, 로그아웃, 회원가입, 탈퇴 관련-----------------------//
        //----------------------------------------------------------------------------------------//
        //로그인 함수
        public bool SignIn(string id, string password)
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
				var tmp = client.GetStringAsync("User/" + id + "/" + password).Result;
				var result = Convert.ToBoolean(tmp);
//				var result = Convert.ToBoolean(client.GetStringAsync("User/" + id + "/" + password).Result);

                if (result)
                    LoginedUser = id;

                return result;
                //  var contacts = JsonConvert.DeserializeObject<contact[]>(json);
            }
        }

        //로그아웃 함수
        public void SignOut()
        {
            LoginedUser = null;
        }

        public void SignUp(string id, string password)
        {
            UserModel SignUpUser = new UserModel { Id = id, password = password };
            using (client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(SignUpUser);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
  
                client.BaseAddress = new Uri(baseUrl);
                var r = client.PostAsync("User/", content).Result;

                // Console.Out.Write(r); 

            }

        }

        //로그인 유저 반환 함수
        public string GetLoginedUser()
        {
            return LoginedUser;
        }

        //로그인 함수
        public bool CheckUser(string id)
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var result = Convert.ToBoolean(client.GetStringAsync("User/" + id).Result);

                if (result)
                    return true;

                return false;
                //  var contacts = JsonConvert.DeserializeObject<contact[]>(json);
            }
        }

        //-----------------------스캐줄 관련-----------------------//
        //---------------------------------------------------------//
        public List<Schedule> GetSchedule(string id)
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var result = client.GetStringAsync("Schedule/" + id).Result;
                var ScheduleList = JsonConvert.DeserializeObject<List<Schedule>>(result);
                return ScheduleList;
            }
        }

        public List<Schedule> GetSchedule(string id,int y, int m, int d)
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                string fullStr = "Schedule/" + id + "/" + y + "/" + m + "/" + d;
                var result = client.GetStringAsync(fullStr).Result;
                var ScheduleList = JsonConvert.DeserializeObject<List<Schedule>>(result);
                return ScheduleList;
            }
        }

        public List<Schedule> GetSchedule(int y, int m, int d)
        {
            return GetSchedule(LoginedUser,y,m,d);
        }


        public List<Schedule> GetSchedule()
        {
            return GetSchedule(LoginedUser);
        }


        public void AddSchedule(Schedule sc,string id)
        {
            ScheduleModel sm = new ScheduleModel { Idx = sc.Idx, day = sc.day, name = sc.name, place = sc.place, userId = id};
            using (client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(sm);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri(baseUrl);
                var r = client.PostAsync("Schedule/", content).Result;
            }
        }


        public void AddSchedule(Schedule sc)
        {
            AddSchedule(sc, LoginedUser);
        }

        //-----------------------프로젝트, 과제 관련-----------------------//
        //-----------------------------------------------------------------//
        public List<Project> GetProject(string id)// 반환형은 나중에 Project로 변환해서 바꿔야함
        {
            List<int> RelationList = GetRelation(id);
            List<ProjectModel> ProjectModelList = new List<ProjectModel>();
            foreach (var item in RelationList)
            {
                using (client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    var result = client.GetStringAsync("Project/" + item).Result;
                    var projectModel = JsonConvert.DeserializeObject<ProjectModel>(result);
                    if (projectModel != null)
                        ProjectModelList.Add(projectModel);
                }
            }

            List<Project> ProjectList = new List<Project>();
            foreach (var item in ProjectModelList)
            {
                ProjectList.Add(new Project(item.Idx, item.name, (DateTime)item.DeadLine, (bool)item.isTeam));
            }

            return ProjectList;

        }

        public List<Project> GetProject()// 반환형은 나중에 Project로 변환해서 바꿔야함
        {
            return GetProject(LoginedUser);
        }

        public List<Assignment> GetAssignment(string id)// 반환형은 나중에 Assignment로 변환해서 바꿔야함
        {
            List<int> RelationList = GetRelation(id);
            List<AssignmentModel> AssignmentModelList = new List<AssignmentModel>();
            foreach (var item in RelationList)
            {
                using (client = new HttpClient())
                {
                    client.BaseAddress = new Uri(baseUrl);
                    var result = client.GetStringAsync("Assignment/" + item).Result;
                    var assignmentModel = JsonConvert.DeserializeObject<AssignmentModel>(result);
                    if (assignmentModel != null)
                        AssignmentModelList.Add(assignmentModel);
                }
            }

            List<Assignment> AssignmentList = new List<Assignment>();
            foreach (var item in AssignmentModelList)
            {
                AssignmentList.Add(new Assignment(item.Idx, item.name, (DateTime)item.DeadLine));
            }

            return AssignmentList;

        }

        public List<Assignment> GetAssignment()// 반환형은 나중에 Project로 변환해서 바꿔야함
        {
            return GetAssignment(LoginedUser);
        }

        public void AddAssignment(Assignment sc, string id)
        {
            StreamDataForProject sm = new StreamDataForProject { userName = id, name = sc.getName(), DeadLine = sc.getDeadline() };
            using (client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(sm);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri(baseUrl);
                var r = client.PostAsync("Assignment/", content).Result;
            }
        }

        public void AddAssignment(Assignment sc)
        {
            AddAssignment(sc, LoginedUser);
        }

        public void AddProject(Project sc, string id)
        {
            StreamDataForProject sm = new StreamDataForProject { userName = id, name = sc.getName(), DeadLine = sc.getDeadline(), isTeam = sc.getIsTeam() };
            using (client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(sm);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri(baseUrl);
                var r = client.PostAsync("Project/", content).Result;
            }
        }
        public void AddProject(Project sc)
        {
            AddProject(sc, LoginedUser);
        }

        public void UpdateAssignment(Assignment sc)
        {
            StreamDataForProject sm = new StreamDataForProject { name = sc.getName(), DeadLine = sc.getDeadline() };
            using (client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(sm);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri(baseUrl);
                var r = client.PutAsync("Assignment/"+sc.getIdx(), content).Result;
            }
        }


        private List<int> GetRelation(string id)
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var result = client.GetStringAsync("RelationUserProject/" + id).Result;
                List<int> RelationUserProjectList = JsonConvert.DeserializeObject<List<int>>(result);
                return RelationUserProjectList;
            }
        }




    }
}
