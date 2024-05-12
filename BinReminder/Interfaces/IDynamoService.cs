using BinReminder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinReminder.Interfaces
{
    public interface IDynamoService
    {
       Task<IEnumerable<User>> GetAllUsers();
    }
}
