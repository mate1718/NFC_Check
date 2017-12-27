using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.System;
using System.Threading;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NFCCheckApp
{   /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>




    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
                this.InitializeComponent();
        }

        public string Uri;                
        BitmapImage bitmapim = new BitmapImage();     // image variable                                     
        Tool tool = new Tool();
        Thread th;                                   //delay thread



        private void NFC_Code_TextChanged(object sender, TextChangedEventArgs e) // NFC code control
        {
            if (th != null)
            {
                th.Interrupt();
                th = null;
            }

            if (NFC_Code.Text.Length==10)
            {
                DataBase dataB = new DataBase();
                Log log = new Log();
                try
                {
                    using (var db = new NFC_Check_DBContext())
                    {

                        if (img.Source == null)
                        {
                            tool = db.Tool.Where(a => a.NfcId == int.Parse(NFC_Code.Text)).First();
                            if (tool != null) //Tool OK
                            {
                                bitmapim.UriSource = new Uri(img.BaseUri, tool.Image);                                               // dimension, so long as one dimension measurement is provided
                                img.Source = bitmapim;
                                Name.Text = tool.Type;

                            }
                            else //Invalid Tool
                            {
                                dataB.ErrorSave(1,"Invalid tool");
                                errorText.Text = "Invalid tool!";
                                ErrorMSGPop.IsOpen=true;
                            }
                        }
                        else
                        {
                            Employee emp = db.Employee.Where(a => a.BadgeId == int.Parse(NFC_Code.Text)).First();
                            Binding bind = db.Binding.Where(a => a.EmployeeId == emp.EmployeeId &&
                            a.ToolId==tool.ToolId).LastOrDefault();
                            if (emp != null)
                            {
                                if(bind==null) 
                                {
                                    dataB.ErrorSave(1, "Invalid binding!");
                                    errorText.Text = "Invalid binding!";
                                    ErrorMSGPop.IsOpen = true;
                                }
                                else //Entry OK
                                {   
                                    dataB.Save(1, bind.BindingId, bind.Log.Last().Active);
                                    bitmapim.UriSource = new Uri(img.BaseUri, emp.Image);                                               // dimension, so long as one dimension measurement is provided
                                    img.Source = bitmapim;
                                    Name.Text = emp.FirstName + " " + emp.Surname;
                                }
                            }
                            else
                            {
                                dataB.ErrorSave(1, "Invalid employee!");
                                errorText.Text = "Invalid employee!";
                                ErrorMSGPop.IsOpen = true;
                            }
                            
                        }

                    }
                }
                catch(Exception ex)
                {using (var db = new NFC_Check_DBContext()) //connection problem
                    {
                        dataB.ErrorSave(1, ex.Message);
                        errorText.Text = ex.Message;
                        ErrorMSGPop.IsOpen = true;
                        NFC_Code.Text = "";
                    }
                }
                NFC_Code.Text = "";
                
            }
        }

        private void img_ImageOpened(object sender, RoutedEventArgs e) // thread start, tool control
        {
            if (th != null)
            {
                th.Interrupt();
                th = null;
            }
            th = new Thread(new ThreadStart(ThreadFunction));
            th.IsBackground = true;
            th.Start();

           
        }

        public void ThreadFunction() // thread delay function
        {
            try
            {
                int i = 0;
                while(true)
                {
                    i++;
                    System.Threading.Thread.Sleep(1000);
                    if(i==10)
                    {
                        tool = null;
                        bitmapim.UriSource = new Uri(img.BaseUri, "");                                               // dimension, so long as one dimension measurement is provided
                        img.Source = null;
                        th.Interrupt();
                        th = null;
                    }
                }
            }
            catch(ThreadInterruptedException)
            {

            }
            
        }


        private void ErrorMSGPop_Loaded(object sender, RoutedEventArgs e) // error popup windows, display
        {
            for(var i=0;i<5;i++)
            {
                NFC_Code.IsEnabled = false;
                System.Threading.Thread.Sleep(1000);
                
            }
            NFC_Code.IsEnabled = true;
            ErrorMSGPop.IsOpen = false;
        }
    }
    
}
