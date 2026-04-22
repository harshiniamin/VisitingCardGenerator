using System.ComponentModel.DataAnnotations;

public class Banks
{
    [Key]   // ⭐ IMPORTANT
    public int BankId { get; set; }

    public string? BankName { get; set; }

    //public string BankName { get; set; }

    // Allow database NULLs for WebsiteURL to avoid SqlNullValueException during materialization
    public string? WebsiteURL { get; set; }
}