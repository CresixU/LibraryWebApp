﻿namespace LibraryAPI.Models
{
    public class LibraryQuery
    {
        public string SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
