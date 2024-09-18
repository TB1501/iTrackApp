using static iTrack.Models.Client;
using System.ComponentModel.DataAnnotations;

namespace iTrack.Models.DTOs
{
	public class AddClientForm
	{
		

		[Required]
		public string Name { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string ImgUrl { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime Enrolment { get; set; }

		[Required]
		public decimal ServicePrice { get; set; }

		[Required]
		public ClientStatus Status { get; set; }

		[Required]
		public decimal Debt { get; set; }
	}
}
