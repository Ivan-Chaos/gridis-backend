namespace GridisBackend.DTOs.Payment
{
    public class Payment_GET_POST_DTO
    {
        public int Id { get; set; }
        public int InstalledMeterId { get; set; }
        public DateTime PaymentTime { get; set; }
        public decimal ReceiptNumber { get; set; }
        public decimal PaidSum { get; set; }
        public int ResidenceId { get; set; }
    }
}
