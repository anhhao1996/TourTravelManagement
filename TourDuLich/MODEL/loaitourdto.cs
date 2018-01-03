using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class loaitourdto
    {
        [System.ComponentModel.DisplayName("Mã loại")]
        public int maloai { get; set; }
        [System.ComponentModel.DisplayName("Tên loại tour")]
        public string tenloaitour { get; set; }
        public loaitourdto(loaitour entity)
        {
            maloai = entity.id;
            tenloaitour = entity.tenloaitour;
        }
    }
}
