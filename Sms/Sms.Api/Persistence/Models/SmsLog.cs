using Framework.EntityFrameworkCore.Models;

namespace Sms.Api.Persistence.Models
{
    public class SmsLog : BaseEntityModel
    {
        public string MessageType { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public string IsSend { get; set; }
    }
}