﻿using Domain.Common;

namespace Domain.Entities
{
    public class Category:EntityBase<Guid>
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public ICollection<AccountCategory> AccountCategories { get; set; }

        public ICollection<NoteCategory> NoteCategories { get; set; }

        
    }
}
