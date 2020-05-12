using CrossTech.Domain.Enumerations;
using CrossTech.Domain.ValueObjects;
using System;

namespace CrossTech.Domain.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public ProfileGender Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public Phone Phone { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public string FullName
        {
            get
            {
                return $"{LastName} {FirstName} {MiddleName}";
            }
        }
    }
}
