using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KhoaXayDung.Models
{
    [MetadataTypeAttribute(typeof(LoaiTinMetadata))]
    public partial class LoaiTin
    {        
        internal sealed class LoaiTinMetadata
        {
            [Display(Name = "Mã Loại")]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            public int MaLT { get; set; }

            [Display(Name = "Tên Loại")]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này.")]
            public string TenLT { get; set; }

            public Nullable<bool> Show { get; set; }

            [Display(Name = "Mã cha")]            
            public Nullable<int> IDcha { get; set; }
           
        }
    }
}