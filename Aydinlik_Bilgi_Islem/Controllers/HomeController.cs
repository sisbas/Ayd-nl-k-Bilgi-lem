using Aydinlik_Bilgi_Islem.Models.db;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Web.Mvc;

namespace Aydinlik_Bilgi_Islem.Controllers
{
    public class HomeController : Controller
    {
        
        genelEntities4 ent = new genelEntities4();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(GenelBilgi usr)
        {   
            usr.ipAdres = GetIp();
            usr.macAdres = GetMac();
            usr.dateTime = DateTime.Now;
            ent.GenelBilgi.Add(usr);
            ent.SaveChanges();            
            // Find your Account Sid and Auth Token at twilio.com/user/account 
            SendEmail();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult DataEdit()
        {
            //ProcessStartInfo si = new ProcessStartInfo("cmd.exe");
            //// Redirect both streams so we can write/read them.
            //si.RedirectStandardInput = true;
            //si.RedirectStandardOutput = true;
            //si.UseShellExecute = false;
            //si.CreateNoWindow = false;
            //// Start the procses.
            //Process p = Process.Start(si);            
            //// Issue the dir command.
            //p.StandardInput.WriteLine(@"dir c:");
            //// Exit the application.
            //p.StandardInput.WriteLine(@"exit");
            //// Read all the output generated from it.
            //string output = p.StandardOutput.ReadToEnd();
            //Response.Write(output);
            return View();
        }
        private void SendEmail()
        {
            MailMessage o = new MailMessage("sisemih@gmail.com", "sisbas@yahoo.com", "Aydinlik Bilgi Islem", "Bir şikayet mesajı ulaşmıştır \n" +GetIp()+ "\n"+GetMac());
            NetworkCredential netCred = new NetworkCredential("sisemih@gmail.com", "Fifediojo179355.Asu");
            SmtpClient smtpobj = new SmtpClient("smtp.gmail.com", 587);
            smtpobj.EnableSsl = true;
            smtpobj.Credentials = netCred;
            smtpobj.Send(o);
        }
        private string GetIp()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST            
            string ipAdresi = Dns.GetHostByName(hostName).AddressList[0].ToString();
            return ipAdresi;            
        }
        private string GetMac()
        {
            NetworkInterface[] nif = NetworkInterface.GetAllNetworkInterfaces();
            String macAdresi = string.Empty;
            foreach (NetworkInterface adapter in nif)
            {
                if (macAdresi == String.Empty)
                {
                    IPInterfaceProperties ipproperties = adapter.GetIPProperties();
                    macAdresi = adapter.GetPhysicalAddress().ToString();
                }
            }
            return macAdresi;
        }
        static void TxtSave()
        {
            try
            {
                string path = Environment.CurrentDirectory + @"\ConsoleApplication1.exe";
                ProcessStartInfo start = new ProcessStartInfo();
                StreamWriter outputFile = new StreamWriter(Environment.CurrentDirectory + @"\log.txt", false);
                start.FileName = "cmd.exe"; // Specify exe name.
                start.Arguments = "/c ipconfig -release";
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                string line;
                using (Process process = Process.Start(start))
                {

                    //
                    // Read in all the text from the process with the StreamReader.
                    //
                    using (StreamReader reader = process.StandardOutput)
                    {

                        while ((line = reader.ReadLine()) != null)
                        {
                            outputFile.WriteLine(line);
                        }
                        outputFile.Dispose();
                        outputFile.Close();

                    }

                }

            }
            catch (Exception e)
            {
                Environment.Exit(0);
                throw e;

            }


        }

    }
}