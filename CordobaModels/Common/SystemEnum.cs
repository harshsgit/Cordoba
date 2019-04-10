using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaModels
{
    public class SystemEnum
    {
        #region enum Declaration

        public enum UserType
        {
            Admin = 1,
            Supplier = 2,
            Buyer = 3
            //Management = 2,
            //Broker = 3,
            //Client = 4,
            //Agent = 5
        }

        //public enum 
        public enum StatingDealStatus
        {
            Proceed = 1,
            Discard = 2,
            OverWrite = 3
        }

        public enum RewardType
        {
            Star = 1,
            Medal = 2
        }

        public enum RewardTypeStarValue
        {
            Star1 = 1,
            Star2 = 2,
            Star3 = 3,
            Star4 = 4,
            Star5 = 5
        }

        public enum RewardTypeMedalValue
        {
            Gold = 1,
            Silver = 2,
            Bronze = 3
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets or sets the ID value.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the Name value.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Description value.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Description value.
        /// </summary>
        public string DisplayName { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Get the Enum Name from Enum Value
        /// </summary>
        /// <param name="objEnumType">Enum Type like typeof(EnumType)</param>
        /// <param name="value">enum value</param>
        /// <returns>string - Name of Enum</returns>
        public static string GetEnumName(Type objEnumType, int value)
        {
            SystemEnumList lstEnum = GetEnumList(objEnumType);
            SystemEnum objSystemEnum;
            objSystemEnum = lstEnum.Find(delegate(SystemEnum systemEnum)
            {
                return systemEnum.ID == value;
            });

            if (objSystemEnum != null)
            {
                return objSystemEnum.Name;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get the Enum Name from Enum Value
        /// </summary>
        /// <param name="objEnumType">Enum Type like typeof(EnumType)</param>
        /// <param name="value">enum value</param>
        /// <returns>string - Name of Enum</returns>
        public static string GetEnumDescription(Type objEnumType, int value)
        {
            SystemEnumList lstEnum = GetEnumList(objEnumType);
            SystemEnum objSystemEnum;
            objSystemEnum = lstEnum.Find(delegate(SystemEnum systemEnum)
            {
                return systemEnum.ID == value;
            });

            if (objSystemEnum != null)
            {
                return objSystemEnum.Description;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get the Enum Name from Enum Value
        /// </summary>
        /// <param name="objEnumType">Enum Type like typeof(EnumType)</param>
        /// <param name="value">enum value</param>
        /// <returns>string - Name of Enum</returns>
        public static string GetEnumDisplayName(Type objEnumType, int value)
        {
            SystemEnumList lstEnum = GetEnumList(objEnumType);
            SystemEnum objSystemEnum;
            objSystemEnum = lstEnum.Find(delegate(SystemEnum systemEnum)
            {
                return systemEnum.ID == value;
            });

            if (objSystemEnum != null)
            {
                return objSystemEnum.DisplayName;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Get the SystemEnumList from given Enum type
        /// </summary>
        /// <param name="objEnumType">Enum Type like typeof(EnumType)</param>
        /// <returns>List of Enum with Name Value pair</returns>
        public static SystemEnumList GetEnumList(Type objEnumType)
        {
            Array values = Enum.GetValues(objEnumType);
            SystemEnumList lstEnum = new SystemEnumList();
            SystemEnum objEnum;
            for (int i = 0; i < values.Length; i++)
            {
                objEnum = new SystemEnum();
                objEnum.ID = values.GetValue(i).GetHashCode();
                objEnum.Name = Convert.ToString(values.GetValue(i), CultureInfo.CurrentCulture).Replace("_", " ");
                objEnum.Description = string.Empty;
                objEnum.DisplayName = string.Empty;

                var memInfo = objEnumType.GetMember(Convert.ToString(values.GetValue(i), CultureInfo.CurrentCulture));
                var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes.Count() > 0)
                {
                    objEnum.Description = ((DescriptionAttribute)attributes[0]).Description;
                }

                var memInfoDisplayName = objEnumType.GetMember(Convert.ToString(values.GetValue(i), CultureInfo.CurrentCulture));
                var attributesDisplayName = memInfoDisplayName[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                if (attributesDisplayName.Count() > 0)
                {
                    objEnum.DisplayName = ((DisplayAttribute)attributesDisplayName[0]).Name;
                }

                lstEnum.Add(objEnum);
            }

            return lstEnum;
        }

        public static T GetEnumValue<T>(string str) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            T val = ((T[])Enum.GetValues(typeof(T)))[0];
            if (!string.IsNullOrEmpty(str))
            {
                foreach (T enumValue in (T[])Enum.GetValues(typeof(T)))
                {
                    if (enumValue.ToString().ToUpper().Equals(str.ToUpper()))
                    {
                        val = enumValue;
                        break;
                    }
                }
            }

            return val;
        }

        #endregion

        /// <summary>
        /// Used to get System EnumList
        /// <CreatedBy>Karan Trivedi</CreatedBy>
        /// <CreatedDate> 21 Feb 2017 </CreatedDate>
        /// </summary>
        public class SystemEnumList : List<SystemEnum>
        {
        }
    }
}
