#region Copyright (c) 2003-2005, Luke T. Maxon

/********************************************************************************************************************
'
' Copyright (c) 2003-2005, Luke T. Maxon
' All rights reserved.
' 
' Redistribution and use in source and binary forms, with or without modification, are permitted provided
' that the following conditions are met:
' 
' * Redistributions of source code must retain the above copyright notice, this list of conditions and the
' 	following disclaimer.
' 
' * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and
' 	the following disclaimer in the documentation and/or other materials provided with the distribution.
' 
' * Neither the name of the author nor the names of its contributors may be used to endorse or 
' 	promote products derived from this software without specific prior written permission.
' 
' THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
' WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
' PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
' ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
' LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
' INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
' OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN
' IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
'
'*******************************************************************************************************************/

#endregion

using System.Collections;
using System.Collections.Generic;

namespace NUnit.Extensions.Forms.Recorder
{
    ///<summary>
    /// The default abstract base class for all collapsing processors.
    ///</summary>
    public abstract class CollapsingProcessor : ICollapsingProcessor
    {
		/// <summary>
		/// Process the given actions, possibly collapsing adjacent ones.
		/// </summary>
		/// <param name="actions">The list of actions to process.</param>
		/// <returns>The collapsed list of actions.</returns>
		public ICollection<Action> Process(ICollection<Action> actions)
        {
			List<Action> list = new List<Action>();

            Action lastAdded = null;

            foreach(Action action in actions)
            {
                if(Collapse(lastAdded, action))
                {
                    list.RemoveAt(list.Count - 1);
                }

                lastAdded = action;
                list.Add(lastAdded);
            }

            return list;
        }

    	/// <summary>
    	/// Returns true if the given actions can be collapsed.
    	/// </summary>
    	/// <param name="action1">The earlier event to test.</param>
    	/// <param name="action2">The latter event to test.</param>
    	/// <returns>True if these events can be collapsed; else false.</returns>
    	public abstract bool CanCollapse(EventAction action1, EventAction action2);

    	private bool Collapse(object action1, object action2)
        {
            EventAction first = action1 as EventAction;
            EventAction second = action2 as EventAction;
            if(first == null)
            {
                return false;
            }
            if(second == null)
            {
                return false;
            }
            return CanCollapse(first, second);
        }

    }
}