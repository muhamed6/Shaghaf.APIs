﻿namespace Shaghaf.Core.Dtos
{
    public class CategoryDto
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public List<int> SelectedCategories { get; set; } = default!;
    }
}