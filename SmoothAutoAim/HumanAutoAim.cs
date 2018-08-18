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
using Lib_K_Relay.Networking.Packets;

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
            proxy.HookPacket<PlayerShootPacket>(OnPlayerShoot);
        }

        private void OnPlayerShoot(Client client, PlayerShootPacket packet)
        {
            //Console.WriteLine(packet);
            /*
             * PLAYERSHOOT(52) Packet Instance
	Time => 2167100
	BulletId => 1
	ContainerType => 2824
	Position => { X=106.9856, Y=162.6525 }
	Angle => 3.080225
	Send => True
	Id => 52
PLAYERSHOOT(52) Packet Instance
	Time => 2167100
	BulletId => 2
	ContainerType => 2824
	Position => { X=106.9856, Y=162.6525 }
	Angle => 3.080225
	Send => True
	Id => 52
PLAYERSHOOT(52) Packet Instance
	Time => 2167228
	BulletId => 3
	ContainerType => 2824
	Position => { X=106.9856, Y=162.6525 }
	Angle => 3.080225
	Send => True
	Id => 52
PLAYERSHOOT(52) Packet Instance
	Time => 2167228
	BulletId => 4
	ContainerType => 2824
	Position => { X=106.9856, Y=162.6525 }
	Angle => 3.080225
	Send => True
	Id => 52
             */
        }

        /// <summary>
        /// convert polar coordinates to cartesian coordinates
        /// </summary>
        public static Location ToRect(float theta, float r)
        {
            return new Location((float)(r * Math.Cos(theta)), (float)(r * Math.Sin(theta)));
        }

        /// <summary>
        /// convert cartesian coordinates to polar coordinates
        /// </summary>
        /// <returns>Tuple: theta, r</returns>
        public static Tuple<float,float> ToPol(Location loc)
        {
            return new Tuple<float, float>((float)Math.Atan2(loc.Y, loc.X), (float)Math.Sqrt(Math.Pow(loc.X, 2) + Math.Pow(loc.Y, 2)));
        }

        /// <summary>
        /// Add two Locations
        /// </summary>
        public static Location Add(Location a, Location b)
        {
            return new Location(a.X+b.X, a.Y+b.Y);
        }

        private void OnNewTick(Client client, NewTickPacket packet)
        {
            PlayerShootPacket shootPacket = Packet.Create<PlayerShootPacket>(PacketType.PLAYERSHOOT);
            shootPacket.Angle = (float)Math.PI;
            shootPacket.Position = Add(ToRect(shootPacket.Angle, 0.3f),client.PlayerData.Pos);
            shootPacket.BulletId = 1;
            shootPacket.ContainerType = 2824;
            shootPacket.Time = client.Time;
            Console.WriteLine(shootPacket);
            client.SendToServer(shootPacket);
            return;
            foreach(Entity entity in client.State.RenderedEntities)
            {
                ObjectStructure obj = GameData.Objects.ByID(entity.ObjectType);
                if (obj.Enemy)
                {
                    Console.WriteLine(obj.Name);
                    PlayerShootPacket p = new PlayerShootPacket();
                    //shootPacket.
                }
            }
        }
    }
}
