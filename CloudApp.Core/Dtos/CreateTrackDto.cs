using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApp.Core.Dtos
{
    public class CreateTrackDto
    {
        public string Title { get; set; }
        public int AlbumId { get; set; }
    }
}
