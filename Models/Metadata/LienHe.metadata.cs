using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using 2 thư viện thiết kế class metadata
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KhoaXayDung.Models
{
    [MetadataTypeAttribute(typeof(LienHeMetadata))]
    public partial class LienHe
    {        
        internal sealed class LienHeMetadata
        {
            [Display(Name = "Mã Liên Hệ")]
            public int MaLH { get; set; }

            [Display(Name = "Họ Tên")]//Thuộc tính Display dùng để đặt tên lại cho cột
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")] //Kiểm tra rổng
            public string Ten { get; set; }

            [Display(Name = "Nội Dung")]//Thuộc tính Display dùng để đặt tên lại cho cột
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")] //Kiểm tra rổng
            public string ND { get; set; }

            [Display(Name = "Địa Chỉ")]//Thuộc tính Display dùng để đặt tên lại cho cột
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")] //Kiểm tra rổng
            public string DC { get; set; }

            [Display(Name = "Điện Thoại")]//Thuộc tính Display dùng để đặt tên lại cho cột
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")] //Kiểm tra rổng            
            public string Phone { get; set; }

            [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email không đúng")]
            [Required(ErrorMessage = "Email không được để trống")]
            public string Email { get; set; }

            [Display(Name = "Ngày Đăng")]
            public Nullable<System.DateTime> NgayDang { get; set; }
            public Nullable<bool> Show { get; set; }

            [Display(Name = "Trạng Thái Xem")]
            public Nullable<bool> TrangThaiXem { get; set; }
        }
    }
}