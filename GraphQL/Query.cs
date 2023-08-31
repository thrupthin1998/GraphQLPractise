using System;
using GraphQLPractise.Data;
using GraphQLPractise.Entities;

namespace GraphQLPractise.GraphQL
{
	public class Query
	{
		public IQueryable<User> GetUsers([Service] ApplicationDbContext context) =>
			context.User;

		public User? GetUserById([Service] ApplicationDbContext context, int? id, string? userName)
		{
			if(id != null)
			{
				return context.User.FirstOrDefault(x => x.Id == id);
			}
			else if(!string.IsNullOrEmpty(userName))
			{
				return context.User.FirstOrDefault(x => x.UserName == userName);
			}
			else
			{
				return context.User.First();
			}
		}
	}
}

