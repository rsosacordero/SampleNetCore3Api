using Microsoft.Extensions.Diagnostics.HealthChecks;
using PwcBios.Api.CQRS.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace PwcBios.Api.Infrastructure.HealthChecks
{
    public class BasicHealthCheck : IHealthCheck
    {
        private readonly IStatusQuery _statusQuery;
        public BasicHealthCheck(IStatusQuery statusQuery)
        {
            _statusQuery = statusQuery;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (!await _statusQuery.GetStatusAsync(cancellationToken))
            {
                return new HealthCheckResult(HealthStatus.Unhealthy, "Database can't connect");
            }


            return new HealthCheckResult(HealthStatus.Healthy, "Hello World");
        }
    }
}
