using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Remotion.Web.UI.Controls
{
  /// <summary>
  /// A <see cref="TypeConverter"/> for <see cref="Badge"/>, used vor ViewState-serialization.
  /// </summary>
  public class BadgeConverter : ExpandableObjectConverter
  {
    public override bool CanConvertFrom (ITypeDescriptorContext context, Type sourceType)
    {
      if (context == null && sourceType == typeof (string))
        return true;

      return base.CanConvertFrom (context, sourceType);
    }

    public override bool CanConvertTo (ITypeDescriptorContext context, Type destinationType)
    {
      if (destinationType == typeof (string))
        return true;

      return base.CanConvertTo (context, destinationType);
    }

    public override object ConvertFrom (ITypeDescriptorContext context, CultureInfo culture, object value)
    {
      if (value == null)
        return null;

      if (value is string stringValue)
      {
        var badge = new Badge();
        if (stringValue != string.Empty)
        {
          var valueParts = stringValue.Split (new[] { '\0' }, 2);
          badge.Value = valueParts[0];
          badge.Description = valueParts[1];
        }

        return badge;
      }

      return base.ConvertFrom (context, culture, value);
    }

    public override object ConvertTo (ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
      if (destinationType == typeof (string))
      {
        if (value == null)
          return null;

        if (value is Badge badge)
        {
          if (context == null) // Required to circumvent the Designer
          {
            if (Badge.ShouldSerialize (badge))
            {
              var serializedValue = new StringBuilder();
              serializedValue.Append (badge.Value).Append ('\0');
              serializedValue.Append (badge.Description);
              return serializedValue.ToString();
            }

            return string.Empty;
          }

          return badge.Value;
        }
      }

      return base.ConvertTo (context, culture, value, destinationType);
    }
  }
}