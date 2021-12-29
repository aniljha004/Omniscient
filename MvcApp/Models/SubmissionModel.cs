using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MvcApp.Models
{
    public class SubmissionModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
