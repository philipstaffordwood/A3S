/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
namespace za.co.grindrodbank.a3s.Models
{
    public class LdapAuthenticationModeLdapAttributeModel : AuditableModel
    {
        public int Id { get; set; }
        public string UserField { get; set; }
        public string LdapField { get; set; }

        public Guid LdapAuthenticationModeId { get; set; }
        public LdapAuthenticationModeModel LdapAuthenticationMode { get; set; }
    }
}
