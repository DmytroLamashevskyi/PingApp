using PingApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingApp.Controllers
{
    public class CommandController
    {
        private static readonly Lazy<CommandController> lazy = new Lazy<CommandController>(() => new CommandController());
        public static CommandController Instance => lazy.Value;

        private delegate TOut ParamsFunc<TIn, TOut>(params TIn[] args);
        private Dictionary<string, ParamsFunc<string, bool>> _commands = new Dictionary<string, ParamsFunc<string, bool>>()
        {
            {"add",Add },
            {"del",Delete },
            {"upd",Update },


        };

        public void Run(string text)
        {
            text = text.Trim();
            string[] stext = text.Split(null);
            if (_commands.ContainsKey(stext[0].ToLower()))
            {
                if (!_commands[stext[0].ToLower()](stext.Skip(1).ToArray()))
                {
                    throw new Exception("Cann't execute command");
                }
            }
        }

        public static bool Add(string[] args)
        {
            if (args.Length >= 2)
            {
                PingManager.Instance.Add(new PingController(new PingData(args[0], args[1])));
                PingManager.Instance.UpdateCollection();
            }

            return true;
        }
        public static bool Delete(string[] args)
        {
            if (args.Length >= 1)
            {
                PingManager.Instance.DeleteByName(args[0]); 
            }

            return true;
        }
        public static bool Update(string[] args)
        {


            return true;
        }
    }
}
