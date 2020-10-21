using User.Contract.Models.Base;

namespace User.Contract.Models.User
{
    public class UserViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string IdentificationNumber { get; set; }
    }
}