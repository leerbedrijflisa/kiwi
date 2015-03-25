using System;
using System.ComponentModel.DataAnnotations;
using Lisa.Kiwi.Web.Models;

namespace Lisa.Kiwi.Web.Utils
{
    public static class EnumDisplayNames
    {
        public static string GetDisplayName(this Enum value)
        {
            var type = value.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException(String.Format("Type '{0}' is not Enum", type));
            }

            var members = type.GetMember(value.ToString());
            if (members.Length == 0)
            {
                throw new ArgumentException(String.Format("Member '{0}' not found in type '{1}'", value, type.Name));
            }

            var member = members[0];
            var attributes = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (attributes.Length == 0)
            {
                throw new ArgumentException(String.Format("'{0}.{1}' doesn't have DisplayAttribute", type.Name, value));
            }

            var attribute = (DisplayAttribute)attributes[0];
            return attribute.GetName();
        }

        public static string GetStatusDisplayNameFromMetadata(this Enum value)
        {
            if (!Enum.IsDefined(typeof(StatusNameMetadata), value.ToString()))
            {
                throw new Exception(String.Format("{0} is no enum property.", value));
            }
            Enum enumVal = (StatusNameMetadata)Enum.Parse(typeof(StatusNameMetadata), value.ToString());

            return enumVal.GetDisplayName();
        }

        public static string GetReportTypeDisplayNameFromMetadata(this Enum value)
        {
            if (!Enum.IsDefined(typeof(ReportTypeMetadata), value.ToString()))
            {
                throw new Exception(String.Format("{0} is no enum property.", value));
            }
            Enum enumVal = (ReportTypeMetadata)Enum.Parse(typeof(ReportTypeMetadata), value.ToString());

            return enumVal.GetDisplayName();
        }
    }
}