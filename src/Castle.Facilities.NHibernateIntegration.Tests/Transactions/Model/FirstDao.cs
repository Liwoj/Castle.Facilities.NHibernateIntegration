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

namespace Castle.Facilities.NHibernateIntegration.Tests.Transactions
{
	#region Using Directives

	using Castle.Services.Transaction;

	using NHibernate;

	using NUnit.Framework;

	#endregion

	[Transactional]
	public class FirstDao
	{
		private readonly ISessionManager _sessionManager;

		public FirstDao(ISessionManager sessionManager)
		{
			this._sessionManager = sessionManager;
		}

		[Transaction]
		public virtual Blog Create()
		{
			return this.Create("xbox blog");
		}

		[Transaction]
		public virtual Blog Create(string name)
		{
			using (var session = this._sessionManager.OpenSession())
			{
				var transaction = session.GetCurrentTransaction();

				Assert.IsNotNull(transaction);
				Assert.IsTrue(transaction.IsActive);

				var blog = new Blog();
				blog.Name = name;
				session.Save(blog);

				return blog;
			}
		}

		[Transaction]
		public virtual void Delete(string name)
		{
			using (var session = this._sessionManager.OpenSession())
			{
				var transaction = session.GetCurrentTransaction();

				Assert.IsNotNull(transaction);

				session.Delete("from Blog b where b.Name ='" + name + "'");
				session.Flush();
			}
		}

		public virtual void AddBlogRef(BlogRef blogRef)
		{
			using (var session = this._sessionManager.OpenSession())
			{
				session.Save(blogRef);
			}
		}

		[Transaction]
		public virtual Blog CreateStateless()
		{
			return this.CreateStateless("xbox blog");
		}

		[Transaction]
		public virtual Blog CreateStateless(string name)
		{
			using (var session = this._sessionManager.OpenStatelessSession())
			{
				var transaction = session.GetCurrentTransaction();

				Assert.IsNotNull(transaction);
				Assert.IsTrue(transaction.IsActive);

				var blog = new Blog();
				blog.Name = name;
				session.Insert(blog);
				return blog;
			}
		}

		[Transaction]
		public virtual void DeleteStateless(string name)
		{
			using (var session = this._sessionManager.OpenStatelessSession())
			{
				var transaction = session.GetCurrentTransaction();

				Assert.IsNotNull(transaction);

				session.Delete("from Blog b where b.Name ='" + name + "'");
			}
		}

		public virtual void AddBlogRefStateless(BlogRef blogRef)
		{
			using (var session = this._sessionManager.OpenStatelessSession())
			{
				session.Insert(blogRef);
			}
		}
	}
}