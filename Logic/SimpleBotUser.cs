using MongoDB.Bson;
using MongoDB.Driver;
using SimpleBot.Persistence;
using SimpleBot.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBot.Logic
{
    public class SimpleBotUser
    {
        public SimpleBotUser()
        {
            this._rep = new MessageRep();
        }

        private readonly MessageRep _rep;


        public string Reply(SimpleMessage message)
        {
            
       
            BsonDocument document = new BsonDocument()
            {
                { "id", Environment.MachineName },
                { "nome", message.User },
                { "mensagem", message.Text }
            };
            Connection.GravarLogAcesso(Environment.MachineName);

            Connection.Insert(document);


            return $"{message.User} disse '{message.Text}";
        }

    }
}