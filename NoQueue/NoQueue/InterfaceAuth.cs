using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoQueue
{
   public interface InterfaceAuth
    {
        Task<string> LoginWithEmailPassword(string email, string password);
    }
}
