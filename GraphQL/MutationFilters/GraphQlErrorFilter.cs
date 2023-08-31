namespace GraphQLPractise.GraphQL.MutationFilters
{
    public class GraphQlErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            if (error.Exception is KeyNotFoundException keyNotFoundException)
            {
                var newError = error.WithMessage(keyNotFoundException.Message);
                return newError;
            }

            return error;
        }
    }
}

