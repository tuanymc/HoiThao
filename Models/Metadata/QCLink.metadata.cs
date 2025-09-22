using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KhoaXayDung.Models
{
    [MetadataTypeAttribute(typeof(QCLinkMetadata))]
    public partial class QCLink
    {
        internal sealed class QCLinkMetadata
        {
            [Display(Name = "Mã Link")]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            public int MaQC { get; set; }

            [Display(Name = "Tên Link")]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            public string TenQC { get; set; }
            
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            public string Link { get; set; }

            [Display(Name = "Hình Ảnh")]
            public string H { get; set; }
            [Display(Name = "Hiển thị")]
            [Required(ErrorMessage = "Vui lòng chọn một lựa chọn.")]
            public Nullable<bool> Show { get; set; }
            [Display(Name = "STT")]
            [Required(ErrorMessage = "Vui lòng nhập số thứ tự.")]
            public Nullable<int> STT { get; set; }
            [Display(Name = "Người tạo")]
            public string MaND { get; set; }

            
        }
    }

}