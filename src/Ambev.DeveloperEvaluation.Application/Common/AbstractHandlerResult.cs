using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Application.Commoon;

public abstract class AbstractHandlerResult
{
    public bool Sucess { get; set; }
    public IEnumerable<ValidationErrorDetail> Erros { get; set; } = new List<ValidationErrorDetail>();
}
