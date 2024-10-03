using ErrorOr;

namespace BuildingBlocks.Base;

public abstract class ResponseBaseService : IErrorOr
{
    public List<Error> Errors { get; set; }
    public bool IsError => Errors.Any();

    protected ResponseBaseService()
    {
        Errors = new List<Error>();
    }


    public void Failure(Error error)
    {
        Errors.Add(error);
    }

    public void Failure(List<Error> errors)
    {
        Errors = errors;
    }
}