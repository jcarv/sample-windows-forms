namespace Mpc.WinFormsIoC.Domain.Models
{
    using System.Collections.Generic;

    public class CountryModel
    {
        public string Alias { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<UserModel> Users { get; set; }
    }
}
