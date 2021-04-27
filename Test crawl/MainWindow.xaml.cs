using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            HttpRequest http = new HttpRequest();
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
        }
    }
}
