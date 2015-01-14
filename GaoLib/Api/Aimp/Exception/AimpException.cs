namespace GaoLib.Api.Aimp.Exception
{
    [System.Serializable]
    public class AimpException : System.Exception
    {
        public AimpException() { }
        public AimpException(string message) : base(message) { }
        public AimpException(string message, System.Exception inner) : base(message, inner) { }
        protected AimpException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
