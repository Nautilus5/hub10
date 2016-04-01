//Rizwan Mohammad and Adam Bennion, 2016

using DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Boggle
{
    [ServiceContract]
    public interface IBoggleService
    {
        [WebGet(UriTemplate = "/api")]
        Stream API();
        [WebInvoke(Method = "POST", UriTemplate = "/users")]
        String Register(string nickname);

        // [WebInvoke(Method = "POST", UriTemplate = "/users")]
        // UserData timeleft(int time);
        [WebInvoke(Method = "POST", UriTemplate = "/games")]
        String joinGame(String Token, int time);
        [WebInvoke(Method = "PUT", UriTemplate = "/games")]
        String Cancel(String token);

        [WebInvoke(Method = "PUT", UriTemplate = "/games:GameID")]
        String playWord(String token, string word);


        [WebInvoke(Method = "GET", UriTemplate = "/games")]
        String gameStatus();// String brief);



        string GetData(int value);

        
        //[OperationContract]
        //CompositeType GetDataUsingDataContract(CompositeType composite);


        // TODO: Add your service operations here
    }

    /*
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
    */
}

