﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Media;
using OpenControls;

namespace SimpleDemo
{
    public enum Genders
    {
        Male,
        Female
    } ;

    #region Mass
    [TypeConverter(typeof (MassConverter))]
    public class Mass
    {
        public double Value { get; set; }

        public static Mass Parse(string s)
        {
            s = s.Replace(',', '.').Trim();
            var r = new Regex(@"[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?");
            Match m = r.Match(s);
            if (!m.Success) return null;
            double value = double.Parse(m.Groups[0].Value, CultureInfo.InvariantCulture);
            // string unit = m.Groups[1].Value;
            return new Mass {Value = value};
        }

        public override string ToString()
        {
            return String.Format("{0:N0} kg", Value, CultureInfo.InvariantCulture);
        }
    }

    public class MassConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof (string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                return Mass.Parse((string) value);
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
    #endregion

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }
        public Mass Weight { get; set; }
        public Genders Gender { get; set; }
        
        public DateTime BirthDate { get; set; }
        public Color HairColor { get; set; }
        public Brush SkinColor { get; set; }

        public bool IsSmoking { get; set; }
        public bool HasBicycle { get; set; }

        [Browsable(false)]
        public bool HasCar { get; set; }

        [Optional("HasCar")]
        public string Car { get; set; }
        //public string Model { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1}, {2}, {3}, {4}",FirstName,LastName, Age, Weight, Gender);
        }
    }
}