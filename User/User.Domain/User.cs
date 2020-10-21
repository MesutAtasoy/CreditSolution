using System;
using Framework.EntityFrameworkCore.Models;

namespace User.Domain
{
    public partial class User : BaseEntityModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentificationNumber { get; set; }
    }
}