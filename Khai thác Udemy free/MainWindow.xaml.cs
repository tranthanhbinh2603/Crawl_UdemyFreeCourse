#region Khai báo thư viện
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using xNet;
#endregion

namespace Khai_thác_Udemy_free
{
    #region Hàm hiển thị thông tin thay đổi trạng thái listview
    public enum Page { RealDiscount, CouponScorpion, OnlineCourses, UdemyFreebies, Teachinguide, Discudemy, Freebiesglobal, Udemycouponsme, Sitepoint, Coursejoiner, Tutorialbar, TongKet };
    class Trangthai : INotifyPropertyChanged
    {
        private string _threadName;
        public string ThreadName
        {
            get
            {
                return _threadName;
            }
            set
            {
                _threadName = value;
                OnPropertyChanged("ThreadName");
            }
        }
        private string _captions;
        public string Captions
        {
            get
            {
                return _captions;
            }
            set
            {
                _captions = value;
                OnPropertyChanged("Captions");
            }
        }

        public Page Page { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    #endregion

    public partial class MainWindow : Window
    {
        #region Biến cần dùng & Một số hàm không liên quan
        List<string> listlink = new List<string>();

        class Header
        {
            public string Key { get; set; }
            public string Value { get; set; }
        }

        bool Equal_List(List<string> list1, List<string> list2)
        {
            int a = 0;

            for (int i = 0; i < list2.Count; i++)
            {
                if (list1[i] == list2[i])
                {
                    a++;
                }
            }
            if ((a == list1.Count) && (list1.Count == list2.Count))
            {
                return false; //Nếu trùng thì trả false
            }
            else
            {
                return true;
            }

        }

        static void Chiaphan(int n, int sophan, ref int[] kq)
        {
            int ph_nguyen = n / sophan;
            int ph_du = n % sophan;
            int cs = 1;
            int hien_tai = 0;
            for (int i = 1; i <= sophan; i++)
            {
                if (cs <= ph_du)
                {
                    kq[i - 1] = hien_tai + ph_nguyen + 1;
                    cs += 1;
                    hien_tai = kq[i - 1];
                }
                else
                {
                    kq[i - 1] = hien_tai + ph_nguyen;
                    hien_tai = kq[i - 1];
                }
            }
        }
        #endregion

        List<Trangthai> items;
        public MainWindow()
        {
            InitializeComponent();
            #region Hiển thị nội dung ban đầu
            items = new List<Trangthai>();
            items.Add(new Trangthai() { ThreadName = "Free Coupon Discount", Captions = "", Page = Page.TongKet });
            items.Add(new Trangthai() { ThreadName = "Real Discount", Captions = "", Page = Page.TongKet });
            items.Add(new Trangthai() { ThreadName = "Yo! Free Samples", Captions = "", Page = Page.TongKet });
            items.Add(new Trangthai() { ThreadName = "Coupon Scorpion", Captions = "", Page = Page.TongKet });
            items.Add(new Trangthai() { ThreadName = "Online Courses", Captions = "", Page = Page.TongKet });
            items.Add(new Trangthai() { ThreadName = "Oz Bargain", Captions = "", Page = Page.TongKet });
            items.Add(new Trangthai() { ThreadName = "Udemy Freebies", Captions = "", Page = Page.TongKet });
            items.Add(new Trangthai() { ThreadName = "teachinguide", Captions = "", Page = Page.TongKet });
            items.Add(new Trangthai() { ThreadName = "discudemy", Captions = "", Page = Page.TongKet });
            items.Add(new Trangthai() { ThreadName = "freebiesglobal", Captions = "", Page = Page.TongKet });
            items.Add(new Trangthai() { ThreadName = "udemycouponsme", Captions = "", Page = Page.TongKet });
            items.Add(new Trangthai() { ThreadName = "Sitepoint", Captions = "", Page = Page.TongKet });
            items.Add(new Trangthai() { ThreadName = "Coursejoiner", Captions = "", Page = Page.TongKet });
            items.Add(new Trangthai() { ThreadName = "Tutorialbar", Captions = "", Page = Page.TongKet }); 
            items.Add(new Trangthai() { ThreadName = "Smartybro", Captions = "", Page = Page.TongKet }); 
            items.Add(new Trangthai() { ThreadName = "Comidoc", Captions = "", Page = Page.TongKet }); 
            items.Add(new Trangthai() { ThreadName = "Freecoursesforall", Captions = "", Page = Page.TongKet });
            items.Add(new Trangthai() { ThreadName = "Anycouponcode", Captions = "", Page = Page.TongKet });
            for (int i = 1; i <= 7; i++)
            {
                items.Add(new Trangthai() { ThreadName = "Thread " + i, Captions = "", Page = Page.RealDiscount });
            }
            for (int i = 1; i <= 8; i++)
            {
                items.Add(new Trangthai() { ThreadName = "Thread " + i, Captions = "", Page = Page.CouponScorpion });
            }
            for (int i = 1; i <= 25; i++)
            {
                items.Add(new Trangthai() { ThreadName = "Thread " + i, Captions = "", Page = Page.Discudemy });
            }
            for (int i = 1; i <= 30; i++)
            {
                items.Add(new Trangthai() { ThreadName = "Thread " + i, Captions = "", Page = Page.Freebiesglobal });
            }
            for (int i = 1; i <= 30; i++)
            {
                items.Add(new Trangthai() { ThreadName = "Thread " + i, Captions = "", Page = Page.OnlineCourses });
            }
            for (int i = 1; i <= 4; i++)
            {
                items.Add(new Trangthai() { ThreadName = "Thread " + i, Captions = "", Page = Page.Teachinguide });
            }
            for (int i = 1; i <= 5; i++)
            {
                items.Add(new Trangthai() { ThreadName = "Thread " + i, Captions = "", Page = Page.Udemycouponsme });
            }
            for (int i = 1; i <= 35; i++)
            {
                items.Add(new Trangthai() { ThreadName = "Thread " + i, Captions = "", Page = Page.UdemyFreebies });
            }
            for (int i = 1; i <= 30; i++)
            {
                items.Add(new Trangthai() { ThreadName = "Thread " + i, Captions = "", Page = Page.Sitepoint });
            }
            for (int i = 1; i <= 25; i++)
            {
                items.Add(new Trangthai() { ThreadName = "Thread " + i, Captions = "", Page = Page.Coursejoiner });
            }
            for (int i = 1; i <= 30; i++)
            {
                items.Add(new Trangthai() { ThreadName = "Thread " + i, Captions = "", Page = Page.Tutorialbar });
            }

            lvUsers.ItemsSource = items;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Page");
            view.GroupDescriptions.Add(groupDescription);
            #endregion
        }

        #region Các hàm chuyên dùng đếm số trang - Đã thành công 
        int Count_in_realdiscount() //https://www.real.discount/ - Hàm này OK
        {
            try
            {
                xNetStandart.HttpRequest http = new xNetStandart.HttpRequest();
                //http.ConnectTimeout = 30000;
                string html = http.Get("https://app.real.discount/filter/?category=All&store=Udemy&duration=All&price=0&rating=All&language=All&search=&submit=Filter&page=1").ToString();
                string res = "";
                Regex reg = new Regex(@"<span class=""text-right"">Total: (?<Page>.*?) offers</span>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                foreach (Match item in reg.Matches(html))
                {
                    foreach (Capture i in item.Groups["Page"].Captures)
                    {
                        res = i.ToString();
                    }
                }
                return (int.Parse(res) / 12) + (int.Parse(res) > 0 ? 1 : 0);

            }
            catch (xNetStandart.HttpException)
            {
                return Count_in_realdiscount();
            }
        }

        int Count_in_couponscorpion() //https://couponscorpion.com/ - Đã hoàn thành
        {
            try
            {
                HttpRequest http = new HttpRequest();
                http.ConnectTimeout = 2000;
                string html = http.Get("https://couponscorpion.com/category/100-off-coupons/").ToString();
                int res = 0;

                Regex reg = new Regex(@"<ul class=""page-numbers"">.*?</ul>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                Match result = reg.Match(html);

                Regex reg1 = new Regex(@"<li><a href="".*?"">(?<Page>.*?)</a></li>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                foreach (Match item in reg1.Matches(result.ToString()))
                {
                    foreach (Capture i in item.Groups["Page"].Captures)
                    {
                        res = int.Parse(i.ToString());
                    }
                }
                return res;
            }
            catch (xNet.HttpException)
            {
                return Count_in_couponscorpion();
            }
        }

        int Count_in_onlinecourses() //https://www.onlinecourses.ooo/ - Đã hoàn thành
        {
            try
            {
                HttpRequest http = new HttpRequest();
                http.ConnectTimeout = 2500;
                string html = http.Get("https://www.onlinecourses.ooo/").ToString();

                string count = "";
                Regex reg = new Regex(@"There are currently <span>(?<Count>\w+)</span> active coupons", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                foreach (Match item in reg.Matches(html))
                {
                    foreach (Capture i in item.Groups["Count"].Captures)
                    {
                        count = i.ToString();
                    }
                }

                int sotrang = int.Parse(count) / 10 + (int.Parse(count) % 10 != 0 ? 1 : 0);
                return sotrang;
            }
            catch (xNet.HttpException)
            {
                return Count_in_onlinecourses();
            }

        }

        int Count_in_udemyfreebies() //https://www.udemyfreebies.com/ - Đã hoàn thành
        {
            try
            {
                HttpRequest http = new HttpRequest();
                http.ConnectTimeout = 3000;
                string html = http.Get("udemyfreebies.com").ToString();
                int res = 0;

                Regex reg = new Regex(@"<ul class=""theme-pagination"">.*?</ul>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                Match result = reg.Match(html);

                Regex REG = new Regex(@"<li><a href="".*?"">(?<Page>.*?)</a></li>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                foreach (Match item in REG.Matches(result.ToString()))
                {
                    foreach (Capture i in item.Groups["Page"].Captures)
                    {
                        if (i.ToString() != "»")
                        {
                            res = int.Parse(i.ToString());
                        }
                    }
                }

                return res;
            }
            catch (xNet.HttpException)
            {
                return Count_in_udemyfreebies();
            }
        }

        int Count_in_teachinguide() // https://www.teachinguide.com/ - Đã hoàn thành
        {
            try
            {
                xNetStandart.HttpRequest http = new xNetStandart.HttpRequest();
                http.ConnectTimeout = 5000;
                string html = http.Get("https://teachinguide.azure-api.net/course-coupon?sortCol=featured&sortDir=DESC&length=12&page=1&inkw=&discount=100&language=&").ToString();
                int res = 0;

                Regex reg = new Regex(@"""pages"": (?<Count_page>\w+),", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                foreach (Match item in reg.Matches(html))
                {
                    foreach (Capture i in item.Groups["Count_page"].Captures)
                    {
                        res = int.Parse(i.ToString());
                    }
                }
                return res;
            }
            catch (xNetStandart.HttpException)
            {
                return Count_in_teachinguide();
            }
        }

        int Count_in_discudemy() // https://www.discudemy.com/all - Đã hoàn thành
        {
            try
            {
                HttpRequest http = new HttpRequest();
                http.ConnectTimeout = 1600;
                string html = http.Get("https://www.discudemy.com/all").ToString();
                int res = 0;

                Regex reg = new Regex(@"<li><a href="".*?"" >(?<Link>.*?)</a></li>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                foreach (Match item in reg.Matches(html))
                {
                    foreach (Capture i in item.Groups["Link"].Captures)
                    {
                        if ((i.ToString() != "...") && (i.ToString() != "»"))
                        {
                            res = int.Parse(i.ToString());
                        }
                    }
                }
                return res;
            }
            catch (xNet.HttpException)
            {
                return Count_in_discudemy();
            }

        }

        int Count_in_freebiesglobal() //https://freebiesglobal.com/dealstore/udemy - Đã hoàn thành
        {
            try
            {
                HttpRequest http = new HttpRequest();
                http.ConnectTimeout = 3000;
                string html = http.Get("https://freebiesglobal.com/dealstore/udemy").ToString();
                int res = 0;

                Regex reg = new Regex(@"<li><a href="".*?"">(?<Link>.*?)</a></li>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                foreach (Match item in reg.Matches(html))
                {
                    foreach (Capture i in item.Groups["Link"].Captures)
                    {
                        if ((i.ToString() != "...") && (i.ToString() != "»"))
                        {
                            res = int.Parse(i.ToString());
                        }
                    }
                }
                return res;
            }
            catch (xNet.HttpException)
            {
                return Count_in_freebiesglobal();
            }

        }

        int Count_in_udemycouponsme() //https://udemycoupons.me/freeudemycourse/ - Đã hoàn thành
        {
            try
            {
                xNetStandart.HttpRequest http = new xNetStandart.HttpRequest();
                http.ConnectTimeout = 3000;
                string html = http.Get("https://udemycoupons.me/freeudemycourse/").ToString();
                int res = 0;
                Regex reg = new Regex(@"<a href=""https://udemycoupons.me/free-course/page/.*?/"" class=""last"" title="".*?"">(?<Link>.*?)</a>");
                foreach (Match item in reg.Matches(html))
                {
                    foreach (Capture i in item.Groups["Link"].Captures)
                    {
                        res = int.Parse(i.ToString());
                    }
                }                
                return res;
            }
            catch (xNetStandart.HttpException)
            {
                return Count_in_udemycouponsme();
            }

        }

        int Count_in_sitepoint() //https://sitepoint.us/courses/all - Đã hoàn thành
        {
            try
            {
                xNetStandart.HttpRequest http = new xNetStandart.HttpRequest();
                http.ConnectTimeout = 2000;
                string html = http.Get("https://sitepoint.us/courses/all").ToString();
                int res = 0;
                Regex reg = new Regex(@"<a class=""pagination-single"" href=""all/.*?"">(?<Page>.*?)</a>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                foreach (Match item in reg.Matches(html))
                {
                    foreach (Capture i in item.Groups["Page"].Captures)
                    {
                        if (i.ToString() != "»")
                        {
                            res = int.Parse(i.ToString());
                        }
                    }
                }
                return res;
            }
            catch (xNetStandart.HttpException)
            {
                return Count_in_sitepoint();
            }

        }

        int Count_in_coursejoiner() //https://www.coursejoiner.com/free-best-udemy-courses/free-certificate/ - Đã hoàn thành
        {
            try
            {
                HttpRequest http = new HttpRequest();
                http.ConnectTimeout = 1000;
                string html = http.Get("https://www.coursejoiner.com/free-best-udemy-courses/free-certificate/").ToString();
                int res = 0;

                Regex reg = new Regex(@"<a class=""page-numbers"" href="".*?""><span class=""elementor-screen-only"">Page</span>(?<Page>.*?)</a>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                foreach (Match item in reg.Matches(html))
                {
                    foreach (Capture i in item.Groups["Page"].Captures)
                    {
                        res = int.Parse(i.ToString());
                    }
                }
                return res;
            }
            catch (xNet.HttpException)
            {
                return Count_in_coursejoiner();
            }

        }

        int Count_in_tutorialbar() //https://www.tutorialbar.com/all-courses/ - Đã hoàn thành
        {
            try
            {
                HttpRequest http = new HttpRequest();
                http.ConnectTimeout = 1800;
                string html = http.Get("https://www.tutorialbar.com/all-courses/").ToString();
                int res = 0;

                Regex reg = new Regex(@"<li><a href=""https://www.tutorialbar.com/all-courses/page/.*?/"">(?<Page>.*?)</a></li>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                foreach (Match item in reg.Matches(html))
                {
                    foreach (Capture i in item.Groups["Page"].Captures)
                    {
                        res = int.Parse(i.ToString());
                    }
                }
                return res;
            }
            catch (HttpException)
            {
                return Count_in_coursejoiner();
            }
        }

        int Count_in_smartybro() //https://smartybro.com/ - Đã hoàn thành
        {
            try
            {
                HttpRequest http = new HttpRequest();
                http.ConnectTimeout = 2000;
                string html = http.Get("https://smartybro.com/page/1/").ToString();
                int kq = 0;
                Regex reg = new Regex(@"<a\nclass=page-numbers href=https://smartybro.com/page/.*?/ >(?<Page>.*?)</a>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                foreach (Match item in reg.Matches(html))
                {
                    foreach (Capture i in item.Groups["Page"].Captures)
                    {
                        kq = int.Parse(i.ToString());
                    }
                }
                return kq;
            }
            catch (HttpException)
            {
                return Count_in_smartybro();
            }
            
        }

        int Count_in_comidoc() //https://comidoc.net - Đã hoàn thành
        {
            try
            {
                HttpRequest http = new HttpRequest();
                http.ConnectTimeout = 1700;
                string html = WebUtility.HtmlDecode(http.Get("https://comidoc.net/coupons").ToString());
                int kq = 0;
                Regex reg = new Regex(@"""Go to page (?<Link>.*?)""", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                foreach (Match item in reg.Matches(html))
                {
                    foreach (Capture i in item.Groups["Link"].Captures)
                    {
                        kq = int.Parse(i.ToString());
                    }
                }
                return kq;
            }
            catch (HttpException)
            {
                return Count_in_comidoc();
            }
            
        }

        int Count_in_freecoursesforall()//https://freecoursesforall.com/ - Đã hoàn thành
        {
            try
            {
                HttpRequest http = new HttpRequest();
                http.ConnectTimeout = 1400;
                string html = WebUtility.HtmlDecode(http.Get("https://freecoursesforall.com/dealstore/udemy/").ToString());
                int kq = 0;
                Regex reg = new Regex(@"<li><a href=""https://freecoursesforall.com/dealstore/udemy/page/(?<Page>.*?)/"">.*?</a></li>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                foreach (Match item in reg.Matches(html))
                {
                    foreach (Capture i in item.Groups["Page"].Captures)
                    {
                        kq = int.Parse(i.ToString());
                    }
                }
                return kq;
            }
            catch (HttpException)
            {
                return Count_in_freecoursesforall();
            }
            
        } 

        int Count_in_anycouponcode()
        {
            try
            {
                HttpRequest http = new HttpRequest();
                http.ConnectTimeout = 1000;
                string html = WebUtility.HtmlDecode(http.Get("http://www.anycouponcode.net/category/online-course/udemy/").ToString());
                int kq = 0;
                Regex reg = new Regex(@"<a class=""page-numbers"" href=""http://www.anycouponcode.net/category/online-course/udemy/page/(?<Page>.*?)/"">.*?</a>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                foreach (Match item in reg.Matches(html))
                {
                    foreach (Capture i in item.Groups["Page"].Captures)
                    {
                        kq = int.Parse(i.ToString());
                    }
                }
                return kq;
            }
            catch (HttpException)
            {
                return Count_in_anycouponcode();    
            }
        }
        #endregion

        #region Hàm cần dùng - Tất cả các hàm phải được kiểm tra.
        bool Check_Udemy_Course_is_free(string Link) //Đã hoàn thiện
        {
            try
            {
                xNetStandart.HttpRequest http = new xNetStandart.HttpRequest();
                http.ConnectTimeout = 1500;
                string course_id = "";
                string html = http.Get(Link).ToString();
                Regex reg = new Regex(@"course_id&quot;:(?<Course_id>\d+)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                foreach (Match item in reg.Matches(html))
                {
                    foreach (Capture i in item.Groups["Course_id"].Captures)
                    {
                        course_id = i.ToString();
                    }
                }
                string classify = "";
                if (Link[Link.Length - 1] == '/')
                {
                    string Link_remove = Link.Remove(Link.Length - 1, 1);
                    classify = Link_remove.Substring(Link_remove.LastIndexOf('/'), Link_remove.Length - Link_remove.LastIndexOf('/'));
                }
                else
                {
                    classify = Link.Substring(Link.LastIndexOf('/'), Link.Length - Link.LastIndexOf('/'));
                }
                string comcat = "?couponCode=";
                if (classify.Contains(comcat))
                {
                    string cuppon_code = Link.Substring(Link.LastIndexOf('='), Link.Length - Link.LastIndexOf('=')).Replace("=", "");

                    xNetStandart.HttpRequest http1 = new xNetStandart.HttpRequest();
                    http1.ConnectTimeout = 1500;
                    string price = http1.Get("https://www.udemy.com/api-2.0/course-landing-components/" + course_id + "/me/?persist_locale=&locale=en_US&couponCode=" + cuppon_code + "&components=deal_badge,discount_expiration,gift_this_course,price_text,purchase,redeem_coupon,cacheable_deal_badge,cacheable_discount_expiration,cacheable_price_text,cacheable_buy_button,buy_button,buy_for_team,cacheable_purchase_text,cacheable_add_to_cart,money_back_guarantee,instructor_links,top_companies_notice_context,curated_for_ufb_notice,sidebar_container,purchase_tabs_context,subscribe_team_modal_context").ToString();
                    string Pricestr = "";
                    Regex reg2 = new Regex(@"purchasePrice"":{.*?price_string"":""(?<Price>.*?)""");
                    foreach (Match item in reg2.Matches(price))
                    {
                        foreach (Capture i in item.Groups["Price"].Captures)
                        {
                            Pricestr = i.ToString();
                        }
                    }

                    if (Pricestr == "Free")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    xNetStandart.HttpRequest http1 = new xNetStandart.HttpRequest(); 
                    http1.ConnectTimeout = 1500;
                    string price = http1.Get("https://www.udemy.com/api-2.0/course-landing-components/" + course_id + "/me/?persist_locale=&locale=en_US&components=deal_badge,discount_expiration,gift_this_course,price_text,purchase,redeem_coupon,cacheable_deal_badge,cacheable_discount_expiration,cacheable_price_text,cacheable_buy_button,buy_button,buy_for_team,cacheable_purchase_text,cacheable_add_to_cart,money_back_guarantee,instructor_links,top_companies_notice_context,curated_for_ufb_notice,sidebar_container,purchase_tabs_context,subscribe_team_modal_context").ToString();
                    string Pricestr = "";
                    Regex reg2 = new Regex(@"purchasePrice"":{.*?price_string"":""(?<Price>.*?)""");
                    foreach (Match item in reg2.Matches(price))
                    {
                        foreach (Capture i in item.Groups["Price"].Captures)
                        {
                            Pricestr = i.ToString();
                        }
                    }

                    if (Pricestr == "Free")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (xNet.HttpException)
            {
                return Check_Udemy_Course_is_free(Link);
            }
    }

        string Get_html_in_link(string Page, int Timeout, HttpRequest http)
        {
            try
            {
                http.ConnectTimeout = Timeout;
                return http.Get(Page).ToString();
            }
            catch (HttpException)
            {
                return Get_html_in_link(Page, Timeout, http);
            }
        }

        string Get_html_in_link(string Page, int Timeout, xNetStandart.HttpRequest http)
        {
            try
            {
                http.ConnectTimeout = Timeout;
                return http.Get(Page).ToString();
            }
            catch (xNetStandart.HttpException)
            {
                return Get_html_in_link(Page, Timeout, http);
            }
        }

        void Crawl_in_freecoupondiscount() //https://freecoupondiscount.com/  - Đã Hoàn Thiện
        {
            bool Next = true;

            while (Next)
            {
                int Page = 1;
                int Pageplus = 0;
                List<string> listlink_freecoupondiscount = new List<string>(12);
                items[0].Captions = "Đang crawler trang thứ " + Page;
                xNetStandart.HttpRequest http = new xNetStandart.HttpRequest();
                string html = Get_html_in_link("https://freecoupondiscount.com/page/" + Page + "/", 3000, http);

                Regex reg = new Regex(@"<article(.*?)</article>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                foreach (Match item in reg.Matches(html))
                {
                    Regex reg2 = new Regex(@"a href=""(?<Link>.*?)"" itemprop=""url""", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                    foreach (Match item2 in reg2.Matches(item.ToString()))
                    {
                        foreach (Capture i in item2.Groups["Link"].Captures)
                        {
                            listlink_freecoupondiscount.Add(i.ToString());
                        }
                    }
                }

                List<string> list_link_freecoupondiscount_no_dupcate = listlink_freecoupondiscount.Distinct().ToList();

                foreach (var item in list_link_freecoupondiscount_no_dupcate)
                {
                    xNetStandart.HttpRequest http1 = new xNetStandart.HttpRequest();
                    string html1 = Get_html_in_link(item.ToString(), 2000, http1);
                    Regex reg1 = new Regex(@"href=""(?<Link>https://click.linksynergy.com/deeplink.*?)""", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                    foreach (Match item1 in reg1.Matches(html1))
                    {
                        foreach (Capture i in item1.Groups["Link"].Captures)
                        {
                            items[0].Captions = "Đang xem xét xem trang " + i.ToString() + " có còn sống hay không?";
                            string link_udemy = WebUtility.UrlDecode(i.ToString().Substring(i.ToString().LastIndexOf('=') + 1, i.ToString().Length - i.ToString().LastIndexOf('=') - 1));
                            if (Check_Udemy_Course_is_free(link_udemy))
                            {
                                listlink.Add(link_udemy);
                                Pageplus++;
                            }
                            else
                            {
                                items[0].Captions = "Có tất cả " + Pageplus + " trang được thêm vào. ";
                                Next = false;
                                return;
                            }
                        }
                    }
                }

                if (Next == true)
                {
                    Page++;
                }
            }
        }

        void Crawl_realdiscount(int from, int to, int cs) //Hàm chính dùng để crawler trang https://www.real.discount/  - Đã hoàn thành
        {
            int k = 0;
            try
            {
                for (k = from; k < to; k++)
                {
                    items[cs].Captions = "Đang quét trang thứ " + k;
                    xNetStandart.HttpRequest http = new xNetStandart.HttpRequest();
                    http.ConnectTimeout = 4000;
                    string html = WebUtility.HtmlDecode(http.Get("https://app.real.discount/filter/?category=All&store=Udemy&duration=All&price=0&rating=All&language=All&search=&submit=Filter&page=" + k).ToString());
                    Regex reg = new Regex(@"<a href=""/offer/(?<Link_parent>.*?)"">");
                    foreach (Match item in reg.Matches(html))
                    {
                        foreach (Capture i in item.Groups["Link_parent"].Captures)
                        {
                            xNetStandart.HttpRequest http1 = new xNetStandart.HttpRequest();
                            http1.ConnectTimeout = 4000;
                            http1.AddHeader((xNetStandart.HttpHeader)HttpHeader.UserAgent, Http.ChromeUserAgent());
                            string html1 = WebUtility.HtmlDecode(http1.Get("https://app.real.discount/offer/" + i.ToString()).ToString());

                            Regex reg1 = new Regex(@"<a href="".*?(?<Link>https://www.udemy.com/course/.*?)"" target=""_blank"" >");
                            foreach (Match item1 in reg1.Matches(html1))
                            {
                                foreach (Capture i1 in item1.Groups["Link"].Captures)
                                {
                                    listlink.Add(i1.ToString());
                                    items[cs].Captions = "Đã thêm trang " + i1.ToString() + " thuộc trang " + k + " vào danh sách chờ.";
                                }
                                Thread.Sleep(200);
                            }
                        }
                    }
                }
            }
            catch (xNetStandart.HttpException)
            {
                Crawl_realdiscount(k, to, cs);
            }
            
        }

        void Crawl_in_discudemy(int from, int to) //https://www.discudemy.com/all
        {

        }

        void Crawl_yofreesamples() //https://yofreesamples.com/courses/free-discounted-udemy-courses-list/
        {
            HttpRequest http = new HttpRequest();
            string html = Get_html_in_link("https://yofreesamples.com/courses/free-discounted-udemy-courses-list/", 7000, http);

            List<string> list_link_in_yofreesamples = new List<string>();
            Regex reg = new Regex(@"href=""(?<Link>https://www.udemy.com/.*?)""", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
            foreach (Match item in reg.Matches(html))
            {
                foreach (Capture i in item.Groups["Link"].Captures)
                {
                    list_link_in_yofreesamples.Add(i.ToString());
                }
            }

            int a = 0;
            List<string> list_link_no_dupcate = list_link_in_yofreesamples.Distinct().ToList();
            foreach (var item in list_link_no_dupcate)
            {
                listlink.Add(item);
                a++;
            }

            items[2].Captions = "Có tất cả " + a + " trang được thêm vào. ";
        }

        void Crawl_couponscorpion(int from, int to, int cs) //https://couponscorpion.com/ - Đã hoàn thành
        {
            int k = 0;
            try
            {
                for (k = from; k < to; k++)
                {
                    items[cs].Captions = "Đang crawler trang thứ " + k;
                    HttpRequest http = new HttpRequest();
                    http.ConnectTimeout = 3500;
                    string html = http.Get("https://couponscorpion.com/category/100-off-coupons/page/" + k + "/").ToString();
                    Regex reg = new Regex(@"<div class=""news-community category_udemy-coupon-code-2021 category_udemy .*?</div>.*?<a href=""(?<Link>.*?)"" target=""_blank"">", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                    foreach (Match item in reg.Matches(html))
                    {
                        foreach (Capture i in item.Groups["Link"].Captures)
                        {
                            HttpRequest http2 = new xNet.HttpRequest();
                            http2.ConnectTimeout = 1500;
                            string html_get_udemy = http2.Get(i.ToString()).ToString();

                            Regex reg_udemy = new Regex(@"var sf_offer_url = '(?<Link>.*?)';", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                            foreach (Match item_udemy in reg_udemy.Matches(html_get_udemy))
                            {
                                foreach (Capture i_udemy in item_udemy.Groups["Link"].Captures)
                                {
                                    xNetStandart.HttpRequest http3 = new xNetStandart.HttpRequest();
                                    http3.ConnectTimeout = 4000;
                                    System.Uri html_res = http3.Get("https://couponscorpion.com/scripts/udemy/out.php?go=" + i_udemy.ToString()).Address;
                                    items[cs].Captions = "Đã thêm trang " + html_res + " vào danh sách chờ";
                                    Thread.Sleep(200);
                                }
                            }
                        }
                    }
                }
            }
            catch (HttpException)
            {
                Crawl_couponscorpion(k, to, cs);
            }
            catch (xNetStandart.HttpException)
            {
                Crawl_couponscorpion(k, to, cs);
            }
            
        }

        void Crawl_onlinecourses() //https://www.onlinecourses.ooo/
        {

            //int page = 1020;
            //bool Next = true;
            //while (Next)
            //{
            //    tbRes.Dispatcher.Invoke(() => tbRes.Text += "Đang quét trang "+page+"!\n");
            //    Thread.Sleep(20);
            //    tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
            //    HttpRequest http = new HttpRequest();
            //    string html = http.Get("https://www.onlinecourses.ooo/page/" + page).ToString();

            //    Regex reg1 = new Regex(@"Sorry, no coupons found");
            //    Match result = reg1.Match(html);
            //    if (result.ToString() == "")
            //    {
            //        List<string> listlinkdupcate = new List<string>();

            //        Regex reg = new Regex(@"<a href=""(?<Link>https://www.onlinecourses.ooo/coupon/.*?/)""", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
            //        foreach (Match item in reg.Matches(html))
            //        {
            //            foreach (Capture i in item.Groups["Link"].Captures)
            //            {
            //                listlinkdupcate.Add(i.ToString());
            //            }
            //        }
            //        List<string> listlinknodupcate = listlinkdupcate.Distinct().ToList();
            //        List<string> lop1 = new List<string>();
            //        foreach (var item in listlinknodupcate)
            //        {
            //            lop1.Add(item);
            //        }
            //        Thread.Sleep(20);
            //        tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
            //        page++;
            //    }
            //    else
            //    {
            //        tbRes.Dispatcher.Invoke(() => tbRes.Text += "Không còn trang. Kết thúc!");
            //        Thread.Sleep(20);
            //        tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
            //        Next = false;
            //    }

            //}

        }

        void Crawl_dailycoursereviews()
        {

        }

        void Crawl_bestcouponhunter()
        {

        }

        void Crawl_udemycouponlearnviral()
        {

        }

        void Crawl_ozbargain()
        {
            HttpRequest http = new HttpRequest();
            string html = Get_html_in_link("https://www.ozbargain.com.au/deals/udemy.com", 500, http);

            List<string> listpage = new List<string>();
            Regex reg = new Regex(@"<div class=""node node-ozbdeal node-teaser"" id=""node(?<Page>.*?)"">", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
            foreach (Match item in reg.Matches(html))
            {
                foreach (Capture i in item.Groups["Page"].Captures)
                {
                    HttpRequest http1 = new HttpRequest();
                    string html2 = Get_html_in_link("https://www.ozbargain.com.au/node/" + i.ToString(), 500, http1);
                    Regex reg1 = new Regex(@"^<li><a href=""(?<Link>.*?)"" class=""external inline"".*?</li>$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                    foreach (Match item1 in reg1.Matches(html2))
                    {
                        foreach (Capture i1 in item1.Groups["Link"].Captures)
                        {
                            listpage.Add(i1.ToString());
                        }
                    }
                }
            }
            List<string> listpage_nodupcate= listpage.Distinct().ToList();
            items[5].Captions = "Có tất cả " + listpage.Count + " trang được thêm vào.";
            foreach (var item in listpage)
            {
                listlink.Add(item);
            }
        }

        void Choose_Folder()
        {
            //CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            //dialog.IsFolderPicker = true;
            //if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            //{
            //    tbPath.Text = dialog.FileName;
            //}
        }

        void Create_browser()
        {
            #region Không cần thiết
            //var driverService = ChromeDriverService.CreateDefaultService();
            //driverService.HideCommandPromptWindow = true;
            //ChromeOptions option = new ChromeOptions();
            //option.AddExcludedArgument("enable-automation");
            //option.AddAdditionalCapability("useAutomationExtension", false);
            //option.AddArguments("--disable-notifications");
            //tbPath.Dispatcher.Invoke(() => option.AddArgument(@"user-data-dir=" + tbPath.Text));
            //ChromeDriver chromeDriver = new ChromeDriver(driverService, option);

            //chromeDriver.Navigate().GoToUrl(@"https://accounts.google.com/signin/v2/identifier?continue=https%3A%2F%2Fmail.google.com%2Fmail%2F&service=mail&sacu=1&rip=1&flowName=GlifWebSignIn&flowEntry=ServiceLogin");

            //MessageBox.Show("Bạn vui lòng nhập địa chỉ đăng nhập của mình");
            #endregion

        }

        void Runner() //Hàm dùng để chạy khi chương trình chạy
        {
            #region Code chạy ban đầu
            //btRun.Dispatcher.Invoke(() => btRun.IsEnabled = false);
            //pnNeo.Dispatcher.Invoke(() => tbRes.Text += "Đang thực hiện crawler trang Link.Discount!\n");
            //Thread.Sleep(20);
            //tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
            ////Crawl_realdiscount();
            //pnNeo.Dispatcher.Invoke(() => tbRes.Text += "============================================\n");
            //pnNeo.Dispatcher.Invoke(() => tbRes.Text += "Đang thực hiện crawler trang freecoupondiscount.com!\n");
            //Thread.Sleep(20);
            //tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
            ////Crawl_in_freecoupondiscount();
            //pnNeo.Dispatcher.Invoke(() => tbRes.Text += "============================================\n");
            //pnNeo.Dispatcher.Invoke(() => tbRes.Text += "Đang thực hiện crawler trang yofreesamples.com!\n");
            //Thread.Sleep(20);
            //tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
            ////Crawl_yofreesamples();
            //pnNeo.Dispatcher.Invoke(() => tbRes.Text += "============================================\n");
            //pnNeo.Dispatcher.Invoke(() => tbRes.Text += "Đang thực hiện crawler trang couponscorpion.com\n");
            //Thread.Sleep(20);
            //tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
            //Crawl_couponscorpion();
            //pnNeo.Dispatcher.Invoke(() => tbRes.Text += "============================================\n");
            //pnNeo.Dispatcher.Invoke(() => tbRes.Text += "Đang check toàn bộ trang có còn sống hay không?\n");
            //Thread.Sleep(20);
            //tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());

            //List<string> list_link_no_dupcate = listlink.Distinct().ToList();
            //for (int i = 0; i < list_link_no_dupcate.Count; i++)
            //{
            //    if (Check_Udemy_Course_is_free(list_link_no_dupcate[i]))
            //    {
            //        pnNeo.Dispatcher.Invoke(() => tbRes.Text += list_link_no_dupcate[i] + " => Còn sống\n");
            //        Thread.Sleep(20);
            //        tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
            //    }
            //    else
            //    {
            //        pnNeo.Dispatcher.Invoke(() => tbRes.Text += list_link_no_dupcate[i] + " => Không còn sống. Xoá!\n");
            //        list_link_no_dupcate[i] = "";
            //        Thread.Sleep(20);
            //        tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());

            //    }
            //}
            //pnNeo.Dispatcher.Invoke(() => tbRes.Text += "============================================\n");


            //pnNeo.Dispatcher.Invoke(() => tbRes.Text += "Kết quả các trang thu thập được là:\n");
            //foreach (var item in list_link_no_dupcate)
            //{
            //    if (item != "")
            //    {
            //        pnNeo.Dispatcher.Invoke(() => tbRes.Text += item + "\n");
            //    }

            //}
            //pnNeo.Dispatcher.Invoke(() => tbRes.Text += "Kết thúc!\n");
            //Thread.Sleep(20);
            //tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
            //btRun.Dispatcher.Invoke(() => btRun.IsEnabled = true);
            #endregion
            #region Setting tạo luồng
            Thread thr1 = new Thread(() =>
            {
                int a = Count_in_realdiscount();
                items[1].Captions = "Có tất cả " + a + " trang";
                int sophan = 7;
                int[] pc = new int[sophan];
                Chiaphan(a, sophan, ref pc);
                for (int i = 1; i <= sophan; i++)
                {
                    if (i == 1)
                    {
                        Thread thr = new Thread(() =>
                        {
                            Crawl_realdiscount(1, pc[i - 1], 17 + i);
                        });
                        thr.Start();
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Thread thr = new Thread(() =>
                        {
                            Crawl_realdiscount(pc[i - 2], pc[i - 1], 17 + i);
                        });
                        thr.Start();
                        Thread.Sleep(1000);

                    }
                }
            });
            Thread thr2 = new Thread(() =>
            {
                int b = Count_in_couponscorpion();
                items[3].Captions = "Có tất cả " + b + " trang";
                int soluong = 8;
                int[] pc = new int[soluong];
                Chiaphan(b, soluong, ref pc);
                for (int i = 1; i <= soluong; i++)
                {
                    if (i == 1)
                    {
                        Thread thr = new Thread(() =>
                        {
                            Crawl_couponscorpion(1, pc[i - 1], 24 + i);
                        });
                        thr.Start();
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Thread thr = new Thread(() =>
                        {
                            Crawl_couponscorpion(pc[i - 2], pc[i - 1], 24 + i);
                        });
                        thr.Start();
                        Thread.Sleep(1000);

                    }
                }
            });
            Thread thr3 = new Thread(() =>
            {
                int c = Count_in_onlinecourses();
                items[4].Captions = "Có tất cả " + c + " trang";
            });
            Thread thr4 = new Thread(() =>
            {
                int e = Count_in_udemyfreebies();
                items[6].Captions = "Có tất cả " + e + " trang";
            });
            Thread thr5 = new Thread(() =>
            {
                int f = Count_in_teachinguide();
                items[7].Captions = "Có tất cả " + f + " trang";
            });
            Thread thr6 = new Thread(() =>
            {
                int g = Count_in_discudemy();
                items[8].Captions = "Có tất cả " + g + " trang";
            });
            Thread thr7 = new Thread(() =>
            {
                int h = Count_in_freebiesglobal();
                items[9].Captions = "Có tất cả " + h + " trang";
            });
            Thread thr8 = new Thread(() =>
            {
                int i = Count_in_udemycouponsme();
                items[10].Captions = "Có tất cả " + i + " trang";
            });
            Thread thr9 = new Thread(() =>
            {
                int i = Count_in_sitepoint();
                items[11].Captions = "Có tất cả " + i + " trang";
            });
            Thread thr10 = new Thread(() =>
            {
                int i = Count_in_coursejoiner();
                items[12].Captions = "Có tất cả " + i + " trang";
            });
            Thread thr11 = new Thread(() =>
            {
                int z = Count_in_tutorialbar();
                items[13].Captions = "Có tất cả " + z + " trang";
            });
            Thread thr12 = new Thread(() =>
            {
                Crawl_in_freecoupondiscount();
            });
            Thread thr13 = new Thread(() =>
            {
                Crawl_yofreesamples();
            });
            Thread thr14 = new Thread(() =>
            {
                Crawl_ozbargain();
            });
            Thread thr15 = new Thread(() =>
            {
                int kq = Count_in_smartybro();
                items[14].Captions = "Có tất cả " + kq + " trang";
            });
            Thread thr16 = new Thread(() =>
            {
                int kq = Count_in_comidoc();
                items[15].Captions = "Có tất cả " + kq + " trang";
            });
            Thread thr17 = new Thread(() =>
            {
                int kq = Count_in_freecoursesforall();
                items[16].Captions = "Có tất cả " + kq + " trang";
            });
            Thread thr18 = new Thread(() =>
            {
                int kq = Count_in_anycouponcode();
                items[17].Captions = "Có tất cả " + kq + " trang";
            });
            //Thread thrinluong = new Thread(() =>
            //{
            //    int cs = 0;
            //    StreamWriter stream = new StreamWriter(@"D:\test.txt");
            //    foreach (var item in items)
            //    {
            //        stream.WriteLine("{0}: {1}", cs, item.Page);
            //        cs++;
            //    }                
            //    stream.Close();
            //});
            #endregion
            #region Thực thi chạy luồng
            thr1.Start();
            thr2.Start();
            thr3.Start();
            thr4.Start();
            thr5.Start();
            thr6.Start();
            thr7.Start();
            thr8.Start();
            thr9.Start();
            thr10.Start();
            thr11.Start();
            thr12.Start();
            thr13.Start();
            thr14.Start();
            thr15.Start();
            thr16.Start();
            thr17.Start();
            thr18.Start();
            //thrinluong.Start();
            #endregion
        }
        #endregion

        #region Các hàm sự kiện
        private void btGetPath_Click(object sender, RoutedEventArgs e)
        {
            Choose_Folder();
        }

        private void btSignIn_Click(object sender, RoutedEventArgs e)
        {
            Thread thr = new Thread(() =>
            {
                Create_browser();
            });
            thr.Start();
        }

        private void btRun_Click(object sender, RoutedEventArgs e)
        {
            Thread thr = new Thread(() =>
            {
                Runner();
            });
            thr.Start();
        }
        #endregion        
    }
}