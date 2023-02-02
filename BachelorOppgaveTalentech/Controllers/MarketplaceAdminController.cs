using System;
using System.Collections.Generic;
using System.Linq;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace BachelorOppgaveTalentech.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarketplaceAdminController : ControllerBase
    {
        private readonly ILogger<MarketplaceAdminController> _logger;

        private string _connectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
        private string _containerName = "appsinfo";

        [HttpGet]
        public IActionResult Get()
        {

            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_containerName);


            List<dynamic> blobs2 = new List<dynamic>();
            var blobs = containerClient.GetBlobs();
            foreach (var blob in blobs)
            {
                BlobClient blobClient = containerClient.GetBlobClient(blob.Name);
                var response = blobClient.Download();

                using var reader = new StreamReader(response.Value.Content);
                var data = reader.ReadToEnd();
                blobs2.Add(JsonSerializer.Deserialize<dynamic>(data));
            }
            Console.WriteLine(blobs2.ToArray());
            return Ok(blobs2.ToArray());
        }
    }
}
