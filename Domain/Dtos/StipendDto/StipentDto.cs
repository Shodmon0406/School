using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.StipendDto
{
    public class StipentDto
    {
        public string StudenId { get; set; } = null!;
        public StipendType StipendType { get; set; }
        public string Description { get; set; } = null!;
    }
}
