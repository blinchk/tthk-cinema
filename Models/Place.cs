using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tthk_kinoteater.Models
{
    class Place
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int Row { get; set; }
        public Hall Hall { get; set; }
    }
}
