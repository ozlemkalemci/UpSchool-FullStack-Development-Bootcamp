﻿using System.ComponentModel.DataAnnotations;

namespace UpSchool.Domain.Dtos
{
    public class AccountEditDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} field is required.")]
        public string Title { get; set; }

        [Required]
        [MaxLength(40)]
        [MinLength(2)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(40)]
        [MinLength(6)]
        public string Password { get; set; }

        public string? Url { get; set; }

        [Required]
        public bool IsFavourite { get; set; }
    }
}
