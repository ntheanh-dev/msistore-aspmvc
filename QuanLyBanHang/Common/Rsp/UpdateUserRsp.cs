using System.Xml.Linq;

namespace Common.Rsp
{
    public class UpdateUserRsp
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? HomeNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Avatar { get; set; }

        public List<string> FieldsChanged { get; set; } = new List<string>();
    }
}
