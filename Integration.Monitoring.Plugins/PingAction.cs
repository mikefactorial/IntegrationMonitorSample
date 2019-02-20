using Integration.Monitoring.Plugins.Responses;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Integration.Monitoring.Plugins
{
    public class PingAction : CodeActivity
    {
        [Output("Response")]
        public OutArgument<string> Response
        {
            get;
            set;
        }
        [Input("Request")]
        public InArgument<string> Request
        {
            get;
            set;
        }
        protected override void Execute(CodeActivityContext context)
        {
            // Getting OrganizationService from Context  
            var workflowContext = context.GetExtension<IWorkflowContext>();
            var serviceFactory = context.GetExtension<IOrganizationServiceFactory>();
            var orgService = serviceFactory.CreateOrganizationService(workflowContext.UserId);

            List<MonitoringResponse> responses = new List<MonitoringResponse>();
            MonitoringResponse response1 = new MonitoringResponse
            {
                SystemDisplayName = "System 1",
                SystemLatency = "10 ms",
                SystemStatusCode = "200"
            };
            responses.Add(response1);

            MonitoringResponse response2 = new MonitoringResponse
            {
                SystemDisplayName = "System 2",
                SystemLatency = "5000 ms",
                SystemStatusCode = "500"
            };
            responses.Add(response2);

            MonitoringResponse[] responseArray = responses.ToArray();
            using (MemoryStream stream = new MemoryStream())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(responseArray.GetType());
                serializer.WriteObject(stream, responseArray);

                string result = Encoding.Default.GetString(stream.ToArray());
                //get JSON data serialized in string format in string variable 
                Response.Set(context, result);
            }

        }
    }
}
