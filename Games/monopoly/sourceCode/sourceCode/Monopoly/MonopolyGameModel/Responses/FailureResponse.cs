using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonopolyGameModel.Responses
{
    public class FailureResponse : AResponse
    {
        private readonly String r_Reason;

        public String Reason { get {return this.r_Reason;}}

        public FailureResponse (): this(String.Empty){}
	    
        public FailureResponse(string Reason)
        {
            this.r_Reason = Reason;
        }
    }
}
