using System.ComponentModel.DataAnnotations;
namespace TransportCompany.Dto_s.Branches
{
    public class CreateBranchDTO
    {
		[Required]
		public string Code { get; set; } = string.Empty;
		[Required]
		[MinLength(4)]
		public string Name { get; set; } = string.Empty;

		[Required]
		[MinLength(2)]

		public string City { get; set; } = string.Empty;

		[Required]
		[MinLength(10)]
		[MaxLength(10)]
		public string Telephone { get; set; } = string.Empty;
	}
}
