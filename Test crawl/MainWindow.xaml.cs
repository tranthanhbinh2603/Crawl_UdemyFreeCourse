using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;

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
            xNetStandart.HttpRequest http = new xNetStandart.HttpRequest();
            string html = WebUtility.HtmlDecode(http.Get("https://app.real.discount/filter/?category=All&store=Udemy&duration=All&price=0&rating=All&language=All&search=&submit=Filter&page=" + 1).ToString());
            Regex reg = new Regex(@"<a href=""/offer/(?<Link_parent>.*?)"">");
            foreach (Match item in reg.Matches(html))
            {
                foreach (Capture i in item.Groups["Link_parent"].Captures)
                {
                    xNetStandart.HttpRequest http1 = new xNetStandart.HttpRequest();
                    string html1 = WebUtility.HtmlDecode(http1.Get("https://app.real.discount/offer/" + i.ToString()).ToString());

                    Regex reg1 = new Regex(@"<a href=""(?<Link>https://www.udemy.com/course/.*?)"" target=""_blank"" >");
                    foreach (Match item1 in reg1.Matches(html1))
                    {
                        foreach (Capture i1 in item1.Groups["Link"].Captures)
                        {
                            tbKQ.Text += i1.ToString() + "\n";
                        }
                    }
                }
            }
        }
    }
}
