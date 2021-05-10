using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using xNet;

namespace Test_crawl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int k = 1;
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
                        }
                    }
                }
            }
            //HttpRequest http = new HttpRequest();
            //http.ConnectTimeout = 3500;
            //string html = http.Get("https://couponscorpion.com/teaching-academics/master-work-time-for-gmat-gre-cat-competitive-exam/").ToString();
        }
    }
}
