namespace GaoLib.Api.Aimp.Exception
{
    [System.Serializable]
    public class MessageTimeoutException : RecieverWindowException
    {
        public MessageTimeoutException() { }
        public MessageTimeoutException(string message) : base(message) { }
        public MessageTimeoutException(string message, System.Exception inner) : base(message, inner) { }
        protected MessageTimeoutException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
