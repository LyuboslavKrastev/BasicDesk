using System;

namespace BasicDesk.App.Helpers.Messages
{
    [Serializable]
    public class MessageModel
    {
        public MessageType Type { get; set; }

        public string Message { get; set; }
    }
}
