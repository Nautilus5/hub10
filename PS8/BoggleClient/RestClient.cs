using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Dynamic;

namespace BoggleClient
{

    public class player
    {
        public String GameID { set; get; }
        public String Nickname { set; get; }
        public String UserToken { set; get; }
        public int TimeLimit { set; get; }
        public String Score { set; get; }
    }

    public class Client
    {
        public player PlayerLocal;
        public String GameState;
        public String Board;
        public player AnotherPlayer;
        public int TimeLeft = -1;
        public List<Tuple<string, int>> wordlist1 = new List<Tuple<string, int>>();
        public List<Tuple<dynamic, dynamic>> wordlist2 = new List<Tuple<dynamic, dynamic>>();
        private Tuple<string, int> currentWord;
        private Tuple<dynamic, dynamic> currentWord1;
        private string address;


        public Client(String address)
        {
            this.address = address;
        }
        public HttpClient CreateClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(address);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }


        public void JoinGame(int TimeLimit)
        {
            PlayerLocal.TimeLimit = TimeLimit;
            using (HttpClient client = CreateClient())
            {

                dynamic data = new ExpandoObject();
                data.UserToken = PlayerLocal.UserToken;
                data.TimeLimit = PlayerLocal.TimeLimit;
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("/BoggleService.svc/games", content).Result;
                if (response.IsSuccessStatusCode)
                {

                    String result = response.Content.ReadAsStringAsync().Result;
                    dynamic newRepo = JsonConvert.DeserializeObject(result);
                    JObject jObject = JObject.Parse(result);
                    JToken jUser = jObject["GameID"];
                    PlayerLocal.GameID = jUser.ToString();
                }
                else
                {
                    Console.WriteLine("Error creating repo: " + response.StatusCode);
                    Console.WriteLine(response.ReasonPhrase);
                }
            }
        }

        public void GetGameStatus()
        {

            using (HttpClient client = CreateClient())
            {
                HttpResponseMessage response = client.GetAsync("/BoggleService.svc/games/" + PlayerLocal.GameID).Result;


                if (response.IsSuccessStatusCode)
                {

                    String result = response.Content.ReadAsStringAsync().Result;
                    dynamic orgs = JsonConvert.DeserializeObject(result);
                    this.GameState = orgs.GameState;
                    if (this.GameState == "active")
                    {
                        this.AnotherPlayer = new player();
                        this.Board = orgs.Board;
                        this.TimeLeft = orgs.TimeLeft;
                        String getInfo = orgs.Player1.Nickname;
                        if (PlayerLocal.Nickname == getInfo)
                        {

                            PlayerLocal.Score = orgs.Player1.Score;
                            AnotherPlayer.Nickname = orgs.Player2.Nickname;
                            AnotherPlayer.Score = orgs.Player2.Score;
                        }
                        else
                        {
                            PlayerLocal.Score = orgs.Player2.Score;
                            AnotherPlayer.Nickname = orgs.Player1.Nickname;
                            AnotherPlayer.Score = orgs.Player1.Score;
                        }

                    }
                    else if (this.GameState == "pending")
                    {

                    }
                    else if (this.GameState == "completed")
                    {
                        String TimeLeft = orgs.TimeLeft;
                        this.AnotherPlayer = new player();
                        this.Board = orgs.Board;
                        this.TimeLeft = orgs.TimeLeft;
                        String getInfo = orgs.Player1.Nickname;
                        if (PlayerLocal.Nickname == getInfo)
                        {

                            PlayerLocal.Score = orgs.Player1.Score;
                            AnotherPlayer.Nickname = orgs.Player2.Nickname;
                            AnotherPlayer.Score = orgs.Player2.Score;
                            dynamic WordPlay = orgs.Player2.WordsPlayed;
                            dynamic[] WordPlayArray = WordPlay.ToObject<dynamic[]>();
                            foreach (dynamic cell in WordPlayArray)
                            {
                                currentWord1 = new Tuple<dynamic, dynamic>(cell.Word, cell.Score);
                                wordlist2.Add(currentWord1);
                            }
                        }
                        else
                        {
                            PlayerLocal.Score = orgs.Player2.Score;
                            AnotherPlayer.Nickname = orgs.Player1.Nickname;
                            AnotherPlayer.Score = orgs.Player1.Score;
                            dynamic WordPlay = orgs.Player1.WordsPlayed;
                            dynamic[] WordPlayArray = WordPlay.ToObject<dynamic[]>();
                            foreach (dynamic cell in WordPlayArray)
                            {
                                currentWord1 = new Tuple<dynamic, dynamic>(cell.Word, cell.Score);
                                wordlist2.Add(currentWord1);
                            }
                        }


                    }
                }
                else
                {
                    Console.WriteLine("Error getting organizations: " + response.StatusCode);
                    Console.WriteLine(response.ReasonPhrase);
                }



            }
        }
        public void CreateUser(String name)
        {
            using (HttpClient client = CreateClient())
            {

                PlayerLocal = new player();
                PlayerLocal.Nickname = name;
                StringContent content = new StringContent(JsonConvert.SerializeObject(PlayerLocal), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("/BoggleService.svc/users", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    String result = response.Content.ReadAsStringAsync().Result;
                    dynamic newRepo = JsonConvert.DeserializeObject(result);
                    JObject jObject = JObject.Parse(result);
                    JToken jUser = jObject["UserToken"];
                    PlayerLocal.UserToken = jUser.ToString();

                }
                else
                {
                    Console.WriteLine("Error creating repo: " + response.StatusCode);
                    Console.WriteLine(response.ReasonPhrase);

                }
            }
        }

        public void PutDemo(String word)
        {
            using (HttpClient client = CreateClient())
            {
                dynamic data = new ExpandoObject();
                data.UserToken = PlayerLocal.UserToken;
                data.Word = word;
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync("/BoggleService.svc/games/" + PlayerLocal.GameID, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    // The deserialized response value is an object that describes the new repository.
                    String result = response.Content.ReadAsStringAsync().Result;
                    dynamic newRepo = JsonConvert.DeserializeObject(result);
                    JObject jObject = JObject.Parse(result);
                    JToken jScore = jObject["Score"];
                    int score;
                    Int32.TryParse(jScore.ToString(), out score);
                    currentWord = new Tuple<string, int>(word, score);
                    wordlist1.Add(currentWord);
                }
                else
                {
                    Console.WriteLine("Error creating repo: " + response.StatusCode);
                    Console.WriteLine(response.ReasonPhrase);

                }
            }
        }

        public void CancelJoin()
        {
            using (HttpClient client = CreateClient())
            {
                dynamic data = new ExpandoObject();
                data.UserToken = PlayerLocal.UserToken;
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync("/BoggleService.svc/games", content).Result;
            }
        }

        public void ClearClient()
        {
            PlayerLocal.GameID = "";
            PlayerLocal.Score = "";
            GameState = "";
            Board = "";
            AnotherPlayer = new player();
            wordlist1 = new List<Tuple<string, int>>();
            wordlist2 = new List<Tuple<dynamic, dynamic>>();
        }
    }
}
