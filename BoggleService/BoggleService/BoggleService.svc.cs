//Rizwan Mohammad and Adam Bennion, 2016

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
        static BoggleData GameObject;
        private int tempGameID = 0;
        Dictionary<Player, BoggleData> playersToGame = new Dictionary<Player, BoggleData>();
        Player players;

        ActivePlayer p1holder;
        ActivePlayer p2holder;
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
            String temp;

            if (token == null || players.p1_ID != token || players.p2_ID != token)
            {
                SetStatus(Forbidden);
                return null;
            }

            if (players.p1_ID.Equals(token))
            {
                temp = players.p1_ID;
                players.p1_ID = "";
                players.p1_Nickname = "";
                return temp;
            }
            else
            {
                temp = players.p2_ID;
                players.p1_ID = "";
                players.p2_Nickname = "";
                return temp;

            }
        }

        public string gameStatus()// string brief)
        {
            throw new NotImplementedException();
        }

        public string joinGame(string UserToken, int TimeLimit)
        {
            if (UserToken == null || TimeLimit < 5 || TimeLimit > 120)
            {
                SetStatus(Forbidden);
                return null;
            }
            if (players.p1_ID != UserToken || players.p1_ID != UserToken)
            {
                SetStatus(Forbidden);
                return null;
            }
            //Otherwise, if UserToken is already a player in the pending game, responds with status 409 (Conflict). 
            if (UserToken == players.p1_ID)
            {
                playersToGame.Add(players, GameObject); // if this updates after player 2 is added, good, otherwise move it down
                GameObject.GameState = "pending";
                SetStatus(Accepted);
                tempGameID++;
                return tempGameID.ToString();
            }
            if (UserToken == players.p2_ID)
            {
                GameObject.GameState = "active";
                SetStatus(Created);

                ActivePlayer player1 = new ActivePlayer();
                ActivePlayer player2 = new ActivePlayer();
                BoggleBoard board = new BoggleBoard();

                player1.UserToken = players.p1_ID;
                player2.UserToken = players.p2_ID;
                player1.GameID = tempGameID.ToString();
                player2.GameID = tempGameID.ToString();
                player1.GameState = GameObject.GameState;
                player2.GameState = GameObject.GameState;
                player1.board = board;
                player2.board = board;

                p1holder = player1;
                p2holder = player2;



                return tempGameID.ToString();
            }
            //if(UserToken == "affjda")//second player
            //{

            //   BoggleData data1 = new BoggleData();
            //    Player p2 = new Player();
            //    SetStatus(Accepted);
            //    return data1.GameID;
            //}

            string GameID = Guid.NewGuid().ToString();

            BoggleData data = new BoggleData();
            Player p1 = new Player();

            data.GameID = GameID;

            SetStatus(Accepted);
            return data.ToString();
        }


        public string playWord(string token, string word)
        {
            if (word == null || word.Trim().Equals(""))
            {
                SetStatus(Forbidden);
                return null;
            }

            if (p1holder.UserToken != token && p2holder.UserToken != token)
            {
                SetStatus(Forbidden);
                return null;
            }

            if (p1holder.GameID == null || p2holder.GameID == null)
            {
                SetStatus(Forbidden);
                return null;
            }

            if (p1holder.UserToken == token)
            {
                if (p1holder.board.CanBeFormed(word))
                {
                    p1holder.Word = word;
                    p1holder.List.Add(word);

                    switch (word.Length)
                    {
                        case 3:
                            p1holder.Score = 1;
                            p1holder.totalScore += 1;
                            break;

                        case 4:
                            p1holder.Score = 1;
                            p1holder.totalScore += 1;
                            break;

                        case 5:
                            p1holder.Score = 2;
                            p1holder.totalScore += 2;
                            break;
                        case 6:
                            p1holder.Score = 3;
                            p1holder.totalScore += 3;
                            break;
                        case 7:
                            p1holder.Score = 5;
                            p1holder.totalScore += 5;
                            break;


                    }

                    if (word.Length > 8)
                    {
                        p1holder.Score += 11;
                        p1holder.totalScore += 1;
                    }



                    return p1holder.Score.ToString();

                }

            }

            else
            {
                p2holder.Word = word;
                p2holder.List.Add(word);

                switch (word.Length)
                {
                    case 3:
                        p2holder.Score = 1;
                        p2holder.totalScore += 1;
                        break;

                    case 4:
                        p2holder.Score = 1;
                        p2holder.totalScore += 1;
                        break;

                    case 5:
                        p2holder.Score = 2;
                        p2holder.totalScore += 2;
                        break;
                    case 6:
                        p2holder.Score = 3;
                        p2holder.totalScore += 3;
                        break;
                    case 7:
                        p2holder.Score = 5;
                        p2holder.totalScore += 5;
                        break;


                }

                if (word.Length > 8)
                {
                    p2holder.Score += 11;
                    p2holder.totalScore += 1;
                }



                return p2holder.Score.ToString();
            }

            return null;
        }


        public String Register(string nickname)
        {
            if (nickname == null || nickname.Trim().Length == 0)
            {
                SetStatus(Forbidden);
                return null;
            }
            String userID = Guid.NewGuid().ToString();
            if (players == null)
            {
                players = new Player();
                players.p1_Nickname = nickname.Trim();
                players.p1_ID = userID;
            }

            //UserData data = new UserData();

            players.p2_Nickname = nickname.Trim();
            players.p2_ID = userID;
            SetStatus(Created);
            return players.p1_ID;
        }

        public string GetData(int value)
        {
            throw new NotImplementedException();
        }
    }
}




