using Cep.Backend.ReceiveEndPoint.Data;
using Cep.Backend.ReceiveEndPoint.Models;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Cep.Backend.ReceiveEndPoint
{
    class Program
    {
        static void Main(string[] args)
        {

            string url = "amqps://elgfojpi:aIaPZPD-CRKBR0xQgMqzgST6iWBxTfx0@fox.rmq.cloudamqp.com/elgfojpi";
            var factory = new ConnectionFactory() 
            {
                Uri = new Uri(url)
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "queue-example",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: true,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                    Category category = JsonConvert.DeserializeObject<Category>(message);
                    using (var _context = new CategoryDbContext())
                    {
                        _context.categories.Add(category);
                        _context.SaveChanges();
                        Console.WriteLine(" Add successful ! ", message);
                    }
                };

                channel.BasicConsume(queue: "queue-example",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
