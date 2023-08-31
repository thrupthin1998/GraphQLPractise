using System;
using System.Reflection;
using System.Text.Json;
using GraphQLPractise.Data;
using GraphQLPractise.Entities;

namespace GraphQLPractise.GraphQL
{
	public class Mutation
	{
		public User CreateUser(
			[Service] ApplicationDbContext context,
			User newUser
		)
		{
			context.User.Add(newUser);
			context.SaveChanges();

			return newUser;
		}

		public User UpdateUser(
			[Service] ApplicationDbContext context,
			int id,
			User? updatedValues
		)
		{
			User user = context.User.Find(id);

			//if (user == null)
			//{
			//	throw new KeyNotFoundException("The user is not found in the database");
			//}
			//else
			//{
				Type? updatedValueType = updatedValues?.GetType();
				PropertyInfo[]? properties = updatedValueType?.GetProperties();

				Type userType = user.GetType();

				if (properties != null)
				{
					foreach (var property in properties)
					{
						if (property.GetValue(updatedValues) != null)
						{
							PropertyInfo? field = userType.GetProperty(property.Name);
							field?.SetValue(user, property.GetValue(updatedValues));
						}
					}

					context.User.Update(user);
					context.SaveChanges();

					return user;

                }
                else
				{
					throw new BadHttpRequestException("There is no data to Update");
				}
			//}

        }
	}
}

