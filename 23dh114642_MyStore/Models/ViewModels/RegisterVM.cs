﻿using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace _23dh114642_MyStore.Models.ViewModels
{
    public class RegisterVM
    {
        [Required]

        [Display(Name = "Tên đăng nhập")] 
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]

        [Display(Name = "Xác nhận mặt khẩu")] 

        [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp. ")] 
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Họ tên")]
        public string CustomerName { get; set; }

        [Required]
        [Display(Name = "Số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        public string CustomerPhone { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)] 
        public string CustomerEmail { get; set; }

        [Required]
        [Display(Name = "Địa chỉ")]
        public string CustomerAddress { get; set; }
    }
}