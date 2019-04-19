using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using System.ComponentModel.DataAnnotations;

namespace CustomerWebService.Models
{
    /// <summary>
    /// Main class for our HTTP CRUD requests, all parameters except Id are required
    /// getters, setters and error messages are defined
    /// </summary>
    public class Customer
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name must be set")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Surrname must be set")]
        public string SurrName { get; set; }

        [Required(ErrorMessage = "Address must be set")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Contact type must be set")]
        public ContactType contactType { get; set; }

    }
}
