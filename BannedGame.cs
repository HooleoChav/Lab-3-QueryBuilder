using QueryBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryBuilder.Models
{
    public class BannedGame : IClassModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Series { get; set; }
        public string Country { get; set; }
        public string Details { get; set; }
    }
}
