using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Plugins.Responses
{
    [DataContract]
    public class MonitoringResponse
    {
        [DataMember]
        public string SystemDisplayName { get; set; }
        [DataMember]
        public string SystemStatusCode { get; set; }
        [DataMember]
        public string SystemLatency { get; set; }
    }
}
