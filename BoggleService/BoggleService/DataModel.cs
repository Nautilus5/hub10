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

namespace DataModel
{
    [DataContract]
    public class UserData
    {
        [DataMember]
        public string UserToken { get; set; }
    }
    [DataContract]
    public class BoggleData
    {
        [DataMember(EmitDefaultValue = false)]
        public int TimeLimit { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string Word { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public int GameState { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public string Board { get; set; }

    }

    [DataContract]
    public class Player
    {
        public String Nickname { get; set; }

        public int score { get; set; }

        //Public List wordsPlayed { get; set; }
    }
}