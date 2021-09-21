using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sergel.DeliveryReport.DataAccess.Repositories;
using Sergel.DeliveryReport.DataAccess.ServiceModels;
using System.Text;
using Sergel.DeliveryReport.DataAccess.CustomResponses;
using System.Collections.Generic;

namespace Sergel.DeliveryReport.Api
{
    public class User
    {
        private readonly IUserRepository _userRespository;

        public User(IUserRepository userRepository)
        {
            _userRespository = userRepository;
        }

        [FunctionName("GetAllUser")]
        public async Task<IActionResult> GetAllUser(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetAllUser")] HttpRequest req,
            ILogger log)
        {
            Response<IEnumerable<UserModel>> response = new Response<IEnumerable<UserModel>>();

            try
            {
                response.ResponseData = await _userRespository.GetAllUser();
                response.ResponseCode = StatusCodes.Status200OK;
                response.ResponseMessage = "User data retrieved successfully.";

                return new OkObjectResult(response);
            }

            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);

                response.ResponseData = null;
                response.ResponseCode = StatusCodes.Status400BadRequest;
                response.ResponseMessage = "Error occuered while processing your request.";

                return new BadRequestObjectResult(response);
            }

        }


        [FunctionName("UpdateUser")]
        public async Task<IActionResult> UpdateUser(
                                            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "UpdateUser")] HttpRequest req,
                                            ILogger log
                                            )
        {
            Response<string> response = new Response<string>();

            try
            {
                string requestBody = String.Empty;
                using (StreamReader streamReader = new StreamReader(req.Body))
                {
                    requestBody = await streamReader.ReadToEndAsync();
                }

                var request = JsonConvert.DeserializeObject<UserModel>(requestBody)as UserModel;

                if (request != null)
                {
                    await _userRespository.UpdateUser(request);

                    response.ResponseData = null;
                    response.ResponseCode = StatusCodes.Status200OK;
                    response.ResponseMessage = "User has been updated successfully.";

                    return new OkObjectResult(response);
                }

                response.ResponseData = null;
                response.ResponseCode = StatusCodes.Status400BadRequest;
                response.ResponseMessage = "Please pass correct request body.";

                return new BadRequestObjectResult(response);
            }

            catch (Exception ex)
            {
                log.LogError(ex, ex.Message);

                response.ResponseData = null;
                response.ResponseCode = StatusCodes.Status400BadRequest;
                response.ResponseMessage = "Error occuered while processing your request.";

                return new BadRequestObjectResult(response);
            }
        }
    }
}
