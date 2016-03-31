using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.ServiceModel.Web;
using System.Text;
using static System.Net.HttpStatusCode;
using DataModel;

namespace Boggle
{
    public class BoggleService : IBoggleService
    {
        /// <summary>
        /// The most recent call to SetStatus determines the response code used when
        /// an http response is sent.
        /// </summary>
        /// <param name="status"></param>
        private static void SetStatus(HttpStatusCode status)
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = status;
        }

        /// <summary>
        /// Returns a Stream version of index.html.
        /// </summary>
        /// <returns></returns>
        public Stream API()
        {
            SetStatus(OK);
            WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
            return File.OpenRead(AppDomain.CurrentDomain.BaseDirectory + "index.html");
        }

        public string Cancel(string token)
        {
            throw new NotImplementedException();
        }

        public string gameStatus(string brief)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Demo.  You can delete this.
        /// </summary>
        public int GetFirst(IList<int> list)
        {
            SetStatus(OK);
            return list[0];
        }

        public string joinGame(string Token, string time)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Demo.  You can delete this.
        /// </summary>
        /// <returns></returns>
        public IList<int> Numbers(string n)
        {
            int index;
            if (!Int32.TryParse(n, out index) || index < 0)
            {
                SetStatus(Forbidden);
                return null;
            }
            else
            {
                List<int> list = new List<int>();
                for (int i = 0; i < index; i++)
                {
                    list.Add(i);
                }
                SetStatus(OK);
                return list;
            }
        }

        public string playWord(string token, string word)
        {
            throw new NotImplementedException();
        }

        public UserData Register(string nickname)
        {
            if (nickname == null || nickname == "")
            {
                SetStatus(Forbidden);
                return null;
            }

            String userID = Guid.NewGuid().ToString();

            UserData data = new UserData();
            Player p1 = new Player();

            p1.Nickname = nickname;

            data.UserToken = userID;

            SetStatus(Created);
            return data;



        }

        //String IBoggleService.Register(string nickname)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
