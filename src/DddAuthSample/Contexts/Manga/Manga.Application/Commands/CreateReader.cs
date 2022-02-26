using Manga.Domain.Entities;
using Manga.Application.Contracts;
using SharedKernel;
using MediatR;

namespace Manga.Application.Commands;

public class CreateReaderCommand : IRequest<ReaderId>
{
	public Guid UserId { get; set; }

	public class CreateReaderCommandHandler : IRequestHandler<CreateReaderCommand, ReaderId>
	{
		private readonly IReaderRepository _readerRepository;

		public CreateReaderCommandHandler(IReaderRepository readerRepository)
		{
			_readerRepository = readerRepository;
		}

		public Task<ReaderId> Handle(CreateReaderCommand request, CancellationToken cancellationToken)
		{
			var userId = new UserId(request.UserId);
			var reader = Reader.Create(userId);
			_readerRepository.Add(reader);
			_readerRepository.Save();
			return Task.FromResult(reader.Id);
		}
	}
}
