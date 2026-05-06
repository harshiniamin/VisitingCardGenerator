using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;  // ✅ REQUIRED

namespace Visitingcardgenerator.Models
{
    public class CustomerInfo
    {
        [Key]
        public int CustomerId { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public int BankId { get; set; }
        public int UploadId { get; set; }

        public string Status { get; set; }

        [NotMapped]   // 🔥🔥 THIS LINE FIXES YOUR ERROR
        public string QRCodeImage { get; set; }
    }
}