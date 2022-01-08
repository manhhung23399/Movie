using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.Core
{
    public class AppSettings
    {
        public string? AuthSecret { get; set; }
        public string? ApiKey { get; set; }
        public string? DatabaseURL { get; set; }
        public string? AuthDomain { get; set; }
        public string? ProjectId { get; set; }
        public string? StorageBucket { get; set; }
        public string? MessageSenderId { get; set; }
        public string? AppId { get; set; }
        public string? MeasurementId { get; set; }
        public string? RefreshToken { get; set; }
        public string? JwtAuthFirebase { get; set; }
    }
}
