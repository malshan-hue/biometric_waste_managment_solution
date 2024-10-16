using bwms_core_domain.UserModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bwms_core_domain.SystemModels
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string PasswordSalt { get; set; } = string.Empty;

        public int ActivationCode { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.MinValue;

        public DateTime LastLogginDate { get; set; } = DateTime.MinValue;

        [DefaultValue("false")]
        public bool IsAdmin { get; set; }

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }

        public Guid UserGlobalIdentity { get; set; }

    }
}
