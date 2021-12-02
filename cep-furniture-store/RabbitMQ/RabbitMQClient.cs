using cep_furniture_store.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cep_furniture_store.RabbitMQ
{
    public class RabbitMQClient
    {
        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _model;

        private static readonly string url = "amqps://elgfojpi:aIaPZPD-CRKBR0xQgMqzgST6iWBxTfx0@fox.rmq.cloudamqp.com/elgfojpi";

        public RabbitMQClient()
        {
            CreateConnection();
        }

        private static void CreateConnection()
        {
            // Create a ConnectionFactory and set the Uri to the CloudAMQP url
            // the connectionfactory is stateless and can safetly be a static resource in your app
            _factory = new ConnectionFactory
            {
                Uri = new Uri(url)
            };

            _connection = _factory.CreateConnection();
            _model = _connection.CreateModel();

            var queueName = "queue-example";
            bool durable = false;
            bool exclusive = false;
            bool autoDelete = true;

            _model.QueueDeclare(queueName, durable, exclusive, autoDelete, null);
        }

        public void Close()
        {
            _connection.Close();
        }

        public void SendMessage(Category category)
        {
            // read message from input
            var message = JsonConvert.SerializeObject(category);
            // the data put on the queue must be a byte array
            var data = Encoding.UTF8.GetBytes(message);
            // publish to the "default exchange", with the queue name as the routing key
            var exchangeName = "";
            var routingKey = "queue-example";
            _model.BasicPublish(exchangeName, routingKey, null, data);
        }

    }
}
