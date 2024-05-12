using BinReminder.Models.BinModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinReminder.Interfaces
{
    public interface ICouncilService
    {
        public Task<string> GetAuthKey();
        public Task<IEnumerable<Bin>> GetThisWeeksBins(string authKey, string requestBody);
        public Task<string> GetPropertyUprn(string authKey, string postCode, string houseNumber);
    }
}
