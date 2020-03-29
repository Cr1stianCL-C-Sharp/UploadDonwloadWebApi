using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace UploadDonwloadWebApi.Controllers
{
    public class UploadController : ApiController
    {

        public async Task<IHttpActionResult> Post([FromUri]string filename)
        {
            Task<Stream> task = this.Request.Content.ReadAsStreamAsync();
            var requestStream = await task;

            try
            {
                Stream fileStream = File.Create(HttpContext.Current.Server.MapPath("~/" + filename));
                requestStream.CopyTo(fileStream);
                fileStream.Close();
                requestStream.Close();
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }

            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.Created;
            return Ok();
        }
    }
}
