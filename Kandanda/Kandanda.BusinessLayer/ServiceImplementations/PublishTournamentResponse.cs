using System;
using Newtonsoft.Json.Linq;

namespace Kandanda.BusinessLayer.ServiceImplementations
{
    public class PublishTournamentResponse
    { 
        public static PublishTournamentResponse Create(string payload, Uri baseUri)
        {
            //TODO Add Error Handling
            var json = JObject.Parse(payload)["tournament"];
            return new PublishTournamentResponse
            {
                Id = (int) json["id"],
                Link = new Uri(baseUri, (string) json["link"])
            };
        }

        public int Id { get; set; }
        public Uri Link { get; set; }
    }
}
