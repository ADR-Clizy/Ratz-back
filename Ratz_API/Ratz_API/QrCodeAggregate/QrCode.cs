using System.ComponentModel.DataAnnotations;

namespace DatabaseConnection
{
    public class QrCode
    {
        [Key]
        public int QrCodeId { get; set; }

        [Required]
        public string url { get; set; }
        public string EncodedUrl { get; set; }
        public string InformationFormat { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }


    }
}
