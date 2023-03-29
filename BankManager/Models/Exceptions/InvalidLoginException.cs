using System;
using System.Runtime.Serialization;

namespace BankManager.Models.Exceptions;

public class InvalidLoginException : Exception
{
    public InvalidLoginException() { }
    public InvalidLoginException(string p_message) : base(p_message) { }
    public InvalidLoginException(string p_message, Exception p_inner) : base(p_message, p_inner) { }
    protected InvalidLoginException(SerializationInfo p_info,
        StreamingContext  p_context) : base(p_info, p_context) { }
}