/**
 * *************************************************
 * Copyright (c) 2019, Grindrod Bank Limited
 * License MIT: https://opensource.org/licenses/MIT
 * **************************************************
 */
﻿using System;
using System.Collections.Generic;

namespace za.co.grindrodbank.a3s.Models
{
    public class LdapAuthenticationModeModel : AuditableModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public bool IsLdaps { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string BaseDn { get; set; }

        public List<UserModel> Users { get; set; }
        public List<LdapAuthenticationModeLdapAttributeModel> LdapAttributes { get; set; }
    }
}
