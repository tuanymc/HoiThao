using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhoaXayDung.Models
{
    [MetadataTypeAttribute(typeof(SlideMetadata))]
    public partial class Slide
    {
        internal sealed class SlideMetadata
        {
            [Display(Name="Mã slide")]
            public int MaSlide { get; set; }
            [Display(Name="Loại slide")]
            public int MaLoai { get; set; }
            [Display(Name="Tiêu đề slide")]
            public string TenSlide { get; set; }
            [Display(Name="Ảnh slide")]
            public string AnhSlide { get; set; }
            [Display(Name="Link slide")]
            public string LinkSide { get; set; }
            [Display(Name="Mô tả slide")]
            public string MoTaSlide { get; set; }
            [Display(Name="Hiển thị")]
            public bool Show { get; set; }
        }
    }
}