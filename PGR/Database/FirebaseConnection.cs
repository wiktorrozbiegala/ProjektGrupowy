using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PGRForms.Database
{
    public class FirebaseConnection
    {
        private IFirebaseClient _client;
        private EventStreamResponse _response;

        public FirebaseConnection()
        {
            IFirebaseConfig config = new FirebaseConfig()
            {
                BasePath = "https://projektgrupowy-pg2018.firebaseio.com"
            };
            _client = new FirebaseClient(config);

        }

        public Dictionary<string, Dictionary<string, Measurement>> GetAllSessionsMeas()
        {
            FirebaseResponse response = _client.Get("networkInfo");
            return response.ResultAs<Dictionary<string, Dictionary<string, Measurement>>>();
        }
        
        public Dictionary<string, Measurement> GetSingleSessionMeas(string session)
        {            
            FirebaseResponse response = _client.Get($"networkInfo/{session}");
            return response.ResultAs<Dictionary<string, Measurement>>();
        }

        public async void SetAction(Action action, string session, FirebaseAction actionType)
        {
            _response = await _client.OnAsync($"/networkInfo/{session}",
                added: (s, args, d) =>
                {
                    if (actionType == FirebaseAction.OnAdd)
                        action();
                },
                changed: (s, args, d) =>
                {
                    if (actionType == FirebaseAction.OnChange)
                        action();
                },
                removed: (s, args, d) =>
                {
                    if (actionType == FirebaseAction.OnDelete)
                        action();
                });
        }

        public void Dispose()
        {
            _response.Dispose();
        }
        public enum FirebaseAction
        {
            OnAdd,
            OnChange,
            OnDelete
        }
    }
}
