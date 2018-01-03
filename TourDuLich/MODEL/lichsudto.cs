using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class lichsudto
    {
        [System.ComponentModel.DisplayName("Mã tour")]
        public int matour { get; set; }
        [System.ComponentModel.DisplayName("Tên tour")]
        public string tentour { get; set; }
        [System.ComponentModel.DisplayName("Giá tour")]
        public double giatour { get; set; }
        [System.ComponentModel.DisplayName("Ngày bắt đầu")]
        public Nullable<System.DateTime> ngaybatdau { get; set; }
        [System.ComponentModel.DisplayName("Ngày kết thúc")]
        public Nullable<System.DateTime> ngayketthuc { get; set; }
        public lichsudto(lichsugiatour entity)
        {
            matour = entity.idtour;
            tentour = entity.tour.tentour;
            giatour = entity.giatour;
            ngaybatdau = entity.ngaybatdau;
            ngayketthuc = entity.ngayketthuc;
        }
    }
}
