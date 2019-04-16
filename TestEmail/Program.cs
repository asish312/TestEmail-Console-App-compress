using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net.Mail;

namespace TestEmail
{
    class Program
    {
        public static string answer = "";
        static void Main(string[] args)
        {           
            do
            {
                 Console.WriteLine("Would you like to Enter your Email ID: ? ___@conduent.com .For e.g asish.panda");
                //Get the user's response and validate that it is either 'y' or 'n'.
                answer = Console.ReadLine();

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
            string rootFolder = @"D:\Kwik10mb+\20151123EI000000001";
            var myUniqueFileName = $@"{DateTime.Now.Ticks}.zip";

            using (ZipFile zip = new ZipFile())
            {
                zip.AddDirectory(rootFolder);
                zip.Save("//MLMLEE4VSQL35/TestAsish/" + myUniqueFileName);
            }

            //string[] subdirs = Directory.GetDirectories(rootFolder);
            //List<string> map_data = new List<string>();

            //foreach (var batch in subdirs)
            //{
            //    map_data.Add(batch);
            //}

            //Compress(map_data, myUniqueFileName, false);



            // Files to be deleted    
            //string authorsFile = "NV170868002789.pdf";
            //string FileName = Path.GetFileNameWithoutExtension(rootFolder + authorsFile);           

            //using (ZipFile zip = new ZipFile())

            //{
            //    zip.AddFile(rootFolder+authorsFile);
            //    zip.Save("C:\\TestFiles\\" + myUniqueFileName);
            //}
            
            string testCompessFile = @"http://MLMLEE4VSQL35//KwikTestFiles/" + myUniqueFileName;
           
            string html = "<html><body><p>Click here for more information: <a href="+ testCompessFile + ">Link to download</a></body></html>";

            SendNotificationARS(html, "Regarding Kwik Email which are having 10mb+", answer.Trim()+"@conduent.com");

            Console.WriteLine("/n Thanks {0} .. An email Sent with Download Link to {1}",answer,answer+"@conduent.com");
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

        private static void AddFolderToZip(ZipFile zipFile, string packageRootFolder, string addFolder)
        {
            string sourceFolder = Path.Combine(packageRootFolder, addFolder);

            if (Directory.Exists(sourceFolder))
                zipFile.AddDirectory(sourceFolder, addFolder);
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
