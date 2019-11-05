using System.Collections.Generic;

namespace za.co.grindrodbank.a3sidentityserver.Quickstart.UI
{
    public class ResetRecoveryCodesModel
    {
        public string RedirectUrl { get; set; }
        public IEnumerable<string> RecoveryCodes { get; set; }
    }
}
