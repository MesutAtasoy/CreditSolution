using User.Contract.Base;

namespace User.Contract.Queries.Users.GetUserByIdentityNumber
{
    public class GetUserByIdentityNumberQuery : IBaseRequest
    {
        public string IdentityNumber { get; set; }
    }
}