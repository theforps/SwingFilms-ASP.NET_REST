using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using SwingFilms.Services.DtoModels;

namespace SwingFilms.Services.Features.Identity.Commands;

// TODO доделать команду и подключить
public class RegistrationSystemCommand : IRequest<ResultDto<string>>
{
    
}

public class RegistrationSystemCommandValidator : AbstractValidator<RegistrationSystemCommand>
{
    public RegistrationSystemCommandValidator(IStringLocalizer<RegistrationSystemCommandValidator> localizer)
    {
        
    }
}

public class RegistrationSystemCommandHandler : IRequestHandler<RegistrationSystemCommand, ResultDto<string>>
{
    public async Task<ResultDto<string>> Handle(RegistrationSystemCommand request, CancellationToken cancellationToken)
    {
        
        
        
        return new ResultDto<string>("");
    }
}