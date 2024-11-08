using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _23dh112072_MyStore.Models.ViewModel
{
    public class RegisterVM
    {
        [Required]
        [Display(Name="Tên đăng nhập")]
        public string Username {  get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Mật khẩu")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khâu và xác nhận không trùng khớp")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name="Họ tên")]
        public string CustomerName {  get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Số điện thoại")]
        public string CustomerPhone { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string CustomerEmail { get; set; }

        [Required]
        [Display(Name = "Địa chỉ")]
        public string CustomerAddress { get; set; }
    }
}