using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Visitingcardgenerator.Models
{
    public class Upload
    {
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        public DateTime UploadDate { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public string Status { get; set; } // New / Processed

        public CustomerInfo Customer { get; set; }
    }
}