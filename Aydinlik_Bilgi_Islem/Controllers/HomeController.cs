using Aydinlik_Bilgi_Islem.Models.db;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Twilio;

namespace Aydinlik_Bilgi_Islem.Controllers
{
    public class HomeController : Controller
    {
        genelEntities6 ent = new genelEntities6();
        private readonly string myIP;
        private readonly string MACAddress;

        // GET: Home
        public ActionResult Index()
        {                       
            return View();
        }
        [HttpPost]
        public ActionResult Index(GenelBilgi usr)
        {
            GetDns();
            usr.ipAdres = myIP;
            usr.macAdres = MACAddress;
            ent.GenelBilgi.Add(usr);
            ent.SaveChanges();
            Response.Write("<script>alert('İşleminiz kısa süre içersinde gerçekleştirilecektir')</script>");
            // Find your Account Sid and Auth Token at twilio.com/user/account 
            SendEmail();
            return RedirectToAction("Index", "Home");
        }

        private void SendEmail()
        {
            MailMessage o = new MailMessage("sisemih@gmail.com", "sisbas@yahoo.com", "Aydınlık Bilgi İşlem", "Bir şikayet mesajı ulaşmıştır");
            NetworkCredential netCred = new NetworkCredential("sisemih@gmail.com", "Fifediojo179355.Asu");
            SmtpClient smtpobj = new SmtpClient("smtp.gmail.com", 587);
            smtpobj.EnableSsl = true;
            smtpobj.Credentials = netCred;
            smtpobj.Send(o);
        }
        private void GetDns()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST            
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

            NetworkInterface[] nif = NetworkInterface.GetAllNetworkInterfaces();
            String MACAddress = string.Empty;
            foreach (NetworkInterface adapter in nif)
            {
                if (MACAddress == String.Empty)
                {
                    IPInterfaceProperties ipproperties = adapter.GetIPProperties();
                    MACAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
        }
    }
}