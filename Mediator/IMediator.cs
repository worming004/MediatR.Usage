namespace Mediator;


public interface IMediator
{
    void Notify(object input);
    void Register(object compoment);
}
