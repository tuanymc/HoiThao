using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhoaXayDung.Models
{
    [MetadataTypeAttribute(typeof(LoaiSlideMetadata))]
    public partial class LoaiSlide
    {
        internal sealed class LoaiSlideMetadata
        {
            [Display(Name="Mã loại slide")]
            public int MaLoaiSlide { get; set; }
            [Display(Name="Tên loại slide")]
            [Required(ErrorMessage="Vui lòng nhập nội dung vào trường này.")]
            public string TenLoaiSlide { get; set; }
        }
    }
}