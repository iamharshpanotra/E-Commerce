using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using ECommerceAPI.Core.DTOs;

namespace ECommerceAPI.App_Start.ExceptionHandling
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var response = new ApiResponse<string>
            {
                Success = false,
                Message = context.Exception.Message,
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            context.Result = new ErrorResult(context.Request, response);
        }
    }

    public class ErrorResult : IHttpActionResult
    {
        private readonly HttpRequestMessage _request;
        private readonly ApiResponse<string> _response;

        public ErrorResult(HttpRequestMessage request, ApiResponse<string> response)
        {
            _request = request;
            _response = response;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = _request.CreateResponse((HttpStatusCode)_response.StatusCode, _response);
            return Task.FromResult(response);
        }
    }
}