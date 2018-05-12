using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGRForms.Database
{
    public class FirebaseConnection
    {
        private IFirebaseClient _client;

        public FirebaseConnection()
        {
            IFirebaseConfig config = new FirebaseConfig()
            {
                BasePath = "https://projektgrupowy-pg2018.firebaseio.com"
            };
            _client = new FirebaseClient(config);

        }

        public Dictionary<string, List<Measurement>> GetAllSessionsMeas()
        {
            FirebaseResponse response = _client.Get("networkInfo");
            return response.ResultAs<Dictionary<string, List<Measurement>>>();
        }

        public List<Measurement> GetSingleSessionMeas(string session)
        {
            FirebaseResponse response = _client.Get($"networkInfo/{session}");
            return response.ResultAs<List<Measurement>>();
        }
    }
}
