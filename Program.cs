using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.IO;
using System.Collections;
namespace Email
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] emails = File.ReadAllLines(@"emails.txt");
            string[] names = File.ReadAllLines(@"names.txt");
            string[] cnic = File.ReadAllLines(@"cnic.txt");
            int i = 0;
            string newBody;

            string body = File.ReadAllText(@"text.txt");
            foreach (string email in emails)
            {
                newBody = body;
                newBody = newBody.Replace("Dear ,", "Dear " + names[i] + ",");
                cnic[i]= cnic[i].Substring(5) + cnic[i].Remove(5);
                newBody = newBody.Replace("Your registration code is ", "Your registration code is " + cnic[i] + ",");
                Email("XXXX@gmail.com", email, newBody);
                i++;
            }
        }
        static void Email(string s,string r, string b)        
        {
            try
            {
                MailMessage message = new MailMessage(s, r){
                    Subject = "XXXXX"
                };
                SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["Host"], Convert.ToInt32(ConfigurationManager.AppSettings["Port"])){
                    Credentials = new NetworkCredential(s, ConfigurationManager.AppSettings["Pass"]),
                    EnableSsl = true
                };
                message.Body = b.ToString();
                smtp.Send(message);
            }
            catch (Exception){
            }

        }
    }
}
