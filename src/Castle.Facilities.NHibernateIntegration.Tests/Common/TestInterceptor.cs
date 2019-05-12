#region License

//  Copyright 2004-2010 Castle Project - http://www.castleproject.org/
//  
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  
//      http://www.apache.org/licenses/LICENSE-2.0
//  
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
// 

#endregion

namespace Castle.Facilities.NHibernateIntegration.Tests.Common
{
	#region Using Directives

	using System.Collections;

	using NHibernate;
	using NHibernate.Type;

	#endregion
	/// <summary>
	///     An implementation of the <see cref="IInterceptor" /> interface for testing
	///     purposes.
	/// </summary>
	public class TestInterceptor : EmptyInterceptor
	{
		private bool _instantiationCall;
		private bool _onSaveCall;

		public bool ConfirmOnSaveCall()
		{
			return this._onSaveCall;
		}

		public bool ConfirmInstantiationCall()
		{
			return this._instantiationCall;
		}

		public void ResetState()
		{
			this._instantiationCall = false;
			this._onSaveCall = false;
		}

		#region IInterceptor Members

		public override int[] FindDirty(object entity, object id, object[] currentState, object[] previousState,
		                                string[] propertyNames, IType[] types)
		{
			return null;
		}

		public override object Instantiate(string clazz, object id)
		{
			this._instantiationCall = true;

			return null;
		}

		public override bool OnFlushDirty(object entity, object id, object[] currentState, object[] previousState,
		                                  string[] propertyNames, IType[] types)
		{
			return false;
		}

		public override bool OnLoad(object entity, object id, object[] state, string[] propertyNames,
		                            IType[] types)
		{
			return false;
		}

		public override bool OnSave(object entity, object id, object[] state, string[] propertyNames,
		                            IType[] types)
		{
			this._onSaveCall = true;

			return false;
		}

		public override void OnDelete(object entity, object id, object[] state, string[] propertyNames,
		                              IType[] types)
		{
		}

		public override void PreFlush(ICollection entities)
		{
		}

		public override void PostFlush(ICollection entities)
		{
		}

		#endregion
	}
}