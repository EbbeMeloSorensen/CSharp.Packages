using MediatR;

namespace ClassLibrary;

public class List
{
    // Bemærk, at string her er det, man får tilbage
    public class Query : IRequest<string>
    {
    }

    // Bemærk, at string her er det, man får tilbage
    public class Handler : IRequestHandler<Query, string>
    {
        // Ofte vil sådan en handler her have en constructor, der tager en DbContext og f.eks. en IMapper som input
        // For at sikre, at den får det, vil man i ConfigureServices i Program.cs-filen skulle angive de services,
        // der skal bruges

        //private readonly DataContext _context;
        //private readonly IMapper _mapper;

        //public Handler(
        //    DataContext context,
        //    IMapper mapper)
        //{
        //    _context = context;
        //    _mapper = mapper;
        //}


        // Bemærk, at string her er det, man får tilbage
        public Task<string> Handle(Query request, CancellationToken cancellationToken)
        {
            return Task.FromResult("Response from query handler");
        }
    }
}
