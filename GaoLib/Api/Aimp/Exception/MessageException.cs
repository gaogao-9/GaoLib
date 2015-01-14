namespace GaoLib.Api.Aimp.Exception
{
    [System.Serializable]
    public class MessageException : AimpException
    {
        public MessageException() { }
        public MessageException(string message) : base(message) { }
        public MessageException(string message, System.Exception inner) : base(message, inner) { }
        protected MessageException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
