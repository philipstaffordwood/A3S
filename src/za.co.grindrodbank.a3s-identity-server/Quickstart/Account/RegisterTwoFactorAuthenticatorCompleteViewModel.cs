using System;
using System.Collections;
using System.Collections.Generic;

namespace za.co.grindrodbank.a3sidentityserver.Quickstart.UI
{
    public class RegisterTwoFactorAuthenticatorCompleteViewModel
    {
        public IEnumerable<string> RecoveryCodes { get; set; }
        public string RedirectUrl { get; set; }
    }
}
