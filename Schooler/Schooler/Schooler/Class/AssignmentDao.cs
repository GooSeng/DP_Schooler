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
            // TODO implement here
            return null;
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
            // TODO implement here
            return null;
        }

        //public bool DeleteComment(int commentIdx)
        //{
        //    // TODO implement here
        //    return False;
        //}


        //-----------------------과제 관련-----------------------//
        //---------------------------------------------------------//
        //public bool DeleteAssigment(int objIdx)
        //{
        //    // TODO implement here
        //    return false;
        //}


    }
}
