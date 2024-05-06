using ZooHelp.Abstractions;
using ZooHelp.Models;
using ZooHelp.Utils;

namespace ZooHelp.Services;

public class ValidationService: IValidationService
{
    public List<string> ValidateRegistrationModel(RegistrationModel model)
    {
        var validator = new UserValidator();
        return validator.ValidateAndGetErrors(model);
    }
}