using MongoDB.Bson;
using MongoDB.Driver;

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
    }
}