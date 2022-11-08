using System;
using System.Net.NetworkInformation;

namespace PingApp.Controllers
{
    public interface IPingManager
    {
        public event PingResponseData OnPingError;
        public event PingResponseData OnPingSuccess; 
        delegate void PingResponseData(PingReply? replay);
        void Start();
        void Stop(); 

    }  
}