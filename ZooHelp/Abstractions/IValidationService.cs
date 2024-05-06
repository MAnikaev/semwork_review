using ZooHelp.Models;

namespace ZooHelp.Abstractions;

public interface IValidationService
{
    public List<string> ValidateRegistrationModel(RegistrationModel model);
}