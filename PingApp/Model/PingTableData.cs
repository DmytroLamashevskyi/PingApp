using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PingApp.Model
{
    public class PingTableData : INotifyPropertyChanged
    {
        public static long IntDifUpdate = 4;
        public string Name { get;  set; }
        public long Ping { get; set; }
        public string LastConnection { get; set; }
        public string Status { get; set; }

        public bool IsRunning { set; get; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }


        public void SetPingData(PingReply reply)
        {
            LastConnection = DateTime.Now.ToString("HH:mm:ss");
            Ping = reply.RoundtripTime-Ping > IntDifUpdate ? reply.RoundtripTime:Ping;
            Status = reply.Status.ToString();
            IsRunning = reply.Status == IPStatus.Success;
            OnPropertyChanged("Ping");
            OnPropertyChanged("LastConnection");
            OnPropertyChanged("Status");
            OnPropertyChanged("IsRunning");
        }
        public void SetPingData(string reply)
        { 
            Status = reply;
            IsRunning = false; 
            OnPropertyChanged("Status");
            OnPropertyChanged("IsRunning");
        }
    }
}
