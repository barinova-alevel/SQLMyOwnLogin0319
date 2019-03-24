using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using PhoneBookApi.Repositories;

namespace PhoneBookApi.Models
{
    public class Contact : IEntity
    {
        public Guid Id { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z]+$")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression("^[A-Za-z]+$")]
        public string LastName { get; set; }

        public List<string> PhoneNumbers { get; set; }

        public Guid GroupId { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public string FullName => $"{FirstName} {LastName}";
    }
}