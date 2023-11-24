using LeafNet.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LeafNet
{
    public class Tester : SingletonBase<Tester>
    {
        public void StartServer()
        {
            ServerEntity serverEntity = new ServerEntity();
            serverEntity.clientConnectEvent += (session) => NetLogger.GetInstance().Log("客户端连接事件调用");
            serverEntity.clientDisconnectEvent += (session) => NetLogger.GetInstance().Log("客户端断开事件调用");
            serverEntity.StartServer();
        }
        public void MessageTest()
        {
            ByteArray byteArray = new ByteArray();

            HeartMsg msg1 = new HeartMsg();
            var temp = msg1.SerializeWithHead();
            byteArray.Write(temp, 0, temp.Length);

            msg1.SerializeToByteArray(byteArray);
            msg1.SerializeToByteArray(byteArray);

            TestMsg msg2 = new TestMsg();
            msg2.SerializeToByteArray(byteArray);

            while (true)
            {
                MessageBase msg = MessageBase.DeserializeFromByteArray(byteArray);
                if (msg != null)
                {
                    msg.HandleProcess(null, null);
                }
                else
                {
                    break;
                }
            }
        }

        public void ServerAndClientTest()
        {
            ServerEntity serverEntity = new ServerEntity();

            Thread serverThread = new Thread(serverEntity.StartServer);
            serverThread.Start();

            ClientEntity clientEntity = new ClientEntity();
            clientEntity.BeginConnect(() =>
            {
                NetLogger.GetInstance().Log("连接成功");
                clientEntity.SendMessageToServer(new TestMsg());
            });

            while (true)
            {
                clientEntity.FrameUpdate(10);
            }
        }
    }
}
