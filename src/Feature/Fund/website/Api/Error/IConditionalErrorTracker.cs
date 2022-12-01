using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LionTrust.Feature.Fund.Api.Error
{
    public interface IConditionalErrorTracker
    {
        void Error(string message, object owner, bool trySendEmail = true);
        void Success(string message = "", bool trySendEmail = true);
        void TrySendEmail(string message, bool isSuccess = false);
    }
}
