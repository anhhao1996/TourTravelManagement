using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class khachsandto
    {
        [System.ComponentModel.DisplayName("Mã khách sạn")]
        public int maks { get; set; }
        [System.ComponentModel.DisplayName("Tên khách sạn")]
        public string tenks { get; set; }
        [System.ComponentModel.DisplayName("Mã tỉnh")]
        public int matinh { get; set; }
        public khachsandto(khachsan entity)
        {
            maks = entity.id;
            tenks = entity.tenkhachsan;
            matinh = entity.idtinh;
        }
    }
}
