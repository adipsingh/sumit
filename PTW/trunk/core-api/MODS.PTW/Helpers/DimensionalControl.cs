using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using MODS.PTW.Dtos;

namespace MODS.PTW.Helpers
{
    
    class OAuth2Token
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
        public string Appcp { get; set; }
        public string Apicp { get; set; }
        public string Subdomain { get; set; }
        public int ExpiresIn { get; set; }

        public OAuth2Token(JObject json)
        {
            if (json != null)
            {
                AccessToken = (string)json["access_token"];
                RefreshToken = (string)json["refresh_token"];
                TokenType = (string)json["token_type"];
                Appcp = (string)json["appcp"];
                Apicp = (string)json["apicp"];
                Subdomain = (string)json["subdomain"];
                ExpiresIn = (int)json["expires_in"];
            }
            else
            {
                AccessToken = "";
                RefreshToken = "";
                TokenType = "";
                Appcp = "";
                Apicp = "";
                Subdomain = "";
                ExpiresIn = 0;
            }
        }
    }
   class DimensionalControlHelper
    {
        private DataContext _context;
        public static OAuth2Token Authenticate(string hostname, string clientId, string clientSecret, string username, string password)
        {
           // String uri = string.Format("https://stw.sf-api.com/oauth/token", hostname);
             String uri = string.Format("https://{0}/oauth/token", hostname);
            Console.WriteLine(uri);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("grant_type", "password");
            parameters.Add("client_id", clientId);
            parameters.Add("client_secret", clientSecret);
            parameters.Add("username", username);
            parameters.Add("password", password);

            ArrayList bodyParameters = new ArrayList();
            foreach (KeyValuePair<string, string> kv in parameters)
            {
                bodyParameters.Add(string.Format("{0}={1}", HttpUtility.UrlEncode(kv.Key), HttpUtility.UrlEncode(kv.Value.ToString())));
            }
            string requestBody = String.Join("&", bodyParameters.ToArray());

            HttpWebRequest request = WebRequest.CreateHttp(uri);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(requestBody);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Console.WriteLine(response.StatusCode);
            JObject token = null;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                String body = reader.ReadToEnd();
                token = JObject.Parse(body);
            }

            return new OAuth2Token(token);
        }

        public static string GetHostname(OAuth2Token token)
        {
            return string.Format("{0}.sf-api.com", token.Subdomain);
        }

        public static void addAuthorizationHeader(HttpWebRequest request, OAuth2Token token)
        {
            request.Headers.Add(string.Format("Authorization: Bearer {0}", token.AccessToken));
        }

        public static void addAuthorizationHeader(HttpWebRequest request, string token)
        {
            request.Headers.Add(string.Format("Authorization: Bearer {0}", token));
        }

        /// <summary>
        /// Get the root level Item for the provided user. To retrieve Children the $expand=Children
        /// parameter can be added.
        /// </summary>
        /// <param name="token">the OAuth2Token returned from Authenticate</param>
        /// <param name="getChildren">retrieve Children Items if true, default is false</param>
        public static JObject GetRoot(string token, string id,string subdomain,bool getChildren = true)
        {
             string hosturl= string.Format("{0}.sf-api.com", subdomain);
             //String uri = string.Format("https://{0}/sf/v3/Items", hosturl);

            // https://account.sf-api.com/sf/v3/Items(id)/Children?includeDeleted=false
            String uri = string.Format("https://{0}/sf/v3/Items({1})/Children?$select=FileName,Id,odata.type&$filter=ItemType eq Folder&includeDeleted=false", hosturl, id);
			//String uri = string.Format("https://{0}/sf/v3/Items({1})/Children?includeDeleted=false", hosturl,"fohfbffd-bb1a-4ad8-a1de-aa5ec1b4e450");
			
            //String uri = "https://STWServicesLLP.sf-api.com/sf/v3/Items(allshared)/Children";
			if (getChildren)
			{
				uri += "?$expand=Children";
			}
			Console.WriteLine(uri);

            HttpWebRequest request = WebRequest.CreateHttp(uri);
            DimensionalControlHelper.addAuthorizationHeader(request, token);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Console.WriteLine(response.StatusCode);
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                String body = reader.ReadToEnd();

                JObject root = JObject.Parse(body);

                return root;

                // just print Id, CreationDate, Name of each element
               /* Console.WriteLine(root["Id"] + " " + root["CreationDate"] + " " + root["Name"]);
                JArray children = (JArray)root["Children"];
                if (children != null)
                {
                    foreach (JObject child in children)
                    {
                        Console.WriteLine(child["Id"] + " " + child["CreationDate"] + " " + child["Name"]);
                    }
                }*/
            }
        }

        public static JObject GetTreeView(string token, string subdomain, string id)
        {
             string hosturl= string.Format("{0}.sf-api.com", subdomain);
             //String uri = string.Format("https://{0}/sf/v3/Items", hosturl);

            // https://account.sf-api.com/sf/v3/Items(id)/Children?includeDeleted=false
            String uri = string.Format("https://{0}/sf/v3/Items({1})?includeDeleted=false&treemode=mode&sourceId=id&canCreateRootFolder=false", hosturl, id);
			//String uri = string.Format("https://{0}/sf/v3/Items({1})/Children?includeDeleted=false", hosturl,"fohfbffd-bb1a-4ad8-a1de-aa5ec1b4e450");
			
            //String uri = "https://STWServicesLLP.sf-api.com/sf/v3/Items(allshared)/Children";
			
			Console.WriteLine(uri);

            HttpWebRequest request = WebRequest.CreateHttp(uri);
            DimensionalControlHelper.addAuthorizationHeader(request, token);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Console.WriteLine(response.StatusCode);
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                String body = reader.ReadToEnd();

                JObject root = JObject.Parse(body);

                return root;

                
            }
        }

        /// <summary>
        /// Get a single Item by Id.
        /// </summary>
        /// <param name="token">the OAuth2Token returned from Authenticate</param>
        /// <param name="id">an item id</param>
        public static JObject GetItemById(string token, string subdomain,string id)
        {

             string hosturl= string.Format("{0}.sf-api.com", subdomain);
            
           String uri = string.Format("https://{0}/sf/v3/Items({1})", hosturl, id);
            Console.WriteLine(uri);

            HttpWebRequest request = WebRequest.CreateHttp(uri);
            DimensionalControlHelper.addAuthorizationHeader(request, token);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Console.WriteLine(response.StatusCode);
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                String body = reader.ReadToEnd();

                JObject item = JObject.Parse(body);
                //Console.WriteLine(item["Id"] + " " + item["CreationDate"] + " " + item["Name"]);

                return item;
            }
        }

        

        /// <summary>
        /// Get a folder using some of the common query parameters that are available. This will
        /// add the expand, select parameters. The following are used:
        /// expand=Children to get any Children of the folder
        /// select=Id,Name,Children/Id,Children/Name,Children/CreationDate to get the Id, Name of the folder and the Id, Name, CreationDate of any Children
        /// </summary>
        /// <param name="token">the OAuth2Token returned from Authenticate</param>
        /// <param name="id">a folder id</param>
        public static void GetFolderWithQueryParameters(string token,string subdomain ,string id)
        {
            string hosturl = string.Format("{0}.sf-api.com", subdomain);

            String uri = string.Format("https://{0}/sf/v3/Items({1})?$expand=Children&$select=Id,Name,Children/Id,Children/Name,Children/CreationDate", hosturl, id);
           // String uri = string.Format("https://stw.sf-api.com/sf/v3/Items({1})?$expand=Children&$select=Id,Name,Children/Id,Children/Name,Children/CreationDate", DimensionalControlHelper.GetHostname(token), id);
            Console.WriteLine(uri);

            HttpWebRequest request = WebRequest.CreateHttp(uri);
            DimensionalControlHelper.addAuthorizationHeader(request, token);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Console.WriteLine(response.StatusCode);
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                String body = reader.ReadToEnd();

                JObject folder = JObject.Parse(body);
                // only Id and Name are available because we specifically selected only those two Properties
                Console.WriteLine(folder["Id"] + " " + folder["Name"]);
                JArray children = (JArray)folder["Children"];
                if (children != null)
                {
                    foreach (JObject child in children)
                    {
                        // CreationDate is also available on Children because we specifically selected that property in addition to Id, Name
                        Console.WriteLine(child["Id"] + " " + child["CreationDate"] + " " + child["Name"]);
                    }
                }
            }
        }

        /// <summary>
        ///  get all files under module folder 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="subdomain"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static JArray GetAllModuleFolderFiles(string token, string subdomain, string id)
        {
            string hosturl = string.Format("{0}.sf-api.com", subdomain);
           
            String uri = string.Format("https://{0}/sf/v3/Items({1})/Children?$select=Id&includeDeleted=false", hosturl, id);
           
            //Console.WriteLine(uri);

            HttpWebRequest request = WebRequest.CreateHttp(uri);
            DimensionalControlHelper.addAuthorizationHeader(request, token);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //Console.WriteLine(response.StatusCode);
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                String body = reader.ReadToEnd();

                JObject folder = JObject.Parse(body);
                // only Id and Name are available because we specifically selected only those two Properties
               
                JArray children = (JArray)folder["Children"];
                return children;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids">["id1","id2"] </param>
        /// <param name="subdomain">subdomain</param>
        /// <param name="moduleId">Folder Id</param>
        /// <returns></returns>
        public static HttpWebResponse DownloadItems(string token,string subdomain, string moduleId)
        {
            string hosturl = string.Format("{0}.sf-api.com", subdomain);
            https://modsmanagement.sf-api.com
            String uri = string.Format("https://{0}/sf/v3/Items({1})/Download?includeallversions=false&includeDeleted=false", hosturl, moduleId);
            Console.WriteLine(uri);

           HttpWebRequest request = WebRequest.CreateHttp(uri);
            addAuthorizationHeader(request, token);
            request.AllowAutoRedirect = true;
 
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
           
            Console.WriteLine(response.StatusCode);

            return response;
        }


        public static JObject CreateFolder(string token, string parentId, string subdomain, string name, string description)
        {
            string hosturl = string.Format("{0}.sf-api.com", subdomain);
            String uri = string.Format("https://{0}/sf/v3/Items({1})/Folder", hosturl, parentId);
            Console.WriteLine(uri);

            HttpWebRequest request = WebRequest.CreateHttp(uri);
            addAuthorizationHeader(request, token);

            Dictionary<string, object> folder = new Dictionary<string, object>();
            folder.Add("Name", name);
            folder.Add("Description", description);
            string json = JsonConvert.SerializeObject(folder);

            //Console.WriteLine(json);

            request.Method = "POST";
            request.ContentType = "application/json";
            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(json);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Console.WriteLine(response.StatusCode);
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                String body = reader.ReadToEnd();
                JObject newFolder = JObject.Parse(body);
                // Console.WriteLine("Created Folder: " + newFolder["Id"]);

                return newFolder;
            }
        }

        public static JObject DownloadItem(string token, string subdomain, string itemId,  string email, DateTime date)
        {
            string hosturl = string.Format("{0}.sf-api.com", subdomain);

            String uri = string.Format("https://{0}/sf/v3/Shares?notify=false", hosturl, itemId);
            Console.WriteLine(uri);

            HttpWebRequest request = WebRequest.CreateHttp(uri);
            addAuthorizationHeader(request, token);
            request.AllowAutoRedirect = true;

            // Dictionary<string, object> Sharefolder = new Dictionary<string, object>();
            //Sharefolder.Add("ShareType", "Send");
            //Sharefolder.Add("Title", "Sample Send Share");
            //Sharefolder.Add("Recipients", "[{ 'User': { 'Email': 'sandeep@softechworld.com' } }]");
            //Sharefolder.Add("Parent", "{ 'Id':'"+itemId+"' }");
            //Sharefolder.Add("ExpirationDate", DateTime.Today.AddDays(10));
            //Sharefolder.Add("RequireUserInfo", false);
            //Sharefolder.Add("MaxDownloads", 4);
            // Sharefolder.Add("UsesStreamIDs", false);

            ShareObject shareObject = new ShareObject();

            shareObject.ShareType = "send";
            shareObject.Title = "Send file";
            shareObject.Items = new List<Parent>();
            shareObject.Items.Add(new Parent() { Id = itemId });
           // shareObject.Parent = new Parent() { Id = itemId };
            shareObject.Recipients = new List<ShareWith>();
            shareObject.Recipients.Add(new ShareWith() { User = new ShareUser() { Email = "swati@softechworld.com" } });
          //  shareObject.Parent = new Parent() { Id = itemId };
            shareObject.ExpirationDate = date.ToString("yyyy-MM-dd");
            shareObject.RequireUserInfo = false;
            shareObject.MaxDownloads = 4;
            shareObject.UsesStreamIDs = false;


            string json = JsonConvert.SerializeObject(shareObject);

            //Console.WriteLine(json);

            request.Method = "POST";
            request.ContentType = "application/json";
            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(json);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
           // Console.WriteLine(response.StatusCode);
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                String body = reader.ReadToEnd();

                JObject item = JObject.Parse(body);
                //Console.WriteLine(item["Id"] + " " + item["CreationDate"] + " " + item["Name"]);

                return item;
            }
        }



         public static JObject SendItem(string token, string subdomain, string itemId,  string email, DateTime date)
        {
            string hosturl = string.Format("{0}.sf-api.com", subdomain);

            String uri = string.Format("https://{0}/sf/v3/Shares?notify=false", hosturl, itemId);
            Console.WriteLine(uri);

            HttpWebRequest request = WebRequest.CreateHttp(uri);
            addAuthorizationHeader(request, token);
            request.AllowAutoRedirect = true;
                
             Dictionary<string, object> Sharefolder = new Dictionary<string, object>();
            Sharefolder.Add("Subject", "Send Files");
            Sharefolder.Add("Title", "Sample Send Share");
            Sharefolder.Add("Emails", "[{ 'User': { 'Email': '"+email+"' } }]");
            Sharefolder.Add("Items", "[{ 'Id':'"+itemId+"' }]");
             Sharefolder.Add("Body", "Email Message");
            Sharefolder.Add("ExpirationDays", -1);
            Sharefolder.Add("RequireUserInfo", false);
            Sharefolder.Add("MaxDownloads", 4);
             Sharefolder.Add("UsesStreamIDs", false);
            string json = JsonConvert.SerializeObject(Sharefolder);

            //Console.WriteLine(json);

            request.Method = "POST";
            request.ContentType = "application/json";
            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(json);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
           // Console.WriteLine(response.StatusCode);
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                String body = reader.ReadToEnd();

                JObject item = JObject.Parse(body);
                //Console.WriteLine(item["Id"] + " " + item["CreationDate"] + " " + item["Name"]);

                return item;
            }
        }

        public static bool  UploadFile(string token, string subdomain, string parentId, string filename ,byte[] fileData)
        {
            string hosturl = string.Format("{0}.sf-api.com", subdomain);
            String uri = string.Format("https://{0}/sf/v3/Items({1})/Upload", hosturl, parentId);
            Console.WriteLine(uri);

            HttpWebRequest request = WebRequest.CreateHttp(uri);
            addAuthorizationHeader(request, token);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                String body = reader.ReadToEnd();

                JObject uploadConfig = JObject.Parse(body);
                string chunkUri = (string)uploadConfig["ChunkUri"];
                if (chunkUri != null)
                {
                    //Console.WriteLine("Starting Upload");
                    UploadMultiPartFile("File1", filename,fileData, chunkUri);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Does a multipart form post upload of a file to a url.
        /// </summary>
        /// <param name="parameterName">multipart parameter name. File1 for a standard upload.</param>
        /// <param name="file">the FileInfo to upload</param>
        /// <param name="uploadUrl">the url of the server to upload to</param>
        public static void UploadMultiPartFile(string parameterName, string filename,byte[] file, string uploadUrl)
        {
            string boundaryGuid = "upload-" + Guid.NewGuid().ToString("n");
            string contentType = "multipart/form-data; boundary=" + boundaryGuid;
 
            MemoryStream ms = new MemoryStream();
            byte[] boundaryBytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundaryGuid + "\r\n");
 
            // Write MIME header
            ms.Write(boundaryBytes, 2, boundaryBytes.Length - 2);
            string header = String.Format(@"Content-Disposition: form-data; name=""{0}""; filename=""{1}""" +
                "\r\nContent-Type: application/octet-stream\r\n\r\n", parameterName, filename);
            byte[] headerBytes = System.Text.Encoding.UTF8.GetBytes(header);
            ms.Write(headerBytes, 0, headerBytes.Length);

            ms.Write(file, 0, file.Length);
             

            // Load the file into the byte array
           // using (FileStream source = file.OpenRead())
            //{
             //   byte[] buffer = new byte[1024 * 1024];
             //   int bytesRead;
 
             //   while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
              //  {
               //     ms.Write(buffer, 0, bytesRead);
              //  }
            //}
 
            // Write MIME footer
            boundaryBytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundaryGuid + "--\r\n");
            ms.Write(boundaryBytes, 0, boundaryBytes.Length);
 
            byte[] postBytes = ms.ToArray();
            ms.Close();
 
            HttpWebRequest request = WebRequest.CreateHttp(uploadUrl);
            request.Timeout = 1000 * 60; // 60 seconds
            request.Method = "POST";
            request.ContentType = contentType;
            request.ContentLength = postBytes.Length;
            request.Credentials = CredentialCache.DefaultCredentials;
 
            using (Stream postStream = request.GetRequestStream())
            {
                int chunkSize = 48 * 1024;
                int remaining = postBytes.Length;
                int offset = 0;
 
                do
                {
                    if (chunkSize > remaining) { chunkSize = remaining; }
                    postStream.Write(postBytes, offset, chunkSize);
 
                    remaining -= chunkSize;
                    offset += chunkSize;
 
                } while (remaining > 0);
 
                postStream.Close();
            }
 
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Console.WriteLine("Upload Status: " + response.StatusCode);
            response.Close();
        }
    }
}
