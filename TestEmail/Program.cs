using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;

namespace TestEmail
{
    class Program
    {
        static void Main(string[] args)
        {

            string answer = "";
            do
            {


                doScheduler();
                //Ask the user if they want to roll the dice
                Console.WriteLine("Would you like to roll the dice (y or n)?");
                //Get the user's response and validate that it is either 'y' or 'n'.
                answer = Console.ReadLine();
            } while (answer == "y");
        }

       static void  doScheduler()
        {
            // Default folder
             string rootFolder = @"C:\TestFiles\";
            // Files to be deleted    
            string authorsFile = "NV170868002789.pdf";
            string FileName = Path.GetFileNameWithoutExtension(rootFolder + authorsFile);

            var myUniqueFileName = $@"{DateTime.Now.Ticks}.zip";

            using (ZipFile zip = new ZipFile())

            {
                zip.AddFile(rootFolder+authorsFile);
                zip.Save("C:\\TestFiles\\" + myUniqueFileName);
            }
            string test = @"http://localhost/Test/"+ myUniqueFileName;

            //string html = "<html><body><p>Click here for more information: <a href=\"http://localhost/Test/hola.zip\">Asish Link to download kwik File</a></body></html>";
            string html = "<html><body><p>Click here for more information: <a href="+test+">Link to download</a></body></html>";

            SendNotificationARS(html, "Regarding Kwik Email which are having 10mb+", "asish.panda@conduent.com");
            //Delete If Req
            //try
            //{
            //    // Check if file exists with its full path    
            //    if (File.Exists(Path.Combine(rootFolder, authorsFile)))
            //    {
            //        // If file found, delete it    
            //        File.Delete(Path.Combine(rootFolder, authorsFile));
            //        Console.WriteLine("File deleted.");


            //    }
            //    else
            //    {
            //        Console.WriteLine("File not found");
            //    }
            //}
            //catch (IOException ioExp)
            //{
            //    Console.WriteLine(ioExp.Message);
            //}


        }
       static private bool SendNotificationARS(string emailBody, string emailSubject, string mailToAddress)
        {
            bool SendSuccess = false;
            MailMessage mailMsg = new MailMessage();
            try
            {
                mailMsg.From = new MailAddress("No-Reply@conduent.com");

                // MailAddress To = new MailAddress("asish.panda@conduent.com");
                mailMsg.To.Add(new MailAddress(mailToAddress));
                //  mailMsg.CC.Add(new MailAddress("asish.panda@conduent.com"));

                //foreach (var address in distribList.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                //{
                //    mailMsg.Bcc.Add(address);
                //}
                mailMsg.Subject = string.Format("Kwik Response for {0}", emailSubject);
                mailMsg.Body = emailBody;
                mailMsg.IsBodyHtml = true;
                //SmtpMail.Send(mailMsg, SMTPReplyServer, SMTPReplyServerPort);
                SmtpMail.Send(mailMsg, "10.239.83.19", 25);
                SendSuccess = true;
            }
            catch (Exception Exc)
            {
                
            }
            return SendSuccess;
        }




        public static bool Compress(List<string> fileList, string zipName, bool isDirStruct)
        {
            try
            {
                using (var zip = new ZipFile(Encoding.Default))
                {
                    foreach (string path in fileList)
                    {
                        string fileName = Path.GetFileName(path);
                        if (Directory.Exists(path))
                        {
                          
                            if (isDirStruct)
                            {
                                zip.AddDirectory(path, fileName);
                            }
                            else
                            {
                                zip.AddDirectory(path);
                            }
                        }
                        if (File.Exists(path))
                        {
                            zip.AddFile(path);
                        }
                    }
                    zip.Save(zipName);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
