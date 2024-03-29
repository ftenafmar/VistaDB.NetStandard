﻿using VistaDB.Engine.Internal;

namespace VistaDB.Engine.Core.Scripting
{
  internal class UnaryMinus : Unary
  {
    internal UnaryMinus(string name, int groupId)
      : base(name, groupId)
    {
    }

    protected override void OnExecute(ProcedureCode pcode, int entry, Connection connection, DataStorage contextStorage, Row contextRow, ref bool bypassNextGroup, Row rowResult)
    {
      Row.Column column = -pcode[entry].ResultColumn;
    }

    internal override Signature DoCloneSignature()
    {
      Signature signature = (Signature) new UnaryMinus(new string(this.Name), this.Group);
      signature.Entry = this.Entry;
      return signature;
    }
  }
}
