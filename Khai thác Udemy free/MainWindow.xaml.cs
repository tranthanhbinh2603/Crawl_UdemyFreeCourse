#region Khai báo thư viện
using Microsoft.WindowsAPICodePack.Dialogs;
using OpenQA.Selenium.Chrome;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using xNetStandart;
#endregion

namespace Khai_thác_Udemy_free
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Các hàm chuyên dùng đếm số trang - Đã thành công 
        int Count_in_realdiscount() //https://www.real.discount/ - Hàm này OK
        {
            try
            {
                HttpRequest http = new HttpRequest();
                http.ConnectTimeout = 30000;
                string html = http.Get("https://app.real.discount/free-courses/").ToString();
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
                string html = http.Get("https://couponscorpion.com/").ToString();
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
            catch (xNetStandart.HttpException)
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
            catch (xNetStandart.HttpException)
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
            catch (xNetStandart.HttpException)
            {
                return Count_in_udemyfreebies();
            }           
        }

        int Count_in_teachinguide() // https://www.teachinguide.com/ - Đã hoàn thành
        {
            try
            {
                HttpRequest http = new HttpRequest();
                http.ConnectTimeout = 2000;
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
                tbRes.Dispatcher.Invoke(() => tbRes.Text = res.ToString());
                return res;
            }
            catch (xNetStandart.HttpException)
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
            catch (xNetStandart.HttpException)
            {
                return Count_in_freebiesglobal();
            }
            
        }

        int Count_in_udemycouponsme() //https://udemycoupons.me/freeudemycourse/ - Đã hoàn thành
        {
            try
            {
                HttpRequest http = new HttpRequest();
                http.ConnectTimeout = 3000;
                string html = http.Get("https://udemycoupons.me/freeudemycourse/").ToString();
                int res = 0;

                Regex reg = new Regex(@"<a href=""https://udemycoupons.me/freeudemycourse/page/(?<Link>.*?)/"" class="".*?"" title="".*?"">", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
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
        #endregion

        #region Hàm cần dùng
        bool Check_Udemy_Course_is_free(string Link) //Đã hoàn thiện
        {
            HttpRequest http = new HttpRequest();
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
            if (Link[Link.Length-1]=='/')
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

                HttpRequest http1 = new HttpRequest();
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
                HttpRequest http1 = new HttpRequest();
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

        void Crawl_in_freecoupondiscount() //https://freecoupondiscount.com/  - Đã Hoàn Thiện
        {
            bool Next = true;

            while (Next)
            {
                int Page = 1;
                tbRes.Dispatcher.Invoke(() => tbRes.Text += "Đang khai thác trang thứ " + Page + "\n");
                Thread.Sleep(20);
                pnNeo.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
                List<string> listlink_freecoupondiscount = new List<string>(12);
                HttpRequest http = new HttpRequest();
                string html = http.Get("https://freecoupondiscount.com/page/" + Page + "/").ToString();

                Regex reg = new Regex(@"<article(.*?)</article>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                foreach (Match item in reg.Matches(html))
                {
                    Regex reg2 = new Regex(@"href=(?<Link>.*?) itemprop=url ", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
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
                    tbRes.Dispatcher.Invoke(() => tbRes.Text += "Đang xem xét trang " + item + "\n");
                    Thread.Sleep(20);
                    pnNeo.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
                    HttpRequest http1 = new HttpRequest();
                    string html1 = http1.Get(item).ToString();
                    Regex reg1 = new Regex(@"href=""(?<Link>https://click.linksynergy.com/deeplink.*?)""", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                    foreach (Match item1 in reg1.Matches(html1))
                    {
                        foreach (Capture i in item1.Groups["Link"].Captures)
                        {
                            string link_udemy = WebUtility.UrlDecode(i.ToString().Substring(i.ToString().LastIndexOf('=') + 1, i.ToString().Length - i.ToString().LastIndexOf('=') - 1));
                            tbRes.Dispatcher.Invoke(() => tbRes.Text += "Đang quét trang " + link_udemy + " còn sống hay không.\n");
                            if (Check_Udemy_Course_is_free(link_udemy))
                            {
                                tbRes.Dispatcher.Invoke(() => tbRes.Text += "Trang " + link_udemy + " còn sống.\n");
                                Thread.Sleep(20);
                                pnNeo.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
                                listlink.Add(link_udemy);
                            }
                            else
                            {
                                tbRes.Dispatcher.Invoke(() => tbRes.Text += "Trang " + link_udemy + " không còn sống.\n");
                                tbRes.Dispatcher.Invoke(() => tbRes.Text += "Kết thúc quét!\n");
                                Thread.Sleep(20);
                                pnNeo.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
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

        void Crawl_realdiscount() //Hàm chính dùng để crawler trang https://www.real.discount/  - Đã hoàn thành
        {
            bool Next = true;
            int x = 1;
            string html = "";
            while (Next)
            {
                tbRes.Dispatcher.Invoke(() => tbRes.Text += "Đang thực hiện crawler trang " + x + " ở trang Link.Discount!\n");
                Thread.Sleep(20);
                tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
                //Check_web_in_realdiscount(x, ref Next, ref html);
                if (Next)
                {
                    Regex reg = new Regex(@"www.udemy.com.*?\""", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                    foreach (Match item in reg.Matches(html))
                    {
                        listlink.Add(item.ToString().Replace(@"\", "").Replace("\"", ""));
                    }
                    x++;
                }
                else
                {
                    break;
                }
            }
        } 

        void Crawl_in_discudemy()
        {

        }

        void Crawl_yofreesamples() //https://yofreesamples.com/courses/free-discounted-udemy-courses-list/
        {
            HttpRequest http = new HttpRequest();
            string html = http.Get("https://yofreesamples.com/courses/free-discounted-udemy-courses-list/").ToString();

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
            tbRes.Dispatcher.Invoke(() => tbRes.Text += "Tổng số lượng trang cộng vào là: " + a + "\n");
            Thread.Sleep(20);
            tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
        }

        void Crawl_couponscorpion() //https://couponscorpion.com/ - Đã hoàn thành
        {
            bool Next = true;
            int Page = 1;
            while (Next)
            {
                tbRes.Dispatcher.Invoke(() => tbRes.Text += "Đang check trang thứ " + Page + "\n");
                pnNeo.Dispatcher.Invoke(() => tbRes.ScrollToEnd());

                HttpRequest http = new HttpRequest();
                string html = http.Get("https://couponscorpion.com/page/" + Page).ToString();
                Regex reg = new Regex(@"<article class=""col_item.*?</article>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                if (reg.Matches(html).Count > 0)
                {
                    foreach (Match item in reg.Matches(html))
                    {
                        Regex reg_get_link = new Regex(@"<a href=""(?<Link>.*?)""", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                        foreach (Match item_link in reg_get_link.Matches(item.ToString()))
                        {
                            foreach (Capture i in item_link.Groups["Link"].Captures)
                            {
                                if ((i.ToString().Contains("category") == false) && (i.ToString() != ""))
                                {
                                    xNet.HttpRequest http2 = new xNet.HttpRequest();
                                    string html_get_udemy = http2.Get(i.ToString()).ToString();

                                    Regex reg_udemy = new Regex(@"var sf_offer_url = '(?<Link>.*?)';", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                                    foreach (Match item_udemy in reg_udemy.Matches(html_get_udemy))
                                    {
                                        foreach (Capture i_udemy in item_udemy.Groups["Link"].Captures)
                                        {
                                            HttpRequest http3 = new HttpRequest();
                                            System.Uri html_res = http3.Get("https://couponscorpion.com/scripts/udemy/out.php?go="+i_udemy.ToString()).Address;
                                            tbRes.Dispatcher.Invoke(() => tbRes.Text += "Đang thêm trang: " + html_res + "\n");
                                            pnNeo.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
                                        }
                                    }
                                }

                            }
                        }
                    }
                    Page++;
                }
                else
                {
                    tbRes.Dispatcher.Invoke(() => tbRes.Text += "Không có link! Kết thúc quét!\n");
                    Next = false;
                }
            }
        }

        void Crawl_onlinecourses() //https://www.onlinecourses.ooo/
        {
            
            int page = 1020;
            bool Next = true;
            while (Next)
            {
                tbRes.Dispatcher.Invoke(() => tbRes.Text += "Đang quét trang "+page+"!\n");
                Thread.Sleep(20);
                tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
                HttpRequest http = new HttpRequest();
                string html = http.Get("https://www.onlinecourses.ooo/page/" + page).ToString();

                Regex reg1 = new Regex(@"Sorry, no coupons found");
                Match result = reg1.Match(html);
                if (result.ToString() == "")
                {
                    List<string> listlinkdupcate = new List<string>();

                    Regex reg = new Regex(@"<a href=""(?<Link>https://www.onlinecourses.ooo/coupon/.*?/)""", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
                    foreach (Match item in reg.Matches(html))
                    {
                        foreach (Capture i in item.Groups["Link"].Captures)
                        {
                            listlinkdupcate.Add(i.ToString());
                        }
                    }
                    List<string> listlinknodupcate = listlinkdupcate.Distinct().ToList();
                    List<string> lop1 = new List<string>();
                    foreach (var item in listlinknodupcate)
                    {
                        lop1.Add(item);
                    }
                    Thread.Sleep(20);
                    tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
                    page++;
                }
                else
                {
                    tbRes.Dispatcher.Invoke(() => tbRes.Text += "Không còn trang. Kết thúc!");
                    Thread.Sleep(20);
                    tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
                    Next = false;
                }
                
            }
            
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

        void Choose_Folder()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                tbPath.Text = dialog.FileName;
            }
        }

        void Create_browser()
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            ChromeOptions option = new ChromeOptions();
            option.AddExcludedArgument("enable-automation");
            option.AddAdditionalCapability("useAutomationExtension", false);
            option.AddArguments("--disable-notifications");
            tbPath.Dispatcher.Invoke(() => option.AddArgument(@"user-data-dir=" + tbPath.Text));
            ChromeDriver chromeDriver = new ChromeDriver(driverService, option);
            
            chromeDriver.Navigate().GoToUrl(@"https://accounts.google.com/signin/v2/identifier?continue=https%3A%2F%2Fmail.google.com%2Fmail%2F&service=mail&sacu=1&rip=1&flowName=GlifWebSignIn&flowEntry=ServiceLogin");

            MessageBox.Show("Bạn vui lòng nhập địa chỉ đăng nhập của mình");
        }

        void Runner() //Hàm dùng để chạy khi chương trình chạy
        {
            btRun.Dispatcher.Invoke(() => btRun.IsEnabled = false);
            pnNeo.Dispatcher.Invoke(() => tbRes.Text += "Đang thực hiện crawler trang Link.Discount!\n");
            Thread.Sleep(20);
            tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
            //Crawl_realdiscount();
            pnNeo.Dispatcher.Invoke(() => tbRes.Text += "============================================\n");
            pnNeo.Dispatcher.Invoke(() => tbRes.Text += "Đang thực hiện crawler trang freecoupondiscount.com!\n");
            Thread.Sleep(20);
            tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
            //Crawl_in_freecoupondiscount();
            pnNeo.Dispatcher.Invoke(() => tbRes.Text += "============================================\n");
            pnNeo.Dispatcher.Invoke(() => tbRes.Text += "Đang thực hiện crawler trang yofreesamples.com!\n");
            Thread.Sleep(20);
            tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
            //Crawl_yofreesamples();
            pnNeo.Dispatcher.Invoke(() => tbRes.Text += "============================================\n");
            pnNeo.Dispatcher.Invoke(() => tbRes.Text += "Đang thực hiện crawler trang couponscorpion.com\n");
            Thread.Sleep(20);
            tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
            Crawl_couponscorpion();
            pnNeo.Dispatcher.Invoke(() => tbRes.Text += "============================================\n");
            pnNeo.Dispatcher.Invoke(() => tbRes.Text += "Đang check toàn bộ trang có còn sống hay không?\n");
            Thread.Sleep(20);
            tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());

            List<string> list_link_no_dupcate = listlink.Distinct().ToList();
            for (int i = 0; i < list_link_no_dupcate.Count; i++)
            {
                if (Check_Udemy_Course_is_free(list_link_no_dupcate[i]))
                {
                    pnNeo.Dispatcher.Invoke(() => tbRes.Text += list_link_no_dupcate[i] + " => Còn sống\n");
                    Thread.Sleep(20);
                    tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
                }
                else
                {
                    pnNeo.Dispatcher.Invoke(() => tbRes.Text += list_link_no_dupcate[i] + " => Không còn sống. Xoá!\n");
                    list_link_no_dupcate[i] = "";
                    Thread.Sleep(20);
                    tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());

                }
            }
            pnNeo.Dispatcher.Invoke(() => tbRes.Text += "============================================\n");


            pnNeo.Dispatcher.Invoke(() => tbRes.Text += "Kết quả các trang thu thập được là:\n");
            foreach (var item in list_link_no_dupcate)
            {
                if (item != "")
                {
                    pnNeo.Dispatcher.Invoke(() => tbRes.Text += item + "\n");
                }

            }
            pnNeo.Dispatcher.Invoke(() => tbRes.Text += "Kết thúc!\n");
            Thread.Sleep(20);
            tbRes.Dispatcher.Invoke(() => tbRes.ScrollToEnd());
            btRun.Dispatcher.Invoke(() => btRun.IsEnabled = true);
            
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
            int sotrang = 0;
            Thread thr = new Thread(() =>
            {
                sotrang  = Count_in_udemycouponsme();
            });            
            thr.Start();
        }


        #endregion
        
    }
}




