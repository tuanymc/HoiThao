using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace KhoaXayDung.Controllers
{
    public class Cus
    {
        public static string GetSHA384Hash(string input)
        {
            System.Security.Cryptography.SHA384CryptoServiceProvider x = new System.Security.Cryptography.SHA384CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string password = s.ToString();
            return password;
        }
        public static string DaoNguocChuoi(string str)
        {
            // tạo StringBuilder
            System.Text.StringBuilder reverseStr = new System.Text.StringBuilder(str.Length);
            //Duyệt ngược chuỗi nguồn
            for (int count = str.Length - 1; count >= 0; count--)
                reverseStr.Append(str[count]); // thêm từng kí tự vào StringBuilder
            //Đảo chuỗi
            return reverseStr.ToString();
        }
        public static string MaHoa(string input)
        {
            string a = GetSHA384Hash(input);
            a = DaoNguocChuoi(a);
            a = GetSHA384Hash(a);
            return a;
        }
        private static readonly string[] VietnameseSigns = new string[]
        {
            "aAeEoOuUiIdDyY--",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ",
            "~`!@#$%^&*()_+=[]{}|';:?/><., ",
            @"\"
        };
        public static string ChuyenKoDau(string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }
        public static string RewriteUrl(string text)
        {
            //url = ChuyenKoDau(url.Trim());
            //bool flag = true;
            //while (flag)
            //{
            //    url = url.Replace("-----", "-");
            //    url = url.Replace("----", "-");
            //    url = url.Replace("---", "-");
            //    url = url.Replace("--", "-");
            //    url = url.Replace(@"""", "");
            //    url = url.Replace(@"”", "");
            //    url = url.Replace(@"“", "");
            //    url = url.Replace("–––––", "-");
            //    url = url.Replace("––––", "-");
            //    url = url.Replace("–––", "-");
            //    url = url.Replace("––", "-");
            //    url = url.Replace("–", "-");
            //    if (url.LastOrDefault() == '-')
            //    {
            //        url = url.Remove(url.Length - 1, 1);
            //    }
            //    if (!url.Contains("--") && !url.Contains("––"))
            //    {
            //        flag = false;
            //    }
            //}
            //return url.ToLower();

            for (int i = 32; i < 48; i++)
            {

                text = text.Replace(((char)i).ToString(), " ");

            }
            text = text.Replace(".", "-");

            text = text.Replace(" ", "-");

            text = text.Replace(",", "-");

            text = text.Replace(";", "-");

            text = text.Replace(":", "-");

            text = text.Replace(@"”", "-");

            text = text.Replace(@"“", "-");

            text = text.Replace(@"""", "-");

            text = text.Replace(@"--", "-");

            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);

            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static int pageCount(string filePath)
        {
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                StreamReader r = new StreamReader(fs);
                string pdfText = r.ReadToEnd();
                Regex rx1 = new Regex(@"/Type\s*/Page[^s]");
                MatchCollection matches = rx1.Matches(pdfText);
                return matches.Count;
            }
            catch
            {
                return 1;
            }
        }

        public static string Spotlight()
        {
            string spo = "https://tdmu.edu.vn/img/spotlight/";

            // Gets the Calendar instance associated with a CultureInfo.
            CultureInfo myCI = new CultureInfo("en-US");
            Calendar myCal = myCI.Calendar;

            // Gets the DTFI properties required by GetWeekOfYear.
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            spo += myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW) + "-";
            if ((int)DateTime.Now.DayOfWeek > 4)
                spo += "2.jpg";
            else
                spo += "1.jpg";

            var req = HttpWebRequest.Create(spo);
            req.Method = "HEAD"; //this is what makes it a "HEAD" request
            WebResponse res = null;
            try
            {
                res = req.GetResponse();
                res.Close();

                return spo;
            }
            catch
            {
                spo = "/Assets/x_coming_sssoon_pages/images/IMG_2424.jpg";
                return spo;
            }
            finally
            {
                if (res != null)
                    res.Close();
            }
        }
    }
}