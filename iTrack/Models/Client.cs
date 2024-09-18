using System.ComponentModel.DataAnnotations;

namespace iTrack.Models;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string ImgUrl { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime Enrolment { get; set; }
    public decimal ServicePrice { get; set; }
    public ClientStatus Status { get; set; }

    public decimal Debt { get; set; }

    public enum ClientStatus
    {
        [Display(Name = "Active")]
        Active,

        [Display(Name = "Inactive")]
        Inactive
    }


}
