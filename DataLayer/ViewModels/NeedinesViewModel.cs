using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class NeedinesViewModel
    {
        
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string LastName { get; set; }

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string Phone { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string Address { get; set; }

        [Display(Name = "جنسیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public Nullable<int> Sex { get; set; }

        [Display(Name = "شغل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string Job { get; set; }

        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public Nullable<System.DateTime> BirthDate { get; set; }

        [Display(Name = "عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string Pic { get; set; }

        [Display(Name = "میانگین درآمد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string Income { get; set; }

        [Display(Name = "تحصیلات")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public Nullable<int> Education { get; set; }
    }
}
