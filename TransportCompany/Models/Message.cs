using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransportCompany.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string MessageInfo { get; set; } = string.Empty;
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
