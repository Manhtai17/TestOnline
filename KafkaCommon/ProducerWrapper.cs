using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace ELearning.KafkaCommon
{
	public class ProducerWrapper<Tkey, TValue> : IDisposable
	{
		private readonly ProducerConfig _config;
		private readonly IProducer<Tkey, TValue> _producer;
		private readonly string _topic;

		public ProducerWrapper(ProducerConfig config, string topic)
		{
			_config = config;
			_producer = new ProducerBuilder<Tkey, TValue>(_config).Build();
			_topic = topic;
		}

		public async Task SendMessage(TValue message)
		{
			var dr = await _producer.ProduceAsync(_topic, new Message<Tkey, TValue>()
			{
				Value = message
			});
		}

		public void Dispose()
		{
			_producer.Dispose();
		}

	}
}
