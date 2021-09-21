using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Sergel.DeliveryReport.Api
{
    public static class DeliveryReportRequestValidation
    {
        public static StringBuilder Validate(string gateId,
                                                           string refId = "",
                                                           string messageId = "",
                                                           string shortCode = "",
                                                           string keyword = "",
                                                           DateTime? startDateTime = null,
                                                           DateTime? endDateTime = null)
        {
            string[] dateFormat = { "yyyy'-'MM'-'ddTHH':'mm':'ss'Z'" };

            try
            {
                var validationResult = new StringBuilder();

                if (String.IsNullOrEmpty(gateId))
                {
                    validationResult.Append("gateId is required, ");
                }
                if (!gateId.All(c => Char.IsLetterOrDigit(c)))
                {
                    validationResult.Append("gateId Must be Numeric or String, ");
                }
                if (gateId.Length != 8)
                {
                    validationResult.Append("Length of gateId Must be 8, ");
                }
               
                if (!String.IsNullOrEmpty(refId))
                {
                    if (refId.Length != 4)
                    {
                        validationResult.Append("Length of refId Must be 4, ");
                    }
                    if (!int.TryParse(refId, out int n))
                    {
                        validationResult.Append("refId Must be Numeric, ");
                    }
                }

                if (startDateTime != null)
                {
                    DateTime startDate;

                    if (!DateTime.TryParseExact(startDateTime.ToString(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
                    {
                        validationResult.Append("Provide Timestamp in correct format, ");
                    }
                }

                if (endDateTime != null)
                {
                    DateTime endDate;

                    if (!DateTime.TryParseExact(endDateTime.ToString(), dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
                    {
                        validationResult.Append("Provide Timestamp in correct format, ");
                    }
                }
                return validationResult;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
