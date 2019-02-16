using BasicDesk.App.Helpers.Messages;

namespace BasicDesk.App.Common.Interfaces
{
    public interface IAlerter
    {
        void AddMessage(MessageType type, string message);
    }
}