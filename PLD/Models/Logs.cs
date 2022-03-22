using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace PLD.Models
{
    public class Logs
    {
        private static ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();

        public static bool Log(string lines, bool error)
        {
            // Set Status to Locked
            _readWriteLock.EnterWriteLock();
            try
            {
                if (!lines.Contains("Thread was being aborted"))
                {
                    //var variables = HttpContext.Current.Request.ServerVariables;
                    //var taskSchdlr = System.Threading.Tasks.TaskScheduler.Current;

                    //var filePath = variables["APPL_PHYSICAL_PATH"] + "\\logs\\log" + DateTime.Now.ToString("yyyy_MM_dd") + ".txt";
                    //var path = System.IO.Directory.GetCurrentDirectory();
                    //var otro = HttpRuntime.AppDomainAppVirtualPath;

                    //var path = System.Web.Hosting.HostingEnvironment.MapPath("/");
                    var path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
                    //var path = "D:\\inetpub\\wwwroot\\wcfBuroCredito";
                    var filePath = path + "\\logs\\log_" + (error == true ? "error_" : string.Empty) + DateTime.Now.ToString("yyyy_MM_dd") + ".txt";

                    if (filePath.Length == 0)
                        return false;
                    if (lines.Length == 0)
                        return false;

                    // Append text to the file
                    //using (StreamWriter sw = File.AppendText(filePath)) {
                    using (StreamWriter sw = File.Exists(filePath) ? File.AppendText(filePath) : File.CreateText(filePath))
                    {
                        sw.WriteLine((error == true ? "ERROR: " : string.Empty) + lines);
                        sw.WriteLine("FECHA: " + DateTime.Now);
                        sw.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
                        sw.Close();
                    }
                }
            }
            finally
            {
                // Release lock
                _readWriteLock.ExitWriteLock();
            }
            return true;
        }
    }
}