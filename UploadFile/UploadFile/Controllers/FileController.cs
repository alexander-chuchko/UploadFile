using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;

namespace UploadFile.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FileController : ControllerBase
    {

        [HttpPost, DisableRequestSizeLimit]
        [Route("createfile")]
        public async Task<IActionResult> CreateFile([FromForm] DataUser dataUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var tempProfile = dataUser;
                    BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=storagealexchuchko;AccountKey=Udh+PMdPg0F8ZXvtyLzSogNFPSzf/o40WhnG/30QohOaA4pDq5Gay64Px4uA/xq4TMVlHlMaI6kt+AStAmU4HQ==;EndpointSuffix=core.windows.net");
                    BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient("container1");
                    blobContainerClient.CreateIfNotExists();
                    var containerClient = blobContainerClient.GetBlobClient(tempProfile.Picture.FileName);

                    /*
                    BlobUploadOptions options = new BlobUploadOptions();
                    options.Tags = new Dictionary<string, string>
                    {
                        { "email", dataUser.Email },
                    };*/

                    //options.HttpHeaders.ContentType = tempProfile.Picture.ContentType;

                    var options = new BlobHttpHeaders
                    {
                        ContentType = tempProfile.Picture.ContentType,
                        ContentDisposition = tempProfile.Picture.ContentDisposition
                    };

                    await containerClient.UploadAsync(tempProfile.Picture.OpenReadStream(), options);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                return Ok();
            }

            return BadRequest();
        }
    }
}
