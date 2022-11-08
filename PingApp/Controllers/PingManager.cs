using PingApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml.Linq;

namespace PingApp.Controllers
{
    public class PingManager : List<PingController>
    {
        private static readonly Lazy<PingManager> lazy = new Lazy<PingManager>(() => new PingManager());
        public static PingManager Instance => lazy.Value;

        public ObservableCollection<PingTableData> TableCollection { set; get; }

        public void InitCollection()
        {
            TableCollection = new ObservableCollection<PingTableData>();
            foreach (var ping in this)
            {
                var data = new PingTableData()
                {
                    Name = ping.pingData.Name, 
                };

                ping.OnPingError += data.SetPingData;
                ping.OnPingSuccess += data.SetPingData;
                ping.OnPingStoped += data.SetPingData;

                TableCollection.Add(data);
            } 
        }

        public void UpdateCollection()
        { 
            foreach (var ping in this)
            {
                if(TableCollection.FirstOrDefault(d=>d.Name == ping.pingData.Name) is null)
                {
                    var data = new PingTableData()
                    {
                        Name = ping.pingData.Name,
                    };

                    ping.OnPingError += data.SetPingData;
                    ping.OnPingSuccess += data.SetPingData;
                    ping.OnPingStoped += data.SetPingData;

                    TableCollection.Add(data);
                } 
            }
        }

        public void StartAll()
        {
            this.ForEach(p=>p.Start());
        }

        public void StopAll()
        {
            this.ForEach(p => p.Stop());
        }


        public void StopByName(string name)
        {
            this.FirstOrDefault(n => n.pingData.Name == name)?.Stop();
        }

        public void StartByName(string name)
        {
            this.FirstOrDefault(n => n.pingData.Name == name)?.Start();
        }
        public void RestartByName(string name)
        {
            var item = this.FirstOrDefault(n => n.pingData.Name == name);
            item.Stop();
            item.Start();
        }

        internal void DeleteByName(string v)
        {
            StopByName(v);
            this.RemoveAll(r => r.pingData.Name == v);
            TableCollection.Remove(TableCollection.FirstOrDefault(e => e.Name == v));
        }
    }
}
