using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class tourdto
    {

        [System.ComponentModel.DisplayName("Mã tour")]
        public int matour { get; set; }
        [System.ComponentModel.DisplayName("Tên tour")]
        public string tentour { get; set; }
        [System.ComponentModel.DisplayName("Giá tour")]
        public double? giatour { get; set; }
        [System.ComponentModel.DisplayName("Loại tour")]
        public string loaitour { get; set; }
        [System.ComponentModel.DisplayName("Đặc điểm")]
        public string dacdiem { get; set; }
        [System.ComponentModel.DisplayName("Ngày tạo")]
        public Nullable<System.DateTime> ngaytao { get; set; }
        [System.ComponentModel.DisplayName("Ngày cập nhật")]
        public Nullable<System.DateTime> ngaycapnhat { get; set; }
        public tourdto(tour entity)
        {
            matour = entity.id;
            tentour = entity.tentour;
            giatour = entity.giatour;
            loaitour = entity.loaitour.tenloaitour;
            dacdiem = entity.dacdiem;
            ngaytao = entity.ngaytao;
            ngaycapnhat = entity.ngaycapnhat;
        }
    }
}
