using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Sergel.DLR.DataAccess.CustomResponses;
using Sergel.DeliveryReport.DataAccess.Repositories;
using Sergel.DeliveryReport.DataAccess.CustomResponses;

namespace Sergel.DeliveryReport.Api
{
    public class DeliveryReport
    {
        private readonly IDeliveryReportRepository _deliveryReportRepository;
        private readonly IMoDeliveryReportRepository _moDeliveryReportRepository;
        private readonly IUserRepository _userRepository;

        public DeliveryReport(
                               IDeliveryReportRepository deliveryReportRepository,
                               IMoDeliveryReportRepository moDeliveryReportRepository,
                               IUserRepository userRepository
                             )
        {
            _deliveryReportRepository = deliveryReportRepository;
            _moDeliveryReportRepository = moDeliveryReportRepository;
            _userRepository = userRepository;
        }

        [FunctionName("GetDeliveryReport")]
        public async Task<IActionResult> GetDeliveryReport([HttpTrigger(AuthorizationLevel.Anonymous, "get",
                                                           Route = "GetDeliveryReport/{gateId}")] HttpRequest request, ILogger log,
                                                           string gateId
                                                          )
        {

            Response<DeliveryReportResponse> response = new Response<DeliveryReportResponse>();
            response.ResponseData = new DeliveryReportResponse();

            try
            {
                string refId = request.Query["refId"];
                string messageId = request.Query["messageId"];

                DateTime? startDateTime = null;
                if (!String.IsNullOrEmpty(request.Query["startDateTime"]))
                { startDateTime = Convert.ToDateTime(request.Query["startDateTime"]); }

                DateTime ? endDateTime = null;
                if (!String.IsNullOrEmpty(request.Query["endDateTime"]))
                {  endDateTime = Convert.ToDateTime(request.Query["endDateTime"]); }

                string shortCode = request.Query["shortCode"];
                string keyword = request.Query["keyword"];

                string authHeader = request.Headers["Authorization"];

                if (await _userRepository.IsUserAuthenticated(authHeader, gateId))
                {

                    var validationError = DeliveryReportRequestValidation.Validate(gateId, refId, messageId, keyword, shortCode, startDateTime, endDateTime);

                    if (validationError.Length==0)
                    {
                        if (string.IsNullOrEmpty(keyword))
                        {
                            response.ResponseData.DeliveryReports = await _deliveryReportRepository.GetDeliveryReportAsyn(gateId, refId, messageId, startDateTime, endDateTime);
                            
                            if (response?.ResponseData?.DeliveryReports?.Count() == 0)
                            {
                                response.ResponseCode = StatusCodes.Status404NotFound;
                                response.ResponseMessage = "No data found";

                                return new NotFoundObjectResult(response);
                            }

                        }
                        else
                        {
                            response.ResponseData.MoDeliveryReports = await _moDeliveryReportRepository.GetMoDeliveryReportAsyn(gateId, refId, messageId, keyword, shortCode, startDateTime, endDateTime);

                            if (response?.ResponseData?.MoDeliveryReports?.Count() == 0)
                            {
                                response.ResponseCode = StatusCodes.Status404NotFound;
                                response.ResponseMessage = "No data foound";
                            }
                        }

                        response.ResponseCode = StatusCodes.Status200OK;
                        response.ResponseMessage = "Report retrieved successfully.";

                        return new OkObjectResult(response);
                    }

                    response.ResponseData = null;
                    response.ResponseCode = StatusCodes.Status400BadRequest;
                    response.ResponseMessage = validationError.ToString();

                    return new BadRequestObjectResult(response);
               }
             else
            {
                response.ResponseData = null;
                response.ResponseCode = StatusCodes.Status401Unauthorized;
                response.ResponseMessage = "User is unauthorized";

                return new UnauthorizedObjectResult(response);
            }
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