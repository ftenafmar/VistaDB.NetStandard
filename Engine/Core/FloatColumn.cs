﻿using System;

namespace VistaDB.Engine.Core
{
  internal class FloatColumn : Row.Column
  {
    private static int DoubleSize = 8;

    internal static double CustValue(Row.Column col)
    {
      switch (col.InternalType)
      {
        case VistaDBType.NChar:
          return double.Parse((string) col.Value, CrossConversion.NumberFormat);
        case VistaDBType.TinyInt:
          return (double) (byte) col.Value;
        case VistaDBType.SmallInt:
          return (double) (short) col.Value;
        case VistaDBType.Int:
          return (double) (int) col.Value;
        case VistaDBType.BigInt:
          return (double) (long) col.Value;
        case VistaDBType.Real:
          return (double) (float) col.Value;
        case VistaDBType.Decimal:
          return Decimal.ToDouble((Decimal) col.Value);
        default:
          return (double) col.Value;
      }
    }

    internal FloatColumn()
      : base((object) null, VistaDBType.Float, FloatColumn.DoubleSize)
    {
    }

    internal FloatColumn(double val)
      : base((object) val, VistaDBType.Float, FloatColumn.DoubleSize)
    {
    }

    internal FloatColumn(FloatColumn col)
      : base((Row.Column) col)
    {
    }

    internal override object DummyNull
    {
      get
      {
        return (object) double.MinValue;
      }
    }

    public override object MaxValue
    {
      get
      {
        return (object) double.MaxValue;
      }
    }

    public override Type SystemType
    {
      get
      {
        return typeof (double);
      }
    }

    public override object Value
    {
      set
      {
        base.Value = value == null ? value : (object) (double) value;
      }
    }

    protected override Row.Column OnDuplicate(bool padRight)
    {
      return (Row.Column) new FloatColumn(this);
    }

    internal override int ConvertToByteArray(byte[] buffer, int offset, Row.Column precedenceColumn)
    {
      Array.Copy((Array) BitConverter.GetBytes((double) this.Value), 0, (Array) buffer, offset, FloatColumn.DoubleSize);
      return offset + FloatColumn.DoubleSize;
    }

    internal override int ConvertFromByteArray(byte[] buffer, int offset, Row.Column precedenceColumn)
    {
      this.val = (object) BitConverter.ToDouble(buffer, offset);
      return offset + FloatColumn.DoubleSize;
    }

    protected override long Collate(Row.Column col)
    {
      double num1 = (double) this.Value;
      double num2 = (double) col.Value;
      return num1 > num2 ? 1L : (num1 < num2 ? -1L : 0L);
    }

    protected override Row.Column DoUnaryMinus()
    {
      this.Value = (object) -(double) this.Value;
      return (Row.Column) this;
    }

    protected override Row.Column DoMinus(Row.Column col)
    {
      this.Value = (object) ((double) this.Value - FloatColumn.CustValue(col));
      return (Row.Column) this;
    }

    protected override Row.Column DoPlus(Row.Column col)
    {
      this.Value = (object) ((double) this.Value + FloatColumn.CustValue(col));
      return (Row.Column) this;
    }

    protected override Row.Column DoMultiplyBy(Row.Column col)
    {
      this.Value = (object) ((double) this.Value * FloatColumn.CustValue(col));
      return (Row.Column) this;
    }

    protected override Row.Column DoDivideBy(Row.Column denominator)
    {
      this.Value = (object) ((double) this.Value / FloatColumn.CustValue(denominator));
      return (Row.Column) this;
    }

    protected override Row.Column DoGetDividedBy(Row.Column numerator)
    {
      this.Value = (object) (FloatColumn.CustValue(numerator) / (double) this.Value);
      return (Row.Column) this;
    }

    protected override Row.Column DoModBy(Row.Column denominator)
    {
      this.Value = (object) ((double) this.Value % FloatColumn.CustValue(denominator));
      return (Row.Column) this;
    }

    protected override Row.Column DoGetModBy(Row.Column numerator)
    {
      this.Value = (object) (FloatColumn.CustValue(numerator) % (double) this.Value);
      return (Row.Column) this;
    }
  }
}
