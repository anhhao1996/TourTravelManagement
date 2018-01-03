using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class cttqdto
    {
        [System.ComponentModel.DisplayName("Mã địa điểm")]
        public int madiadiem { get; set; }
        [System.ComponentModel.DisplayName("Tên địa điểm")]
        public string tendiadiem { get; set; }
        [System.ComponentModel.DisplayName("Tên tỉnh")]
        public string tentinh { get; set; }
        public cttqdto(ctthamquan entity)
        {
            madiadiem = entity.iddiadiem;
            tendiadiem = entity.diadiem.tendiadiem;
            tentinh = entity.diadiem.tinh.tentinh;
        }
    }
}
