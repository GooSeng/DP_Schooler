using Newtonsoft.Json;
using PCLStorage;
using System;
using System.Collections.Generic;
using System.IO;
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
        protected string thisUrl;

        public AssignmentDao(int idx)
        {
            this.idx = idx;
            thisUrl = "Assignment";
        }

        //-----------------------파일 관련-----------------------//
        //---------------------------------------------------------//

        public async Task<bool> UploadFile(File file)
        {
            FileWithByte ByteFile = new FileWithByte { projectIdx = file.projectIdx, name = file.name, uploadUserId = file.uploadUserId };
            ByteFile.data = await GetFileByte(file.url);

            if(ByteFile.data.Length > 1024)
            {
                return false;
            }
           
            using (client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(ByteFile);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri(baseUrl);
                var r = client.PostAsync("File/", content).Result;
            }
            return true;

        }

        private async Task<byte[]> GetFileByte(string url)
        {
            var file = await PCLStorage.FileSystem.Current.GetFileFromPathAsync(url);
            using (Stream fileStream = await file.OpenAsync(FileAccess.Read))
            {
                using (var memoryStream = new MemoryStream())
                {
                    fileStream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }

        }

        public void DownloadFile(int idx)
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var result = client.GetStringAsync("File/" + idx).Result;
                SaveFileByte(result);
                
            }
        }

        private async void SaveFileByte(string bytes)
        {
            var file = await PCLStorage.FileSystem.Current.LocalStorage.CreateFileAsync("file",CreationCollisionOption.GenerateUniqueName);
            await file.WriteAllTextAsync(bytes);
        }

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

        public void UploadComment(Comment comment)
        {
             using (client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(comment);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri(baseUrl);
                var r = client.PostAsync("Comment/", content).Result;
            }
        }


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

        public void DeleteComment(int idx)
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var r = client.DeleteAsync("Comment/" + idx).Result;
            }
        }

        //public bool DeleteComment(int commentIdx)
        //{
        //    // TODO implement here
        //    return False;
        //}


        //-----------------------과제 관련-----------------------//
        //---------------------------------------------------------//


        public void Delete()
        {
            using (client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var result = client.DeleteAsync(thisUrl + "/" + idx).Result;
            }
        }

        public void Update(Assignment sc)
        {
            StreamDataForProject sm = new StreamDataForProject { name = sc.getName(), DeadLine = sc.getDeadline() };
            using (client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(sm);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                client.BaseAddress = new Uri(baseUrl);
                var r = client.PutAsync(thisUrl + "/" + sc.getIdx(), content).Result;
            }
        }


    }
}
