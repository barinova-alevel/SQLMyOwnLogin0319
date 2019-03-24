using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyOwnLogin.Core.Models
{
    public class Contact
    {
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z]+$")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z]+$")]
        public string LastName { get; set; }

        [UIHint("PhoneNumbers")]
        public List<string> PhoneNumbers { get; set; }
    }
}