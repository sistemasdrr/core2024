using DRRCore.Transversal.Common.Interface;

namespace DRRCore.Transversal.Common
{
    public class MailFormatter : IMailFormatter
    {
        public async Task<string> GetEmailBody(string emailKey,string body,List<string> parameters, List<List<string>> table)
        {
           switch (emailKey)
            {
                case Constants.DRR_SYS_ENG_0003:
                case Constants.DRR_SYS_0001:
                case Constants.DRR_SYS_0002:
                case Constants.DRR_SYS_ESP_0003:
                case Constants.DRR_WORKFLOW_ENG_0023:
                case Constants.DRR_WORKFLOW_ENG_0032:
                case Constants.DRR_WORKFLOW_ENG_0039:
                case Constants.DRR_WORKFLOW_ENG_0039_ERROR:
                    return GetHtmlStringBody(body,parameters);
                case Constants.DRR_WORKFLOW_ENG_0001:
                case Constants.DRR_WORKFLOW_ESP_0001:
                case Constants.DRR_WORKFLOW_ESP_0003:
                    return GetHtmlStringBodyWithTable_DRR_WORKFLOW_ESP_ENG_0001(body,parameters,table);
                case Constants.DRR_WORKFLOW_ENG_0002:
                case Constants.DRR_WORKFLOW_ESP_0002:
                    return GetHtmlStringBodyWithTable_DRR_WORKFLOW_ESP_ENG_0002(body, parameters, table);
                case Constants.DRR_WORKFLOW_ENG_0020:
                    return GetHtmlStringBodyWithTable_DRR_WORKFLOW_ENG_0020(body, parameters, table);
                default:
                    return body;

            }
        }

        private string GetHtmlStringBody(string body, List<string> parameters)
        {
            return string.Format(body,parameters);
        }       
        private string GetHtmlStringBodyWithTable_DRR_WORKFLOW_ESP_ENG_0001(string body, List<string> parameters, List<List<string>> table)
        {
            body = string.Format(body, parameters);
            
            string tableString=string.Empty;
            foreach (var tableItem in table)
            {
                var rowString = string.Empty;
                rowString = "<tr>" +
                    "<td style='padding:5px;Margin:0;font-size:13px;'> {0}</td>+" +
                    "<td style='padding:5px;Margin:0;font-size:13px;text-align: center;'>{1}</td>" +
                    "<td style='padding:5px;Margin:0;font-size:13px;text-align: center;'>{2}</td>" +
                    "<td style='padding:5px;Margin:0;font-size:13px;text-align: center;'>{3}</td>" +
                    "<td style='padding:5px;Margin:0;font-size:13px;text-align: center;'>{4}</td>" +
                    "<td style='padding:5px;Margin:0;font-size:13px;text-align: center;'>{5}</td>" +
                    "</tr>";
                rowString=string.Format(rowString,tableItem);
                tableString += rowString;
            }
            return body.Replace(Constants.TABLEBODY, tableString);
           
        }
        private string GetHtmlStringBodyWithTable_DRR_WORKFLOW_ESP_ENG_0002(string body, List<string> parameters, List<List<string>> table)
        {
            body = string.Format(body, parameters);

            string tableString = string.Empty;
            foreach (var tableItem in table)
            {
                var rowString = string.Empty;
                rowString = "<tr>" +
                     "<td style='padding:5px;Margin:0;font-size:13px;text-align: center;'>{0}</td>" +
                    "<td style='padding:5px;Margin:0;font-size:13px;'> {1}</td>+" +
                    "<td style='padding:5px;Margin:0;font-size:13px;text-align: center;'>{2}</td>" +
                    "<td style='padding:5px;Margin:0;font-size:13px;text-align: center;'>{3}</td>" +
                    "<td style='padding:5px;Margin:0;font-size:13px;text-align: center;'>{4}</td>" +
                    "<td style='padding:5px;Margin:0;font-size:13px;text-align: center;'>{5}</td>" +
                    "</tr>";
                rowString = string.Format(rowString, tableItem);
                tableString += rowString;
            }
            return body.Replace(Constants.TABLEBODY, tableString);

        }
        private string GetHtmlStringBodyWithTable_DRR_WORKFLOW_ENG_0020(string body, List<string> parameters, List<List<string>> table)
        {
            body = string.Format(body, parameters);

            string tableString = string.Empty;
            foreach (var tableItem in table)
            {
                var rowString = string.Empty;
                rowString = "<tr>" +                    
                    "<td style='padding:5px;Margin:0;font-size:13px;text-align: center;'>{0}</td>" +
                    "<td style='padding:5px;Margin:0;font-size:13px;text-align: center;'>{1}</td>" +
                    "<td style='padding:5px;Margin:0;font-size:13px;text-align: center;'>{2}</td>" +
                    "<td style='padding:5px;Margin:0;font-size:13px;text-align: center;'>{3}</td>" +
                    "</tr>";
                rowString = string.Format(rowString, tableItem);
                tableString += rowString;
            }
            return body.Replace(Constants.TABLEBODY, tableString);

        }
    }
}
