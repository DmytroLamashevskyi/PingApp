using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingApp.Model
{
    public class PingData
    {
        public PingData(string name, string value)
        {
            if (value.StartsWith("http://"))
            {
                Ip = value.Substring("http://".Length).TrimEnd('/');
            }
            if (value.StartsWith("https://"))
            {
                Ip = value.Substring("https://".Length).TrimEnd('/');
            }
            else
            {
                Ip = value.TrimEnd('/');
            }
            Name =name; 
        }

        public string Name { get; set; }
        public string Ip { get; set; }
    }
}
