using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;

namespace SwingFilms.Services.Features.Identity.Commands;

// TODO доделать команду и подключить
public class RegistrationSystemCommand : IRequest
{
    
}

public class RegistrationSystemCommandValidator : AbstractValidator<RegistrationSystemCommand>
{
    public RegistrationSystemCommandValidator(IStringLocalizer<RegistrationSystemCommandValidator> localizer)
    {
        
    }
}

public class RegistrationSystemCommandHandler : IRequestHandler<RegistrationSystemCommand>
{
    public Task Handle(RegistrationSystemCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}