﻿using VistaDB.Engine.Internal;

namespace VistaDB.Engine.Core.Scripting
{
  internal class Minus : Summation
  {
    internal Minus(string name, int groupId, int unaryOffset)
      : base(name, groupId, unaryOffset)
    {
    }

    protected override void OnExecute(ProcedureCode pcode, int entry, Connection connection, DataStorage contextStorage, Row contextRow, ref bool bypassNextGroup, Row rowResult)
    {
      Row.Column column = -pcode[entry + 1].ResultColumn;
      base.OnExecute(pcode, entry, connection, contextStorage, contextRow, ref bypassNextGroup, rowResult);
    }

    internal override Signature DoCloneSignature()
    {
      Signature signature = (Signature) new Minus(new string(this.Name), this.Group, this.unaryOffset);
      signature.Entry = this.Entry;
      return signature;
    }
  }
}
