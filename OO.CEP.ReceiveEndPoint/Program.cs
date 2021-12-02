using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OO.CEP.ReceiveEndPoint.Data;
using OO.CEP.ReceiveEndPoint.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OO.CEP.ReceiveEndPoint
{
    public class Program
    {
        private static readonly CategoryDbContext _context;
        public static void Main(string[] args)
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
                    _context.categories.Add(JsonConvert.DeserializeObject<Category>(message));
                    _context.SaveChanges();
                    Console.WriteLine(" Add successful ! ", message);
                };

                channel.BasicConsume(queue: "queue-example",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
    }
}
