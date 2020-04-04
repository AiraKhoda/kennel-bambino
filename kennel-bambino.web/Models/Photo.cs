﻿using System.ComponentModel.DataAnnotations;

namespace kennel_bambino.web.Models
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }

        [Required]
        [StringLength(75)]
        public string PhotoName { get; set; }

        [Required]
        public int PetId { get; set; }

        #region Relations - navigation Properties
        public Pet Pet { get; set; }
        #endregion
    }
}
