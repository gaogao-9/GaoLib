namespace GaoLib.Api.Aimp.Exception
{
    [System.Serializable]
    public class FileMappingException : AimpException
    {
        public FileMappingException() { }
        public FileMappingException(string message) : base(message) { }
        public FileMappingException(string message, System.Exception inner) : base(message, inner) { }
        protected FileMappingException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
