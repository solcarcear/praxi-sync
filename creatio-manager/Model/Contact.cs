namespace creatio_manager.Model
{
    public class Contact
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string UsrIDContacto { get; set; }
        public bool UsrSincronizar { get; set; }



    }
}