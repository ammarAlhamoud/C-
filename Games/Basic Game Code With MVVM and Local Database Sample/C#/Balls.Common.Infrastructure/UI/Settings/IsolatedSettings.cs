using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.IsolatedStorage;
using System.Collections;

namespace Balls.Common.Infrastructure.UI.Settings
{

    /// <summary>
    /// This is class taking care of Isolated operations of the application.
    /// </summary>
    public sealed class IsolatedSettings
    {
        private static readonly IsolatedStorageSettings _isolatedStorageSetting = IsolatedStorageSettings.ApplicationSettings;

        /// <summary>
        /// Store the list of objects, if key is already exit, it remove old value and insert the new value
        /// </summary>
        /// <param name="listOfData"></param>
        /// <returns></returns>
        public static bool StoreDataDeleteIfExist(IDictionary listOfData)
        {
            foreach (var item in listOfData.Keys)
            {
                if (_isolatedStorageSetting.Contains(item.ToString().ToUpper()))
                {
                    _isolatedStorageSetting.Remove(item.ToString().ToUpper());
                }
                _isolatedStorageSetting.Add(item.ToString().ToUpper(), listOfData[item]);
            }
            return true;
        }

        /// <summary>
        /// Clear the Isolated settings
        /// </summary>
        /// <returns></returns>
        public static bool DeleteAllStoredData()
        {
            _isolatedStorageSetting.Clear();

            return true;
        }

        /// <summary>
        /// Return object value for a given key, if key is not avaible 'null' value will return
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetData(string key)
        {
            if (_isolatedStorageSetting.Contains(key.ToUpper()))
            {
                return _isolatedStorageSetting[key.ToUpper()];
            }
            return null;
        }

        /// <summary>
        /// Set an object with given key, if key already exist, it will replace
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetData(string key, object value)
        {
            _isolatedStorageSetting[key.ToUpper()] = value;
            return true;
        }

        /// <summary>
        /// Set an object with given key, if key is not exist.
        /// Return if is value is added/replaced, otherwise false.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetDataIfNotExist(string key, object value)
        {
            if (!_isolatedStorageSetting.Contains(key.ToUpper()))
            {
                _isolatedStorageSetting[key.ToUpper()] = value;
                return true;
            }

            return false;
        }
        /// <summary>
        /// Determines if the application settings dictionary contains the specified key.
        /// </summary>
        /// <param name="key">The key for the entry to be located.</param>
        /// <returns>true if the dictionary contains the specified key; otherwise, false.</returns>
        public static bool IsExist(string key)
        {
            return _isolatedStorageSetting.Contains(key.ToUpper());

        }
        /// <summary>
        /// Removes the entry with the specified key.
        /// </summary>
        /// <param name="key">The key for the entry to be deleted.</param>
        /// <returns>true if the specified key was removed; otherwise, false.</returns>
        public static bool Remove(string key)
        {
            if (IsExist(key))
            {
                return _isolatedStorageSetting.Remove(key.ToUpper());
            }
            return false;

        }
        /// <summary>
        /// Determines if the application settings dictionary not contains the specified key.
        /// </summary>
        /// <param name="key">The key for the entry to be located.</param>
        /// <returns>true if the dictionarynot contains the specified key; otherwise, false.</returns>
        public static bool IsNotExist(string key)
        {
            return !_isolatedStorageSetting.Contains(key.ToUpper());

        }



    }
}
