namespace CodeCoolAPI.DAL.Models
{
    public class Review : BaseEntity
    {
        public string TextBased { get; set; }
        public int DigitBased { get; set; }
        public Material Material { get; set; }
        public int MaterialId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}