using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UsersMetaData
    {
        [Key]
        [Display(Name = "کد کاربر")]
        public int UserID { get; set; }

        [Display(Name = "کد نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int RoleID { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Email { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }

        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Phone { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public bool IsActive { get; set; }

        [Display(Name = "کد فعالسازی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ActiveCode { get; set; }

        [Display(Name = "تاریخ و زمان ثبت نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public System.DateTime RegisterDate { get; set; }

        [Display(Name = "لاگین ناموفق")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int FailedLogin { get; set; }
    }
    [MetadataType(typeof(UsersMetaData))]
    public partial class Users
    {

    }
}
