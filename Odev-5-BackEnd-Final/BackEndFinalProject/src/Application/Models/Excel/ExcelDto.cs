using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Excel
{
    public class ExcelDto
    {
        public bool Download { get; set; }


        public ExcelDto()
        {
            Download = true;
        }
    }
}
