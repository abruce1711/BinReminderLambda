using BinReminder.Models.BinModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinReminder.Interfaces
{
    public interface IWhatsAppService
    {
        public void SendWhatsApp(List<Bin> bins, string to);
        public void SendWhatsApp(string message, string to);
    }
}
