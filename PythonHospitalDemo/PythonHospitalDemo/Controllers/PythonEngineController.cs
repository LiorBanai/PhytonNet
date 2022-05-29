using PythonNetEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Python.Runtime;

namespace PythonHospitalDemo.Controllers
{
    public class PythonEngineController : IPythonEngineController
    {
        private readonly IPythonEngine m_pythonEngine;

        public PythonEngineController(IPythonEngine pythonEngine)
        {
            m_pythonEngine = pythonEngine;
            Initialize("","");
        }
        public void Initialize(string pathToVirtualEnv, string pythonEXEFolder)
        {
            if (string.IsNullOrEmpty(pathToVirtualEnv))
            {
                pathToVirtualEnv = @"C:\Users\liorb\PycharmProjects\pythonProject\venv";
            }

            if (string.IsNullOrEmpty(pythonEXEFolder))
            {
                pythonEXEFolder = @"C:\Users\liorb\AppData\Local\Programs\Python\Python37";
            }
            string pathToLib = Path.Combine(pathToVirtualEnv, "lib");
            string pathToPackages = Path.Combine(pathToLib, "site-packages");
            string env = $"{pathToPackages};{pathToLib}";
            Environment.SetEnvironmentVariable("PYTHONPATH",env , EnvironmentVariableTarget.Process);

            //Environment.SetEnvironmentVariable("PATH", pathToVirtualEnv, EnvironmentVariableTarget.Process);
            //  Environment.SetEnvironmentVariable("PYTHONHOME", pathToVirtualEnv, EnvironmentVariableTarget.Process);
           
            
          //  PythonEngine.PythonHome = pythonEXEFolder;
           var path= Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
            //PythonEngine.PythonPath = path;
            var paths = m_pythonEngine.PythonPaths();
            m_pythonEngine.SetSearchPath(new List<string> { "./Python/" });
            m_pythonEngine.Initialize(Module.Container);
        }

        public void RunCommand(string command)
        {
            m_pythonEngine.ExecuteCommand(command);
        }
    }
}
