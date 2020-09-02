using System.ComponentModel.DataAnnotations;

namespace WashWorldParking.MDL
{
    public enum WashEnum
    {
        [Display(Name ="Bronze Wash")] Bronze_Wash,
        [Display(Name = "Silver wash")] Silver_Wash,
        [Display(Name = "Golden Shower")] Golden_Shower
    }
}
