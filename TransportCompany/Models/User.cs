using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportCompany.Models
{
    public class User : IdentityUser
    {

        public DateTime JoiningDate { get; set; }

        public DateTime BirthDate { get; set; }

        public int Salary {  get; set; }
        public int? BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public virtual Branch Branch { get; set; }

        public List<Message> Messages { get; set; }

    }
}
