using kennel_bambino.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kennel_bambino.web.ViewModels
{
    public class PhotoPagingViewModel
    {
        public List<Photo> Photos { get; set; }
        public int PageCount { get; set; }
        public int PageNumber { get; set; }
    }
}
