using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class suacttqdto
    {
        [System.ComponentModel.DisplayName("STT")]
        public int stt { get; set; }
        [System.ComponentModel.DisplayName("Mã tour")]
        public int matour { get; set; }
        [System.ComponentModel.DisplayName("Tên địa điểm")]
        public string tendiadiem { get; set; }
        public suacttqdto(ctthamquan entity)
        {
            stt = 0;
            matour = entity.idtour;
            tendiadiem = entity.diadiem.tendiadiem;
        }
    }
}
