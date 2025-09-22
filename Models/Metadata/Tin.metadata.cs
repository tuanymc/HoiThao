using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KhoaXayDung.Models
{
    [MetadataTypeAttribute(typeof(TinMetadata))]
    public partial class Tin
    {        
        internal sealed class TinMetadata
        {
            [Display(Name = "Mã Tin")]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            public int MaT { get; set; }

            [Display(Name = "Tên Tin")]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            public string TenT { get; set; }

            [Display(Name = "Mô Tả")]
            [DataType(DataType.MultilineText)]
            public string MT { get; set; }

            [Display(Name = "Nội Dung")]            
            public string ND { get; set; }

            [Display(Name = "Ảnh Đại Diện")]
            public string H { get; set; }

            [Display(Name = "File")]
            public string F { get; set; }
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
            [Display(Name = "Ngày Đăng")]
            public Nullable<System.DateTime> NgayDang { get; set; }
            public Nullable<int> Xem { get; set; }

            [Display(Name = "Mã Loại Tin")]
            public Nullable<int> MaLT { get; set; }
            public string Link { get; set; }
            public Nullable<bool> Show { get; set; }

            [Display(Name = "Mã Người Dùng")]
            public string MaND { get; set; }

            
        }
    }
}