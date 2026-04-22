using System.ComponentModel.DataAnnotations;

namespace Visitingcardgenerator.Models
{
    public class UploadHistory
    {
        [Key]
        public int Id { get; set; }   // PRIMARY KEY

        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }
        public string BankName { get; set; }
        public int TotalRecords { get; set; }
        public string Status { get; set; }  // New / Processed
    }
}