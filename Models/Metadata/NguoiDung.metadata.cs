using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KhoaXayDung.Models
{
    [MetadataTypeAttribute(typeof(NguoiDungMetadata))]
    public partial class NguoiDung
    {
        internal sealed class NguoiDungMetadata
        {
            [Display(Name = "Mã ND")]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            public string MaND { get; set; }

            [Display(Name = "Họ Tên")]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            public string HT { get; set; }

            [Display(Name = "Mật khẩu")]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            public string MatKhau { get; set; }
            public string Email { get; set; }

            [Display(Name = "Giới Tính")]
            public Nullable<bool> GT { get; set; }

            [Display(Name = "Điện Thoại")]
            public string DT { get; set; }

            [Display(Name = "Địa Chỉ")]
            public string DC { get; set; }
            public Nullable<System.DateTime> NS { get; set; }

            [Display(Name = "Quyền")]
            public string Quyen { get; set; }
            public Nullable<bool> Show { get; set; }

        }
    }

}