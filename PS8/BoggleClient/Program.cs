using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using Newtonsoft.Json;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BoggleClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Interaction inter = new Interaction();
            Application.Run(inter.game);

            if (inter.game.ready == true)
            {
                Interaction rest = new Interaction();
            }
        }
    }
    /// <summary>
    /// Handles interaction with server.
    /// </summary>
    public class Interaction
    {
        public Form1 game = new Form1();

        /// <summary>
        /// Creates an HttpClient for communicating with boggle on azurewebsite.
        /// </summary>
        public static HttpClient CreateClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://bogglecs3500s16.azurewebsites.net");

            // There is more client configuration to do, depending on the request.
            return client;
        }

        /// <summary>
        /// Prints out the names of the organizations to which the user belongs.
        /// Illustrates a simple GET request.
        /// </summary>
        public static void Get()
        {
            // Create the HttpClient.  It will be automatically closed when
            // this using block is exited.
            using (HttpClient client = CreateClient())
            {
                HttpResponseMessage response = client.GetAsync("http://bogglecs3500s16.azurewebsites.net/user").Result;
            }
        }
 
        /// <summary>
        /// For a POST, the parameters go into the body of the request instead of in
        /// the URL.
        /// </summary>
        public static void Post()
        {
            using (HttpClient client = CreateClient())
            {
                // An ExpandoObject is one to which in which we can set arbitrary properties.
                // To create a new public repository, we must send a request parameter which
                // is a JSON object with various properties of the new repo expressed as
                // properties.
                dynamic data = new ExpandoObject();
                data.name = "TestRepo";
                data.description = "A test repository for CS 3500";
                data.has_issues = false;

                // To send a POST request, we must include the serialized parameter object
                // in the body of the request.
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync("/BoggleService.svc/users", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    // The deserialized response value is an object that describes the new repository.
                    String result = response.Content.ReadAsStringAsync().Result;
                    dynamic newRepo = JsonConvert.DeserializeObject(result);
                    Console.WriteLine("New repository: ");
                    Console.WriteLine(newRepo);
                }
                else
                {
                    Console.WriteLine("Error creating repo: " + response.StatusCode);
                    Console.WriteLine(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Commits a file to a repository.  Demonstrates the use of a PUT request.
        /// For a PUT, the parameters go into the body of the request.
        /// </summary>
        public static void Put()
        {
            using (HttpClient client = CreateClient())
            {
                dynamic data = new ExpandoObject();
                data.message = "Committing via API";
                data.content = Convert.ToBase64String(Encoding.UTF8.GetBytes("This is a test"));

                String url = String.Format("");
                StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(url, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("File created");
                }
                else
                {
                    Console.WriteLine("Error putting file: " + response.StatusCode);
                    Console.WriteLine(response.ReasonPhrase);
                }
            }
        }


        /// <summary>
        /// Deletes an existing public repository.  Here, we demonstrate the use of
        /// a DELETE request.  
        /// </summary>
        public static async void Delete()
        {
            using (HttpClient client = CreateClient())
            {
                // Here the repo name and user name appear in the path.  If there were
                // any parameters, they would go in the body as with POST and PUT.
                String url = String.Format("");
                HttpResponseMessage response = await client.DeleteAsync(url);

                // No response object is sent back.
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Deletion succeeded");
                }
                else
                {
                    Console.WriteLine("Error deleting repo: " + response.StatusCode);
                    Console.WriteLine(response.ReasonPhrase);
                }
            }
        }
    }
}
