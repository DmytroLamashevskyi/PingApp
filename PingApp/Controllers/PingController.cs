using PingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace PingApp.Controllers
{
    public class PingController : IPingManager
    {
        private Thread _thread;

        public event IPingManager.PingResponseData OnPingError;
        public event IPingManager.PingResponseData OnPingSuccess;
        public event PingStopResponseData OnPingStoped;
        public delegate void PingStopResponseData(string replay);

        public PingData pingData { get; private set; }

        public PingController(PingData pingData)
        {
            this.pingData = pingData;
        }

        public bool IsRunning { get; private set; }

        private void Ping(object timeout)
        {
            using (var ping = new Ping())
            {
                while (IsRunning)
                {
                    try
                    {
                        if (pingData == null)
                            IsRunning = false;

                        var reply = ping.Send(pingData.Ip, 2000);
                        if (reply.Status != IPStatus.Success)
                        {
                            OnPingError?.Invoke(reply);
                        }
                        else
                        {
                            OnPingSuccess?.Invoke(reply);
                        }
                    }
                    catch (PingException ex)
                    {
                        MessageBox.Show(ex.Message);
                        Stop();
                        OnPingStoped?.Invoke($"Stop error, [{ex.Message}]");
                    } 
                }
            }

        }

        public void Start()
        {
            IsRunning = true;
            _thread = new Thread(new ParameterizedThreadStart(Ping));
            _thread.IsBackground = true;
            _thread.Start();
        }

        public void Stop()
        {
            IsRunning = false;
            _thread?.Join();
            _thread = null;
            OnPingStoped?.Invoke("Stop command");
        }
    }
}
