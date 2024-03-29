﻿using VistaDB.Engine.Internal;

namespace VistaDB.Engine.SQL.Signatures
{
  internal class DatabaseIdVariable : SystemVariable
  {
    internal DatabaseIdVariable(SQLParser parser)
      : base(parser)
    {
      this.dataType = VistaDBType.UniqueIdentifier;
    }

    protected override IColumn InternalExecute()
    {
      ((IValue) this.result).Value = (object) this.parent.Database.VersionGuid;
      return this.result;
    }

    public override SignatureType OnPrepare()
    {
      return this.signatureType;
    }

    protected override bool InternalGetIsChanged()
    {
      return false;
    }
  }
}
