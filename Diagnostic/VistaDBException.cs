﻿using System;
using System.Data.Common;
using System.Runtime.Serialization;
using System.Security;

namespace VistaDB.Diagnostic
{
  [Serializable]
  public class VistaDBException : DbException
  {
    private static readonly Errors errors = new Errors();
    private static char[] trimmingArray = new char[3]{ '\r', '\n', ' ' };

    internal VistaDBException(Exception e, int errorId, string hint)
      : base(VistaDBException.CreateMessage(errorId, hint, e), e)
    {
      this.HResult = errorId;
    }

    internal VistaDBException(int errorId)
      : this((Exception) null, errorId, "")
    {
    }

    internal VistaDBException(int errorId, string hint)
      : this((Exception) null, errorId, hint)
    {
    }

    internal VistaDBException(Exception e, int errorId)
      : this(e, errorId, "")
    {
    }

    protected VistaDBException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
    }

    public override string Message
    {
      get
      {
        if (this.InnerException == null)
          return base.Message;
        if (!(this.InnerException is VistaDBException))
          return this.GetNonVistaDBExceptionMessage(this.InnerException) + base.Message;
        return this.InnerException.Message + base.Message;
      }
    }

    private string GetNonVistaDBExceptionMessage(Exception e)
    {
      string str = string.Empty;
      for (Exception exception = e; exception != null; exception = exception.InnerException)
        str = exception.Message + "\r\n" + str;
      return str.Trim(VistaDBException.trimmingArray);
    }

    public string LevelMessage
    {
      get
      {
        return base.Message.Trim(VistaDBException.trimmingArray);
      }
    }

    private static string CreateMessage(int errorId, string hint, Exception parentException)
    {
      return (parentException is VistaDBException ? "" : "\r\n") + VistaDBException.errors.GetMessage(errorId) + " " + hint + "\r\n";
    }

    public int ErrorId
    {
      get
      {
        return this.HResult;
      }
    }

    public bool Contains(long errorId)
    {
      for (Exception innerException = this.InnerException; innerException != null; innerException = innerException.InnerException)
      {
        if (innerException is VistaDBException && (long) ((VistaDBException) innerException).ErrorId == errorId)
          return true;
      }
      return false;
    }
  }
}
