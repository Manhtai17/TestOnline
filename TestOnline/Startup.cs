using ApplicationCore;
using ApplicationCore.Entity;
using AutoMapper;
using Confluent.Kafka;
using Infrastructure.DatabaseContext;
using Infrastructure.Repository;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Text;
using TestOnline.Interfaces;
using TestOnline.MiddleWare;
using TestOnline.Services;
using static ApplicationCore.Enums.Enumration;

namespace TestOnline
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			DatabaseContext.ConnectionString = "server = localhost; port = 3306; user =root; password =1234	; database =elearning";
			services.AddControllers();

			var consumerConfig = new ConsumerConfig();
			Configuration.Bind("Kafka:ConsumerConfig", consumerConfig);

			var producerConfig = new ProducerConfig();
			Configuration.Bind("Kafka:ProducerConfig", producerConfig);

			services.AddSingleton<ConsumerConfig>(consumerConfig);
			services.AddSingleton<ProducerConfig>(producerConfig);

			services.AddTransient(typeof(IBaseEntityService<>), typeof(BaseService<>));
			services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
			services.AddTransient<ITermService, TermService>();
			services.AddTransient<ITermRepository, TermRepository>();

			services.AddTransient<IContestService, ContestService>();
			services.AddTransient<IContestRepository, ContestRepository>();

			services.AddTransient<IExamService, ExamService>();
			services.AddTransient<IExamRepository, ExamRepository>();


			var mappingConfig = new MapperConfiguration(mc =>
			{
				mc.AddProfile(new MappingProfile());
			});
			IMapper mapper = mappingConfig.CreateMapper();
			services.AddSingleton(mapper);
			services.AddMvc();
			services.AddMvc(options => options.EnableEndpointRouting = false);

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service", Version = "v1" });
			});
		}


		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseCors();

			app.Use(async (context, next) =>
			{
				var actionResult = new ActionServiceResult();
				var path = context.Request.Path.ToString();
				//var authorization = context.Request.Headers["Authorization"].ToString();
				var userID = context.Request.Headers["UserID"].ToString();
				if (path.Contains("api") && string.IsNullOrEmpty(userID))
				{
					context.Response.StatusCode = 401;
					context.Response.ContentType = "application/json";
					actionResult.Success = false;
					actionResult.Code = Code.ValidateEntity;
					actionResult.Message = Resources.ValidateEntity;
					actionResult.Data = null;
					var jsonString = JsonConvert.SerializeObject(actionResult);
					await context.Response.WriteAsync(jsonString, Encoding.UTF8);
					await context.Response.CompleteAsync();
					return;
				}
				await next.Invoke();
			});

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service V1");
				c.RoutePrefix = "swagger";
			});
			app.UseHttpsRedirection();

			app.UseMiddleware(typeof(ErrorHandlingMiddleware));

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
