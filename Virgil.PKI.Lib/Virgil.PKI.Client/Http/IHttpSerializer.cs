namespace Virgil.PKI.Http
{
    public interface IRequestResponseSerializer
    {
        void SerializeRequest(IRequest request, object body);
        TResponse DeserializeResponse<TResponse>(IResponse response);
    }
}