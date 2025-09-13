using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudEFCore.Models
{
    public class Album
    {
        public int ID { get; set; }
        public List<SingleSong> Songs { get; set; }
    }
}
