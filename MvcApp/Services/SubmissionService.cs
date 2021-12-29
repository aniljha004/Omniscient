using MongoDB.Driver;
using MvcApp.Models;
using System.Collections.Generic;

namespace MvcApp.Services
{
    public class SubmissionService
    {
        private readonly IMongoCollection<SubmissionModel> _submissions;

        public SubmissionService(IDatabaseSettings settings)
        {
            //var client = new MongoClient(settings.ConnectionString);
            var client = new MongoClient();

            var database = client.GetDatabase(settings.DatabaseName);
            _submissions = database.GetCollection<SubmissionModel>("Submissions");
        }

        public SubmissionModel Create(SubmissionModel submission)
        {
            _submissions.InsertOne(submission);
            return submission;
        }

        public IList<SubmissionModel> Read() =>
            _submissions.Find(sub => true).ToList();

        public SubmissionModel Find(string id) =>
            _submissions.Find(sub => sub.Id == id).SingleOrDefault();

        public void Update(SubmissionModel submission) =>
            _submissions.ReplaceOne(sub => sub.Id == submission.Id, submission);

        public void Delete(string id) =>
            _submissions.DeleteOne(sub => sub.Id == id);
    }
}

