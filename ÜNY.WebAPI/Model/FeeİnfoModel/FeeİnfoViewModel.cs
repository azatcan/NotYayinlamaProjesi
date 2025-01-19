namespace ÜNY.WebAPI.Model.FeeİnfoModel
{
    public class FeeİnfoViewModel
    {
        public int YourReferenceNumber { get; set; }
        public decimal YourCurrentDebt { get; set; }
        public DateTime period { get; set; }
        public string DebtType { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public Guid UserId { get; set; }
    }
}
