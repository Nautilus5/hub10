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
using System.Timers;

namespace Boggle
{
    public class BoggleService : IBoggleService
    {
        static BoggleData GameObject = new BoggleData();
        private int tempGameID = 0;
        Dictionary<Player, BoggleData> playersToGame = new Dictionary<Player, BoggleData>();
        Player players;
        ActivePlayer p1holder;
        ActivePlayer p2holder;
        int timeLeft = -1;
        Timer timer = new Timer(1000);
        private void TimerEventProcessor(object sender, EventArgs e)
        {
            timeLeft--;

        }
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
        /// <summary>
        /// If GameID is invalid, responds with status 403 (Forbidden).
        /// Otherwise, returns information about the game named by GameID as illustrated below. 
        /// Note that the information returned depends on whether "Brief=yes" was included as a 
        /// parameter as well as on the state of the game. Responds with status code 200 (OK). 
        /// Note: The Board and Words are not case sensitive. 
        /// </summary>
        /// <returns></returns>

        public ExpandoObject gameStatus(string brief)
        {
            if(p1holder.GameID != tempGameID.ToString() || p2holder.GameID != tempGameID.ToString())
            {
                SetStatus(Forbidden);
                return null;
            }
            else if(GameObject.GameState == "pending")
            {
                SetStatus(OK);
                dynamic gameStat = new ExpandoObject();
                gameStat.GameState = "pending";
                return gameStat;
            }
            else if((GameObject.GameState == "active" || GameObject.GameState == "completed")&& brief == "yes")
            {
                SetStatus(OK);
                PlayerBriefReport Player1 = new PlayerBriefReport();
                Player1.Score = p1holder.totalScore;
                PlayerBriefReport Player2 = new PlayerBriefReport();
                Player2.Score = p2holder.totalScore;
                dynamic gameStat = new ExpandoObject();
                gameStat.GameState = GameObject.GameState;
                gameStat.TimeLeft = timeLeft;//need timeleft counter
                gameStat.Player1 = Player1;
                gameStat.Player2 = Player2;
                return gameStat;
            }
            else if (GameObject.GameState == "active" && brief != "yes")
            {
                SetStatus(OK);
                PlayerReport Player1 = new PlayerReport();
                Player1.Nickname = players.p1_Nickname;
                Player1.Score = p1holder.totalScore;
                PlayerReport Player2 = new PlayerReport();
                Player2.Nickname = players.p2_Nickname;
                Player2.Score = p2holder.totalScore;

                dynamic gameStat = new ExpandoObject();

                gameStat.GameState = GameObject.GameState;
                gameStat.Board = GameObject.Board;
                gameStat.TimeLimit = GameObject.TimeLimit;
                gameStat.TimeLeft = timeLeft;//need timeleft counter
                gameStat.Player1 = Player1;
                gameStat.Player2 = Player2;
                
                return gameStat;
            }
            else if (GameObject.GameState == "completed" && brief != "yes")
            {
                SetStatus(OK);
                GCompletePlayerReport Player1 = new GCompletePlayerReport();
                Player1.Nickname = players.p1_Nickname;
                Player1.Score = p1holder.totalScore;
                Player1.WordsPlayed = p1holder.List;//need words played list from player
                GCompletePlayerReport Player2 = new GCompletePlayerReport();
                Player2.Nickname = players.p2_Nickname;
                Player2.Score = p2holder.totalScore;
                Player2.WordsPlayed = p2holder.List;//need words played list from player
                dynamic gameStat = new ExpandoObject();

                gameStat.GameState = GameObject.GameState;
                gameStat.Board = GameObject.Board;
                gameStat.TimeLimit = GameObject.TimeLimit;
                gameStat.TimeLeft = timeLeft;//need timeleft counter
                gameStat.Player1 = Player1;
                gameStat.Player2 = Player2;

                return gameStat;
            }
            SetStatus(Forbidden);
            return null;

        }

        public string joinGame(string UserToken, int time)
        {
            if (UserToken == null || time < 5 || time > 120)
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
                GameObject.TimeLimit = time;
            }
            if (UserToken == players.p2_ID)
            {
                GameObject.TimeLimit = (GameObject.TimeLimit+time) /2;
                GameObject.GameState = "active";
                SetStatus(Created);

                ActivePlayer player1 = new ActivePlayer();
                ActivePlayer player2 = new ActivePlayer();
                BoggleBoard board = new BoggleBoard();
                GameObject.Board = board.ToString();
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

                timeLeft = GameObject.TimeLimit;

                return tempGameID.ToString();
            }

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




