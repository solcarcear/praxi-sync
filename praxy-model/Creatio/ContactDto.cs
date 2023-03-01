namespace praxi_model.Creatio
{
    public class ContactDto
    {
        public string Id { get; set; }
        public string IdCreatio { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string JobTitle { get; set; }
        public bool UsrSincronizar { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
}