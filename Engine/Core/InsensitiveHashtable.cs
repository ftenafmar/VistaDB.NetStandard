﻿using System.Collections;
using System.Globalization;

namespace VistaDB.Engine.Core
{
  internal class InsensitiveHashtable : Hashtable
  {
    private CultureInfo culture = CultureInfo.InvariantCulture;

    public InsensitiveHashtable()
    {
    }

    public InsensitiveHashtable(int capacity)
      : base(capacity)
    {
    }

    public override bool ContainsKey(object key)
    {
      return base.ContainsKey((object) ((string) key).ToUpper(this.culture));
    }

    protected override int GetHash(object key)
    {
      return base.GetHash((object) ((string) key).ToUpper(this.culture));
    }

    protected override bool KeyEquals(object item, object key)
    {
      return base.KeyEquals((object) ((string) item).ToUpper(this.culture), (object) ((string) key).ToUpper(this.culture));
    }

    public override IDictionaryEnumerator GetEnumerator()
    {
      return (IDictionaryEnumerator) this.Values.GetEnumerator();
    }
  }
}
