using Lib_K_Relay;
using Lib_K_Relay.Interface;
using Lib_K_Relay.Networking.Packets.Server;
using Lib_K_Relay.Networking.Packets.Client;
using Lib_K_Relay.Networking.Packets.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib_K_Relay.Networking;
using Lib_K_Relay.GameData;
using Lib_K_Relay.GameData.DataStructures;

namespace HumanAutoAim
{
    public class HumanAutoAim : IPlugin
    {
        #region IPlugin Meta
        public string GetAuthor()
        {
            return "Makurell";
        }
        public string[] GetCommands()
        {
            return new string[] { };
        }
        public string GetDescription()
        {
            return "Human-like Auto Aim";
        }
        public string GetName()
        {
            return "Human AutoAim";
        }
        #endregion
        public bool HumanMode = false;

        public void Initialize(Proxy proxy)
        {
            proxy.HookPacket<NewTickPacket>(OnNewTick);
        }

        private void OnNewTick(Client client, NewTickPacket packet)
        {
            foreach(Entity entity in client.State.RenderedEntities)
            {
                ObjectStructure obj = GameData.Objects.ByID(entity.ObjectType);
                if (obj.Enemy)
                {
                    Console.WriteLine(obj.Name);
                }
            }
        }
    }
}
