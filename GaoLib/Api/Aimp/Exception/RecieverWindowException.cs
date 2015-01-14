namespace GaoLib.Api.Aimp.Exception
{
    [System.Serializable]
    public class RecieverWindowException : AimpException
    {
        public RecieverWindowException() { }
        public RecieverWindowException(string message) : base(message) { }
        public RecieverWindowException(string message, System.Exception inner) : base(message, inner) { }
        protected RecieverWindowException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
