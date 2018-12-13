using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using PGRForms.Measurement;

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

        public Dictionary<string, Dictionary<string, BaseMeasurement>> GetAllSessionsMeas()
        {
            FirebaseResponse response = _client.Get("networkInfo");
            return response.ResultAs<Dictionary<string, Dictionary<string, BaseMeasurement>>>();
        }
        
        public Dictionary<string, BaseMeasurement> GetSingleSessionMeas(string session)
        {            
            FirebaseResponse response = _client.Get($"networkInfo/{session}");
            return response.ResultAs<Dictionary<string, BaseMeasurement>>();
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
