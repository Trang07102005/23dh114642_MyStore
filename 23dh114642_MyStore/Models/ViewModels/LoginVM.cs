﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace _23dh114642_MyStore.Models.ViewModels
{
    public class LoginVM
    {
        [Required]
        [Display(Name= "Tên đăng nhập")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name= "Mật khẩu")]
        public string Password { get; set; }
    }
}