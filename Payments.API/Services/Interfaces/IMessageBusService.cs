namespace Payments.API.Services.Interfaces
{
    public interface IMessageBusService
    {
        void Publish(string queue, byte[] message);
    }
}
