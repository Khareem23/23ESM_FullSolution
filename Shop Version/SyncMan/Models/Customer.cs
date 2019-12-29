namespace SyncMan.Core
{
    public class Customer: Sync
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string gender { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }

        public int shopId { get; set; }
       //public Shop shop { get; set; }
    }




}
