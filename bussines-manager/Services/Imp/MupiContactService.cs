using bussines_manager.DbContext;
using bussines_manager.Model;
using Newtonsoft.Json;
using praxi_model.Creatio;
using praxi_model.Mupi;


namespace bussines_manager.Services.Imp
{
    public class MupiContactService : IMupiContactService
    {
        private readonly MupiDbContext _mupiContext;

        public MupiContactService(MupiDbContext mupiContext)
        {
            _mupiContext = mupiContext;
        }
        public List<ContactDto> GetContactsToSync(DateTime from)
        {
            var result= new List<ContactDto>();

            var contacts = _mupiContext.Contact.Where(x => x.ModifyOn >= from).ToList();




            return contacts.Select(x => new ContactDto
            {
                Id = x.Id.ToString(),
                IdCreatio = x.IdCreatio,
                Name = x.Nombre,
                Surname = x.Apellido,
                Email = x.Email,
                JobTitle = x.Tipo,
                UsrSincronizar = false
            }).ToList();
        }


        public List<ContactDto> SyncMupiContactsFromCreatio(List<ContactDto> ContactsCreatio)
        {
            var result = new List<ContactDto>();

            var idsContactsCreatio = ContactsCreatio.Select(x => x.IdCreatio).ToArray();
            //FETCH CONTACTS TO SYNC FROM THE LIST
            var contactsIdMupi = _mupiContext.Contact.Where(x => idsContactsCreatio.Contains(x.IdCreatio)).Select(x => x.IdCreatio).ToList();

            //SYNC CONTACTS ON MUPI DB
            var contactsToAdd = ContactsCreatio.Where(x => !contactsIdMupi.Contains(x.IdCreatio)).ToList();
            var contactsToUpdate = ContactsCreatio.Where(x => contactsIdMupi.Contains(x.IdCreatio)).ToList();

            if (contactsToAdd.Any())
            {
                AddContacts(contactsToAdd);
            }
            if (contactsToUpdate.Any())
            {
                UpdateContacts(contactsToUpdate);
            }
            _mupiContext.SaveChanges();

            result.AddRange(ContactsCreatio);


            //PopulateContacts();
            //_mupiContext.SaveChanges();

            return result;
        }

        private void AddContacts(List<ContactDto> ContactsCreatio)
        {
            foreach (var contact in ContactsCreatio)
            {
                _mupiContext.Contact.Add(new Contact
                {
                    IdCreatio= contact.IdCreatio,
                    Nombre= contact.Name,
                    Apellido = contact.Surname?? " ",
                    Tipo = contact.JobTitle,
                    Email= contact.Email,
                    FechaNacimiento= DateTime.Now.AddYears(-30),
                    ModifyOn= DateTime.Now,
                });
            }
        }
        private void UpdateContacts(List<ContactDto> ContactsCreatio)
        {
            var idsContactsCreatio = ContactsCreatio.Select(x => x.IdCreatio).ToArray();
            var contactsIdMupi = _mupiContext.Contact.Where(x => idsContactsCreatio.Contains(x.IdCreatio)).ToList();

            foreach (var elem in contactsIdMupi)
            {
                var creatioContact = ContactsCreatio.Find(x => x.IdCreatio == elem.IdCreatio);
                elem.Nombre = creatioContact?.Name ?? "";
                elem.Apellido = creatioContact?.Surname ?? " ";
                elem.Tipo = creatioContact?.JobTitle ?? "";
                elem.Email = creatioContact?.Email ?? "";
                elem.ModifyOn = DateTime.Now;
            }

        }


        //private void PopulateContacts()
        //{
        //    for (int i = 0; i < 10001; i++)
        //    {
        //        var typeContact = (i % 3 == 0) ? 3 : (i % 2 == 0) ? 2 : 1;
        //        _mupiContext.Contact.Add(new Contact
        //        {
        //            IdCreatio = "",
        //            Nombre = $"batcher{i}",
        //            Apellido = $"test{i}",
        //            Tipo = $"batcherType{typeContact}",
        //            Email = $"nick{i}@mail.com",
        //            FechaNacimiento = DateTime.Now.AddYears(-30),
        //            ModifyOn = RandomDay()
        //        });
        //    }
        //}

        private Random gen = new Random();
        DateTime RandomDay()
        {
            DateTime start = new DateTime(2020, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }



    }
}
