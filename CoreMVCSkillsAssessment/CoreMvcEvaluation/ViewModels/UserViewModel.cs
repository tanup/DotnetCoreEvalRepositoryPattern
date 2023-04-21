﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using CoreMvcEvaluation.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoreMvcEvaluation.ViewModels
{
    public class UserViewModel
    {
        private UserService svcUser = new UserService();
                
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(125)]
        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
            ErrorMessage = "The Email Address field must be a valid format")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Display(Name = "Company Name")]
        public String CompanyName { get; set; }

        [Display(Name = "User Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Last Login Date/Time")]
        public DateTime? LastLogin { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Created By")]
        public int CreatedBy { get; set; }
       

        public IEnumerable<SelectListItem> EmpTypes { get; set; }
        [Required(ErrorMessage = "You must select an Employee Type")]
        public string EmpTypeSelected { get; set; }
        
        [Display(Name = "Employee Type")]
        public string EmpTypeName { get; set; }

        public UserViewModel()
        {
            EmpTypes = svcUser.getEmployeeTypeList();
        }
        public UserViewModel(Domain.Entities.User user)
        {
            EmpTypes = svcUser.getEmployeeTypeList();
            if (user.EmpType != null)
            {
                this.EmpTypeSelected = user.EmpType.Id.ToString();
                this.EmpTypeName = user.EmpType.Name;
            }
            this.Id = user.Id;
            this.Email = user.Email;
            this.LastName = user.LastName;
            this.FirstName = user.FirstName;
            this.IsActive = user.IsActive;
            this.CompanyName = user.CompanyName;
            this.LastLogin = user.LastLogin;
            this.CreatedBy = user.CreatedBy;
            this.CreatedDate = user.CreatedDate;
        }

        public UserViewModel(User user)
        {
            EmpTypes = svcUser.getEmployeeTypeList();
            if (user.EmpType != null)
            {
                this.EmpTypeSelected = user.EmpType.Id.ToString();
                this.EmpTypeName = user.EmpType.Name;
            }
            this.Id = user.Id;
            this.Email = user.Email;
            this.LastName = user.LastName;
            this.FirstName = user.FirstName;
            this.IsActive = user.IsActive;
            this.CompanyName = user.CompanyName;
            this.LastLogin = user.LastLogin;
            this.CreatedBy = user.CreatedBy;
            this.CreatedDate = user.CreatedDate;
        }

        public static implicit operator UserViewModel(Domain.Entities.User v)
        {
            throw new NotImplementedException();
        }
    }

}