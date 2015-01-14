namespace GaoLib.Api.Aimp.Exception
{
    [System.Serializable]
    public class RemoteWindowNotFoundException : AimpException
    {
        public RemoteWindowNotFoundException() { }
        public RemoteWindowNotFoundException(string message) : base(message) { }
        public RemoteWindowNotFoundException(string message, System.Exception inner) : base(message, inner) { }
        protected RemoteWindowNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
