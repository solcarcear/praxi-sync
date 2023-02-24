using praxi_auth_creatio.Model;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

namespace praxi_auth_creatio
{
    public class CreatioAuth:ICreatioAuth
    {
        //private static CookieContainer AuthCookie = new CookieContainer();
        private string _authServiceUri = "";
        private string _authUsr = "";
        private string _authPass = "";

        private CookieContainer AuthCookie = new CookieContainer();

        public CreatioAuth(IOptions<AppSettings> settings)
        {
            _authServiceUri = settings.Value.UrlCreatio;
            _authUsr = settings.Value.UsrName;
            _authPass = settings.Value.UsrPass;
            GetConnectionBPM();
        }

        public CookieContainer GetAuthCookie()
        {
            return GetConnectionBPM();
        }

        private CookieContainer GetConnectionBPM()
        {
            try
            {
                string file = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Cookie/cookies.dat");
                AuthCookie = ReadCookiesFromDisk(file);
                LoginBPM();
            }
            catch { }
            return AuthCookie;

        }




        private bool LoginBPM()
        {
            try
            {
                var authRequest = HttpWebRequest.Create(_authServiceUri) as HttpWebRequest;
                authRequest.Method = "POST";
                authRequest.ContentType = "application/json";
                authRequest.CookieContainer = AuthCookie;
                authRequest.Headers.Set("ForceUseSession", "true");

                try
                {

                    CookieCollection cookieCollection = AuthCookie.GetCookies(new Uri(_authServiceUri));
                    string csrfToken = cookieCollection["BPMCSRF"].Value;
                    authRequest.Headers.Add("BPMCSRF", csrfToken);

                }
                catch { }


                using (var requestStream = authRequest.GetRequestStream())
                {
                    using (var writer = new StreamWriter(requestStream))
                    {
                        writer.Write(@"{
                    ""UserName"":""" + _authUsr + @""",
                    ""UserPassword"":""" + _authPass + @"""
                    }");
                    }
                }

                ResponseStatus status = null;
                using (var response = (HttpWebResponse)authRequest.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        string responseText = reader.ReadToEnd();
                        //status = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<ResponseStatus>(responseText);
                        status = new ResponseStatus();


                    }

                }

                if (status != null)
                {
                    if (status.Code == 0)
                    {
                        WriteCookiesToDisk(AuthCookie);
                        return true;
                    }

                }
            }
            catch { }

            return false;
        }

        public void WriteCookiesToDisk(CookieContainer cookieJar)
        {
            string file = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "cookies.dat");

            using (Stream stream = File.Create(file))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, cookieJar);
                }
                catch (Exception e)
                {

                }
            }
        }

        public CookieContainer ReadCookiesFromDisk(string file)
        {

            try
            {
                using (Stream stream = File.Open(file, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return (CookieContainer)formatter.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                return new CookieContainer();
            }
        }

      
    }
}