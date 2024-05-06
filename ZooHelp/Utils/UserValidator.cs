using FluentValidation;
using ZooHelp.Models;

namespace ZooHelp.Utils;

public class UserValidator : AbstractValidator<RegistrationModel>
{
    public UserValidator()
    {
        RuleFor(m => m.Email)
            .EmailAddress()
            .WithMessage("Некорректный адрес электронной почты");
        RuleFor(m => m.Password)
            .MinimumLength(8)
            .WithMessage("Длина пароля должна быть не менее 8 символов")
            .MaximumLength(50)
            .WithMessage("Длина пароля не должна быть более 50 символов")
            .Must(p => p.Any(s => "0123456789".Contains(s)))
            .WithMessage("Пароль должен содержать хотя бы одну цифру")
            .Must(p => p.Any(s => "qwertyuiopasdfghjklzxcvbnm".Contains(char.ToLower(s))))
            .WithMessage("Пароль должен содержать хотя бы одну букву");
        RuleFor(m => m.Name)
            .Must(n => !n.Any(s => "1234567890".Contains(s)))
            .WithMessage("В имени не должно быть цифр");
        RuleFor(m => m.Surname)
            .Must(n => !n.Any(s => "1234567890".Contains(s)))
            .WithMessage("В фамилии не должно быть цифр");
    }

    public List<string> ValidateAndGetErrors(RegistrationModel model)
        => Validate(model).Errors.Select(e => e.ErrorMessage).ToList();
    
}