using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class diadiemdto
    {
        [System.ComponentModel.DisplayName("Mã địa điểm")]
        public int madiadiem { get; set; }
        [System.ComponentModel.DisplayName("Tên địa điểm")]
        public string tendiadiem { get; set; }
        [System.ComponentModel.DisplayName("Mã tỉnh")]
        public int matinh { get; set; }
        public diadiemdto(diadiem entity)
        {
            madiadiem = entity.id;
            tendiadiem = entity.tendiadiem;
            matinh = entity.idtinh;
        }
    }
}
