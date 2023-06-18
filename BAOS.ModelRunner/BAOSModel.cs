using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAOS.Web.Domain.Models;

namespace BAOS.ModelRunner
{
    public class BAOSModel
    {
        private ProcessStartInfo psi;
        public BAOSModel()
        {
            // 1) Create process info
            psi = new ProcessStartInfo();
            psi.FileName = @"C:\Users\ata-s\AppData\Local\Programs\Python\Python311\python.exe";
        }
        public string Run(ModelFeatures features)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).FullName;

            // 2) Provide script and arguments
            var cmd = projectDirectory + "\\BAOS.ModelRunner\\python_model\\app.py";

            psi.Arguments = BuildArguments(cmd, features.features);

            // 3) Process configurations
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            // 4) Execute process and get output
            var errors = "";
            var results = "";

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            return results;
        }

        public string BuildArguments(string cmd, List<int> features)
        {
            string result = cmd;

            foreach (var feature in features)
            {
                result += " ";
                result += feature;
            }

            return result;
        }
    }
}
