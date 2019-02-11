using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBot.Persistence;

namespace SimpleBot.Persistence.Repository
{
    public class MessageRep : Common.Repository
    {
        public MessageRep()
            : base("message") { }

        public void Insert(BsonDocument document)
        {
            Connection._table.InsertOne(document);
        }
    }
}