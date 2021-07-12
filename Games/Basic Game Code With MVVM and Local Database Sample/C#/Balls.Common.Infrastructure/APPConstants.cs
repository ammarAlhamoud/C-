using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Balls.Common.Infrastructure
{
    public static class APPConstants
    {
        /// <summary>
        /// Application Owner Email ID, this is used in about use and other email option section.
        /// </summary>
        public static string AppOwnerEmailID = "arthanarieaswaran@gmail.com";

        /// <summary>
        /// Application Owner Name, this is used in about use and other places.
        /// </summary>
        public static string AppOwner = "ARTHANARIEASWARAN";


        //By changeing this corresponding update as to be done in the WPManfeast file and appInfo file. 
        public static string ApplicationName = "Balls";

        /// <summary>
        /// This is taken from the assembly information
        /// </summary>
        public static string ApplicationVertion;// = "1.0.0.0";

        /// <summary>
        /// Returns Application name and Version no. in particular format.
        /// </summary>
        public static string ApplicationNameAndVersion
        {
            get
            {
                return string.Format("{0} [v{1}]: ", ApplicationName, ApplicationVertion);
            }
        }

        /// <summary>
        /// Description of About Page
        /// </summary>
        public static string AboutDescription = " BALLs is a game application, it has two types of games with 5 levels each." + Environment.NewLine +

                                                    "1. Ball Counter" + Environment.NewLine +
                                                    "   Player has to select the moving balls - score will calculate based on how fast it complete." + Environment.NewLine +

                                                     Environment.NewLine +

                                                    "2. Ball Balance" + Environment.NewLine +
                                                    "   Player has to balance the ball and place it in boat object - score will calculate based on how fast it complete." + Environment.NewLine +
                                                    "NOTE:" + Environment.NewLine +
                                                    "   Please write few words to me about this app.";

    }
}
