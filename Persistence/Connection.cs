using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace SimpleBot.Persistence
{
    public sealed class Connection
    {
        public Connection() { }

        private static MongoClient _client;
        private static IMongoDatabase _database;
        public static IMongoCollection<BsonDocument> _table;
        private static readonly Connection _instance = new Connection();

        public static Connection Open(string tableName)
        {

            _client = new MongoClient("mongodb://localhost:27017");
            _database = _client.GetDatabase("chatBot");
            _table = _database.GetCollection<BsonDocument>(tableName);
            return _instance;
        }

        public static void Insert(BsonDocument document)
        {
            _table.InsertOne(document);
        }

        public static  void GravarLogAcesso(string machineName)
        {
            var collection = _client.GetDatabase("chatBot").GetCollection<BsonDocument>("acessos");
            var values = collection.Find("{ chatBot: { $eq: " + machineName + " } }").ToList();
            int quantity = 0;
            var db = _client.GetDatabase("chatBot");
            var tb = db.GetCollection<BsonDocument>("acessos");

            if (values != null)
            {
                quantity = Convert.ToInt32(values[0]["quantity"]) + 1;
            }
            
            BsonDocument bson = new BsonDocument()
                {
                    { "user", Environment.MachineName },
                    { "lastaccess",  DateTime.Now.ToString()},
                    { "quantity", quantity }
                };
            tb.UpdateOne("{ user: { $eq: " + machineName + " } }", bson, new UpdateOptions() { IsUpsert = true });
        }
    }
}