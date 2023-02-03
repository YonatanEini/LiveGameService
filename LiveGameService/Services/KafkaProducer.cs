using Confluent.Kafka;
using LiveGameService.objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveGameService.Services
{
    public class KafkaProducer
    {
        public string Topic { get; set; }
        private readonly ProducerConfig _config;
        public KafkaProducer(string gameID)
        {
            this.Topic = gameID;
            this._config = new ProducerConfig
            {
                BootstrapServers = "127.0.0.1:9092",
            };
        }
        public async Task<bool> WriteToKafka(ScoreUpdateDto updateDto)
        {
            using var producer = new ProducerBuilder<Null, string>(_config).Build();
            try
            {
                var updateDtoJson = JsonConvert.SerializeObject(updateDto);
                var deliveryResult = await producer.ProduceAsync(Topic, new Message<Null, string> { Value = updateDtoJson });
                return deliveryResult.Status == PersistenceStatus.Persisted;
            }
            catch (ProduceException<Null, string> e)
            {
                return false;
            }
        }
    }
}
