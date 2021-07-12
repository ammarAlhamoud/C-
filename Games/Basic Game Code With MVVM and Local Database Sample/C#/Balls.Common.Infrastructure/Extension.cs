using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Balls.Common.Infrastructure.UI.Navigation;
using Balls.Common.Infrastructure.UI.Settings;
using Microsoft.Xna.Framework;

namespace Balls.Common.Infrastructure
{
    public static class Extension
    {
        /// <summary>
        /// Maintains Exception counts
        /// </summary>
        public static int IsExceptionOccurred = 0;

        /// <summary>
        /// Returns no. of query string parameter in the uri.
        /// '/' consider as query string parameter separator
        /// </summary>
        /// <param name="uri">intput uri</param>
        /// <returns>parapmeter count</returns>
        public static int UriQueryParameterCount(this string uri)
        {
            if (uri.IndexOf("?") > 0)
            {
                return uri.Substring(uri.IndexOf("?") + 1).Split('/').Length;
            }
            return 0;
        }

        /// <summary>
        /// Returns all paramerters into string array
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string[] UriQueryParameters(this string uri)
        {
            return uri.Substring(uri.IndexOf("?") + 1).Split('/');
        }

        /// <summary>
        /// Converts the specified System.String representation of a number to an equivalent
        /// 32-bit signed integer
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int ToInt(this string val)
        {
            int result = 0;
            try { result = Convert.ToInt32(val); }
            catch (Exception) { }

            return result;
        }

        /// <summary>
        ///  Navigates to the content specified by the uniform resource identifier (URI).
        /// </summary>
        /// <param name="uri"></param>
        public static void Navigate(this string uri)
        {
            (new NavigationService()).Navigate(uri);
        }

        /// <summary>
        /// This method is used to remove the most recent entry from the back stack,
        /// </summary>
        /// <param name="val"></param>
        public static void RemoveBackEntry(this string val)
        {
            (new NavigationService()).RemoveBackEntry();
        }

        /// <summary>
        /// This method is used to remove all entries from the back stack,
        /// </summary>
        /// <param name="val"></param>
        public static void RemoveAllBackEntry(this string val)
        {
            (new NavigationService()).RemoveAllBackEntry();
        }

        /// <summary>
        /// Set an entry to the dictionary for the key-value pair. 
        /// If key already exist, remove that and insert this value
        /// </summary>
        /// <param name="key">The key for the entry to be stored.</param>
        /// <param name="value">The value to be stored.</param>
        /// <returns></returns>
        public static bool IsoSetData(this string key, object value)
        {
            return IsolatedSettings.SetData(key, value);

        }

        /// <summary>
        /// Set an entry to the dictionary for the key-value pair. 
        /// If key already exist, it will not insert new value 
        /// </summary>
        /// <param name="key">The key for the entry to be stored.</param>
        /// <param name="value">The value to be stored.</param>
        /// <returns>Return true, if successfully inserted, otherwise return false.</returns>
        public static bool IsoSetDataIfNotExist(this string key, object value)
        {
            return IsolatedSettings.SetDataIfNotExist(key, value);

        }

        /// <summary>
        /// Determines if the application settings dictionary contains the specified key.
        /// </summary>
        /// <param name="key">The key for the entry to be located.</param>
        /// <returns>true if the dictionary contains the specified key; otherwise, false.</returns>
        public static bool IsoIsExist(this string key)
        {
            return IsolatedSettings.IsExist(key);
        }

        /// <summary>
        /// Removes the entry with the specified key.
        /// </summary>
        /// <param name="key">The key for the entry to be deleted.</param>
        /// <returns>true if the specified key was removed; otherwise, false.</returns>
        public static bool IsoRemove(this string key)
        {
            return IsolatedSettings.Remove(key);

        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the item to get or set.</param>
        /// <returns>The value associated with the specified key.</returns>
        public static object IsoGetData(this string key)
        {
            return IsolatedSettings.GetData(key);
        }

        /// <summary>
        /// This value is used for Page level data storing key across the application.
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string APPPageData(this string val)
        {
            return "APPPAGEDATA";
        }

        /// <summary>
        /// Determines if the application settings dictionary not contains the specified key.
        /// </summary>
        /// <param name="key">The key for the entry to be located.</param>
        /// <returns>true if the dictionarynot contains the specified key; otherwise, false.</returns>
        public static bool IsoIsNotExist(this string key)
        {
            return IsolatedSettings.IsNotExist(key);
        }

        /// <summary>
        /// Any object can save using this method, which helps to use store page level data
        /// </summary>
        /// <param name="pageData"></param>
        public static void SetPageData(this object pageData)
        {
            "".APPPageData().IsoSetData(pageData);
        }

        /// <summary>
        /// Remove the page level data
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool APPRemoveData(this string val)
        {
            "".APPPageData().IsoRemove();
            return true;
        }

        /// <summary>
        /// Compare a vector2 value comes between upper and lower limit.
        /// </summary>
        /// <param name="val">vector2 value</param>
        /// <param name="min">Lower limit of vector2</param>
        /// <param name="max">Upper limit of vector2</param>
        /// <returns>true if value within the limit, otherwise false</returns>
        public static bool IsWithinLimits(this Vector2 val, Vector2 min, Vector2 max)
        {
            return Vector2.Clamp(val, min, max) == val;
        }

        /// <summary>
        /// This methos add the whitespace in prepix of input string.
        /// </summary>
        /// <param name="scorecard"></param>
        /// <returns></returns>
        public static string ScoreCardSrting(long scorecard)
        {
            int l = 11 - (2 * scorecard.ToString().Length);
            string space = "";
            for (int i = 0; i < l; i++)
            {
                space += " ";
            }

            return space + scorecard.ToString();
        }

    }
}
