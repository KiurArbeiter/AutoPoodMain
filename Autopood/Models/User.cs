namespace Autopood.Models
{
    public class User
    {
        public int userId {  get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime SignupAt { get; set; }
        public bool IsDisabled { get; set; }
        public string DisableReason { get; set; }
        public ICollection<Purchases> PurchasesDone { get; set; }

        //

        public DateTime CreatedAt { get; set; }
        public DateTime Modified { get; set;}
    }
}
