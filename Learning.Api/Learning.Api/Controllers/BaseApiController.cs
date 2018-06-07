using Learning.Api.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Learning.Api.Controllers
{
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiController"/> class. 
        /// </summary>
        public BaseApiController()
        {
        }

        /// <summary>
        /// Method that creates a <see cref="IHttpActionResult"/> based on the content of <see cref="Response"/>
        /// </summary>
        /// <param name="response">The api reponse wrapper</param>
        /// <returns>The IHttpActionResult created</returns>
        protected virtual IHttpActionResult CreateHttpResponse(Response response)
        {
            if (response.Successful)
            {
                if (response.ResultType == ResultType.Empty)
                {
                    return this.NotFound();
                }

                return this.StatusCode(HttpStatusCode.NoContent);
            }

            return this.HandleErrors(response);
        }

        /// <summary>
        /// Method that creates a <see cref="IHttpActionResult"/> based on the content of <see cref="Response{T}"/>
        /// </summary>
        /// <typeparam name="T">
        /// The type of the class to create response from
        /// </typeparam>
        /// <param name="response">
        /// The api reponse wrapper
        /// </param>
        /// <returns>
        /// The IHttpActionResult created
        /// </returns>
        protected virtual IHttpActionResult CreateHttpResponse<T>(Response<T> response) where T : class
        {
            if (response.Successful)
            {
                if (response.HasResult)
                {
                    return this.Ok(response.Result);
                }

                if (response.ResultType == ResultType.Empty)
                {
                    return this.NotFound();
                }

                return this.StatusCode(HttpStatusCode.NoContent);
            }

            return this.HandleErrors(response);
        }

        private IHttpActionResult HandleErrors(Response response)
        {
            if (response.ResultType == ResultType.ValidationError || response.ResultType == ResultType.CustomerInformation)
            {
                return this.BadRequest(response.Message);
            }

            if (!string.IsNullOrEmpty(response.Message))
            {
                return this.ResponseMessage(this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, response.Message));
            }

            return this.InternalServerError();
        }
    }
}
