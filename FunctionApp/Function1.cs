using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace FunctionApp
{
    public class Function1
    {
        private readonly MyContext _context;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IAssetRepository _assetRepository;
        public Function1(MyContext context, IServiceScopeFactory scopeFactory, IAssetRepository assetRepository)
        {
            _context = context;
            _scopeFactory = scopeFactory;
            _assetRepository = assetRepository;
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("ParallelRun")]
        public async Task<IActionResult> ParallelRun(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            var dataList = await _context.Assets.ToListAsync();

            Parallel.ForEach(dataList, async data =>
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    //var scopedContext = scope.ServiceProvider.GetRequiredService<MyContext>();
                    //data.Desc = data.Name + " " + DateTime.Now.ToString();

                    //scopedContext.Assets.Update(data);
                    //await scopedContext.SaveChangesAsync();

                    var assetRepoContext = scope.ServiceProvider.GetRequiredService<IAssetRepository>();
                    data.Desc = data.Name + " " + DateTime.Now.ToString();

                    assetRepoContext.Update(data);
                }

                //var assetRepoContext = scope.ServiceProvider.GetRequiredService<IAssetRepository>();
                data.Desc = data.Name + " " + DateTime.Now.ToString();

                _context.Assets.Update(data);
                _context.SaveChanges();
            });

            return new OkObjectResult(dataList);
        }

        [FunctionName("TaskRun")]
        public async Task<IActionResult> TaskRun(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            var dataList = await _context.Assets.ToListAsync();

            var task = Task.Run(() =>
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var scopedContext = scope.ServiceProvider.GetRequiredService<MyContext>();

                    Task.Delay(5000);
                    var test = scopedContext.Assets.Find(1);
                    Console.WriteLine(test.Name);

                    //var assetRepoContext = scope.ServiceProvider.GetRequiredService<IAssetRepository>();
                    //data.Desc = data.Name + " " + DateTime.Now.ToString();

                    //assetRepoContext.Update(data);
                }
            });

            return new OkObjectResult(dataList);
        }
    }
}
