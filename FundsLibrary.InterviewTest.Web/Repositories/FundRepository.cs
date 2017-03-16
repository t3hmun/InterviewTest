using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FundsLibrary.InterviewTest.Common;

namespace FundsLibrary.InterviewTest.Web.Repositories
{
    public class FundRepository : IFundRepository
    {
        private readonly IOdataClientWrapper _client;

        public FundRepository(IOdataClientWrapper client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Fund>> GetFunds(Guid managerGuid)
        {
            return await _client.GetFundsForManager(managerGuid);
        }
    }

    public interface IFundRepository
    {
        Task<IEnumerable<Fund>> GetFunds(Guid managerGuid);
    }
}