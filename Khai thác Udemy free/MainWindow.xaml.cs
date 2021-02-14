#region Khai báo thư viện
using Microsoft.WindowsAPICodePack.Dialogs;
using OpenQA.Selenium.Chrome;
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
        #region Biến cần dùng
        List<string> listlink = new List<string>();
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

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
                                listlink.Add(link_udemy);
                            }
                            else
                            {
                                tbRes.Dispatcher.Invoke(() => tbRes.Text += "Trang " + link_udemy + " không còn sống.\n");
                                tbRes.Dispatcher.Invoke(() => tbRes.Text += "Kết thúc quét!\n");
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
        
        void Check_web_in_realdiscount(int n, ref bool res, ref string html) //Hàm dùng để check link https://www.real.discount/ có sống không - Đã hoàn thành
        {
            HttpRequest http = new HttpRequest();
            try
            {
                html = http.Get("https://www.real.discount/product-tag/100-off/page/" + n + "/?filter_platform=udemy&count=36").ToString();
                res = true;
            }
            catch (xNetStandart.HttpException)
            {
                html = "";
                res = false;
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
                Check_web_in_realdiscount(x, ref Next, ref html);
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
            List<string> list_link_no_dupcate = list_link_in_yofreesamples.Distinct().ToList(); ;
            foreach (var item in list_link_no_dupcate)
            {
                listlink.Add(item);
                a++;
            }
            tbRes.Dispatcher.Invoke(() => tbRes.Text += "Tổng số lượng trang cộng vào là: " + a + "\n");
        }

        void Crawl_couponscorpion() //https://couponscorpion.com/ - Chưa hoàn thành
        {
            //bool Next = true;
            //int Page = 869;
            //while (Next)
            //{
            //    tbRes.Dispatcher.Invoke(() => tbRes.Text += "Đang check trang thứ "+Page+"\n");
            //    HttpRequest http = new HttpRequest();
            //    string html = http.Get("https://couponscorpion.com/page/"+ Page).ToString();
            //    Regex reg = new Regex(@"<article class=""col_item.*?</article>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
            //    if (reg.Matches(html).Count > 0)
            //    {
            //        foreach (Match item in reg.Matches(html))
            //        {
            //            Regex reg_get_link = new Regex(@"<a href=""(?<Link>.*?)""", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
            //            foreach (Match item_link in reg_get_link.Matches(item.ToString()))
            //            {
            //                foreach (Capture i in item_link.Groups["Link"].Captures)
            //                {
            //                    if (i.ToString().Contains("category") == false)
            //                    {
            //                        listlink.Add(i.ToString());
            //                    }
            //                }
            //            }
            //        }
            //        Page++;
            //    }
            //    else
            //    {
            //        tbRes.Dispatcher.Invoke(() => tbRes.Text += "Không có link! Kết thúc quét!\n");
            //        Next = false;
            //    }
            //}   

            HttpRequest http = new HttpRequest();

            //http.AddHeader(":authority", "couponscorpion.com");
            //http.AddHeader(":method", "POST");
            //http.AddHeader(":path", "/scripts/udemy/button.php");
            //http.AddHeader(":scheme", "https");
            //http.AddHeader("accept", "*/*");
            //http.AddHeader(HttpHeader.ContentEncoding, "gzip, deflate, br");
            //http.AddHeader(HttpHeader.AcceptLanguage, "vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5");
            ////http.AddHeader(HttpHeader.ContentLength, "215");
            //http.AddHeader(HttpHeader.ContentType, "application/x-www-form-urlencoded; charset=UTF-8");
            //http.AddHeader("cookie", "__cfduid=de5e7d471287b2c2dd798fb37531e20d51613263310; ezoadgid_84493=-1; ezoref_84493=; ezoab_84493=mod1; lp_84493=https://couponscorpion.com/; ezovid_84493=1247938318; ezovuuid_84493=49633127-7f36-4e42-7c08-7211cfdbac8e; ezCMPCCS=true; cs=ro; ezds=ffid%3D1%2Cw%3D1280%2Ch%3D800; ezohw=w%3D1280%2Ch%3D657; _wpfuuid=33d04fed-b97f-4bdd-8fc2-9f49b9cb1bb6; _ga=GA1.2.448186730.1613263326; _gid=GA1.2.1872125843.1613263326; ezosuigeneris=571580f64056584e80bc24db6ec3d35e; __gads=ID=457cea60e73e7b44:T=1613263330:S=ALNI_MavVxdNFG5AlAt7S3f5asM65OTpAA; pbjs-unifiedid=%7B%22TDID%22%3A%22c93120bf-dbe1-4170-aa30-7e231ad5a100%22%2C%22TDID_LOOKUP%22%3A%22TRUE%22%2C%22TDID_CREATED_AT%22%3A%222021-01-14T00%3A42%3A13%22%7D; id5id.1st_last=Sun%2C%2014%20Feb%202021%2000%3A42%3A15%20GMT; id5id.1st=%7B%22created_at%22%3A%222021-02-14T00%3A42%3A15.28Z%22%2C%22id5_consent%22%3Atrue%2C%22original_uid%22%3A%22ID5-ZHMOwy-XEAxXhW5BIixt7gzevthILVFRA9ATXqYABw%22%2C%22universal_uid%22%3A%22ID5-ZHMOwy-XEAxXhW5BIixt7gzevthILVFRA9ATXqYABw%22%2C%22signature%22%3A%22ID5_AZD9EEHuBxtlR0iABQamqlYwk3klREKp9aMuBTCgSnrjwZhbgUlqgn7Arf6JiFM6VdQ4X0VsZgMJXZpRTqQPGSQ%22%2C%22link_type%22%3A0%2C%22cascade_needed%22%3Atrue%7D; __qca=P0-2083069699-1613263351372; cto_bundle=n-44j19wNUolMkZjJTJCckhIbTAwYzdwNjJqZkt5TzQ5NEE3VVFpY0VGNFB3SllUV3hIb01najdNaVBON3k4eENqMzE2c3RXQ1dvTmY1eFIwNXJjSjY2VVdlTDU5STAwdE9TaFRmQXczVXRqc2h5U1lhQm1UdEpSZ1dhbG1vOUdpN1ZKZkdzQkQ; cto_bidid=vxaodV9lRDJiS2xKSURHU0gyRCUyQkNXZ1hLeDNMd2R0UmpteURQaGxIbExUdE9UYXBKczlxaVdiaWNiNnA2RTdkYmhMY1o1TnVpVHJScXBSdHNMakxiWG9IYXVnJTNEJTNE; ezepvv=49; id5id.1st_457_nb=17; ezux_lpl_84493=1613265514508|8faa5b60-dd65-4172-739e-b2f9b3fb73b8|false; ezouspvh=6; ezux_ifep_84493=true; ezouspvv=18; ezouspva=3; active_template::84493=pub_site.1613265671; ezopvc_84493=20; ezovuuidtime_84493=1613265674; ezux_et_84493=38; ezux_tos_84493=154");
            //http.AddHeader("origin", "https://couponscorpion.com");
            //http.Referer = "https://couponscorpion.com/personal-development/the-six-keys-to-liberating-your-life/";
            //http.AddHeader("sec-fetch-dest", "empty");
            //http.AddHeader("sec-fetch-mode", "cors");
            //http.AddHeader("sec-fetch-site", "same-origin");
            //http.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.150 Safari/537.36");
            //http.AddHeader("x-requested-with", "XMLHttpRequest");
            
            //string html = http.Post(,).ToString();
            
            //tbRes.Dispatcher.Invoke(() => tbRes.Text += html);
        }

        void Crawl_onlinecourses()
        {

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
            //btRun.Dispatcher.Invoke(() => btRun.IsEnabled = false);
            //tbRes.Dispatcher.Invoke(() => tbRes.Text += "Đang thực hiện crawler trang Link.Discount!\n");
            //Crawl_realdiscount();
            //tbRes.Dispatcher.Invoke(() => tbRes.Text += "============================================\n");
            //tbRes.Dispatcher.Invoke(() => tbRes.Text += "Đang thực hiện crawler trang freecoupondiscount.com!\n");
            //Crawl_in_freecoupondiscount();
            //tbRes.Dispatcher.Invoke(() => tbRes.Text += "============================================\n");
            //tbRes.Dispatcher.Invoke(() => tbRes.Text += "Đang thực hiện crawler trang yofreesamples.com!\n");
            //Crawl_yofreesamples();
            //tbRes.Dispatcher.Invoke(() => tbRes.Text += "============================================\n");

            //tbRes.Dispatcher.Invoke(() => tbRes.Text += "Đang check toàn bộ trang có còn sống hay không?\n");
            Crawl_couponscorpion();
            //List<string> list_link_no_dupcate = listlink.Distinct().ToList();
            //for (int i = 0; i < list_link_no_dupcate.Count; i++)
            //{                
            //    if (Check_Udemy_Course_is_free(list_link_no_dupcate[i]))
            //    {
            //        tbRes.Dispatcher.Invoke(() => tbRes.Text += list_link_no_dupcate[i] + " => Còn sống\n");
            //    }
            //    else
            //    {
            //        tbRes.Dispatcher.Invoke(() => tbRes.Text += list_link_no_dupcate[i] + " => Không còn sống. Xoá!\n");
            //        list_link_no_dupcate[i] = "";

            //    }
            //}
            //tbRes.Dispatcher.Invoke(() => tbRes.Text += "============================================\n");

            
            //tbRes.Dispatcher.Invoke(() => tbRes.Text += "Kết quả các trang thu thập được là:\n");
            //foreach (var item in list_link_no_dupcate)
            //{
            //    if (item != "")
            //    {
            //        tbRes.Dispatcher.Invoke(() => tbRes.Text += item + "\n");
            //    }

            //}
            //tbRes.Dispatcher.Invoke(() => tbRes.Text += "Kết thúc!\n");


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
