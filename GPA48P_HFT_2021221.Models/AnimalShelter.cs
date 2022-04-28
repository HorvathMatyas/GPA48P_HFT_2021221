using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GPA48P_HFT_2021221.Models
{
    public class AnimalShelter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShelterId { get; set; }

        [Required]
        public string ShelterName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string TaxNumber { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Pet> Pets { get; set; }

        public AnimalShelter()
        {
            Pets = new HashSet<Pet>();
        }
    }
}
