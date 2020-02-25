using PwcBios.Api.Data;
using System.Threading;
using System.Threading.Tasks;

namespace PwcBios.Api.CQRS.Queries
{
    public interface IStatusQuery
    {
        Task<bool> GetStatusAsync(CancellationToken cancellationToken);
    }
    public class StatusQuery: IStatusQuery
    {
        private readonly HumanBiosContext _humanBiosContext;

        public StatusQuery(HumanBiosContext humanBiosContext) 
        {
            this._humanBiosContext = humanBiosContext;
        }

        public async Task<bool> GetStatusAsync(CancellationToken cancellationToken)
        {
            var connect = await _humanBiosContext.Database.CanConnectAsync(cancellationToken);
            return connect;
        }
    }
}
