using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class doanhthutourdto
    {
        [System.ComponentModel.DisplayName("Tên tour")]
        public string tentour { get; set; }
        [System.ComponentModel.DisplayName("Tên đoàn")]
        public string tendoan { get; set; }
        [System.ComponentModel.DisplayName("Giá tour")]
        public double giatour { get; set; }
        [System.ComponentModel.DisplayName("Tổng thu")]
        public double tongthu { get; set; }
        [System.ComponentModel.DisplayName("Ngày tạo")]
        public Nullable<System.DateTime> ngaytao { get; set; }

        public doanhthutourdto(doandulich entity)
        {
            tendoan = entity.tendoan;
            tentour = entity.tour.tentour;
            giatour = entity.tienve;
            tongthu = entity.tongtientour;
            ngaytao = entity.ngaytao;
        }
    }
}
