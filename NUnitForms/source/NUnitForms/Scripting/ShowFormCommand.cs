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

using System;
using System.Reflection;
using System.Windows.Forms;

namespace NUnit.Extensions.Forms
{
    ///<summary>
    /// A <see cref="Command"/> which shows a form.
    ///</summary>
    public class ShowFormCommand : Command
    {
        ///<summary>
        /// Constructs a new <see cref="ShowFormCommand"/>.
        ///</summary>
        ///<param name="controlName"></param>
        public ShowFormCommand(string controlName) : base(controlName, null, null){}

        ///<summary>
        /// Executes this command, showing the form.
        ///</summary>
        ///<param name="speed">This parameter is ignored; there is no delay when showing a form.</param>
        ///<exception cref="FormsTestAssertionException">Thrown if the form cannot be loaded.</exception>
        public override void Execute(Speed speed)
        {
            //no delay for speed on showing a form.
            try
            {
                Type formType = Type.GetType(ControlName.Trim());
                if(formType == null)
                {
                    foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        formType = assembly.GetType(ControlName.Trim());
                        if(formType != null)
                            break;

						formType = assembly.GetType(ControlName.Substring(0, ControlName.IndexOf(",")));
                        if(formType != null)
                            break;
                    }
                }

                Form formInstance = (Form) Activator.CreateInstance(formType);
                formInstance.Show();
            }
            catch
            {
                throw new FormsTestAssertionException("Form display failed. Be sure to load the form assembly first.");
            }
        }
    }
}