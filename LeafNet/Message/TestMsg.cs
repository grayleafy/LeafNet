using LeafNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace LeafNet
{
    [Serializable]
    public class TestMsg : MessageBase
    {
        public override void HandleProcess(Session senderSession, NetworkEntity networkEntity)
        {
            NetLogger.GetInstance().Log("testMsg, 发送者" + senderSession.socket.RemoteEndPoint.ToString());
            if (networkEntity.GetType() == typeof(ServerEntity))
            {
                networkEntity.SendMessage(new TestMsg(), senderSession);
            }
        }
    }
}
