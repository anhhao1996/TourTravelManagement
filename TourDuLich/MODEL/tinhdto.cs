using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class tinhdto
    {
        [System.ComponentModel.DisplayName("Mã tỉnh")]
        public int matinh { get; set; }
        [System.ComponentModel.DisplayName("Tên tỉnh")]
        public string tentinh { get; set; }
        public tinhdto(tinh entity)
        {
            matinh = entity.id;
            tentinh = entity.tentinh;
        }
    }
}
