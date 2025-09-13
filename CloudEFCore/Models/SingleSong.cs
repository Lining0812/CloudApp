using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudEFCore.Models
{
    public class SingleSong
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime Time { get; set; }
    }
}
