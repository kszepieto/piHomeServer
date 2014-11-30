using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using piHome.WebApi;

namespace piHome.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            const string baseAddress = "http://+:8081/";

            using (WebApp.Start<WebApiStartup>(url: baseAddress))
            {
                Console.ReadLine();
            }
        }
    }
}
