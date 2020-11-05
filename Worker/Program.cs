using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Infrastructure.DatabaseContext;
using Infrastructure.Repository;
using Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Worker
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureServices((hostContext, services) =>
				{
					DatabaseContext.ConnectionString = "server = localhost; port = 3306; user =root; password =1234	; database =elearning";
					var configuration = hostContext.Configuration;
					var consumerConfig = new ConsumerConfig();
					configuration.Bind("Kafka:ConsumerConfig", consumerConfig);

					var producerConfig = new ProducerConfig();
					configuration.Bind("Kafka:ProducerConfig", producerConfig);

					services.AddSingleton<ConsumerConfig>(consumerConfig);
					services.AddSingleton<ProducerConfig>(producerConfig);

					//services.AddTransient(typeof(IBaseEntityService<>), typeof(BaseService<>));
					services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

					services.AddTransient<IExamRepository, ExamRepository>();
					services.AddHostedService<Worker>();
				});
	}
}
