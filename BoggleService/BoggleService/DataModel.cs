//Rizwan Mohammad and Adam Bennion, 2016

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web.Services.Description;
using System.Runtime.Serialization;
using Boggle;

namespace DataModel
{
    [DataContract]
    public class ActivePlayer
    {
        [DataMember]
        public string UserToken { get; set; }

        [DataMember]
        public string Word { get; set; }

        [DataMember]
        public string GameID { get; set; }

        [DataMember]
        public int Score { get; set; }

        [DataMember]
        public int totalScore { get; set; }

        [DataMember]
        public List<String> List { get; set; }

        public String GameState { get; set; }

        public BoggleBoard board { get; set; }

    }
    [DataContract]
    public class BoggleData
    {
        [DataMember(EmitDefaultValue = false)]
        public string GameID { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int TimeLimit { get; set; }

        //[DataMember(EmitDefaultValue = false)]
        //public int timeleft { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Word { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string GameState { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string Board { get; set; }

    }

    [DataContract]
    public class Player
    {
        [DataMember]
        public String p1_Nickname { get; set; }
        [DataMember]
        public String p1_ID { get; set; }
        [DataMember]
        public String p2_Nickname { get; set; }
        [DataMember]
        public String p2_ID { get; set; }

        //Public List wordsPlayed { get; set; }
    }
}