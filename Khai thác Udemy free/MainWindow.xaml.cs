using Microsoft.WindowsAPICodePack.Dialogs;
using OpenQA.Selenium.Chrome;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using xNetStandart;


#region Biến cần dùng

#endregion

namespace Khai_thác_Udemy_free
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Hàm cần dùng
        void Check_Udemy_Course_is_free()
        {

        }

        void Crawl_in_freecoupondiscount()
        {

        }

        void Crawl_in_realdiscount()
        {
            HttpRequest http = new HttpRequest();
            string html = WebUtility.HtmlDecode(http.Get("https://www.real.discount/product-tag/100-off/?filter_platform=udemy&count=36").ToString());
            Regex reg = new Regex(@"www.udemy.com.*?\""", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline | RegexOptions.Singleline);
            foreach (Match item in reg.Matches(html))
            {
                tbRes.Dispatcher.Invoke(() => tbRes.Text += item.ToString().Replace(@"\","").Replace("\"","") + "\n");
            }

        }

        void Crawl_in_discudemy()
        {

        }

        void Crawl_yofreesamples()
        {

        }

        void Crawl_couponscorpion()
        {

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
            HttpRequest http = new HttpRequest();
            string html = WebUtility.HtmlDecode(http.Get("https://udemycoupon.learnviral.com/coupon-category/free100-discount/").ToString());
            
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
        #endregion

        private void btRun_Click(object sender, RoutedEventArgs e)
        {
            Thread thr = new Thread(() =>
            {
                Crawl_in_realdiscount();
            });
            thr.Start();
        }
    }
}
