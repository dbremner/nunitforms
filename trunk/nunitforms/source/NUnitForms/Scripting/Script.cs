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
using System.Collections;
using System.Collections.Generic;

namespace NUnit.Extensions.Forms
{
	///<summary>
	/// A <see cref="Script"/> is a list of <see cref="ICommand"/>s that can be played back.
	///</summary>
	public class Script : IEnumerable<ICommand>
	{
		private List<ICommand> commands = new List<ICommand>();

		private ScriptPlayer player;

		///<summary>
		/// Gets or sets the <see cref="Player"/> associated with this <see cref="Script"/>.
		///</summary>
		public ScriptPlayer Player
		{
			get { return player; }
			set { player = value; }
		}

		///<summary>
		/// Returns the index of the given <see cref="ICommand">command</see> in this script.
		///</summary>
		public int IndexOf(ICommand command)
		{
			return commands.IndexOf(command);
		}

		///<summary>
		/// Adds the given <see cref="ICommand">command</see> to this script.
		///</summary>
		///<param name="command"></param>
		public void Add(ICommand command)
		{
			commands.Add(command);
			command.Script = this;
		}

		///<summary>
		/// Enumerables all the <see cref="ICommand">commands</see> in this script.
		///</summary>
		///<returns></returns>
		public IEnumerator<ICommand> GetEnumerator()
		{
			foreach (ICommand command in commands)
			{
				if (command is NullCommand)
					continue;

				yield return command;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}