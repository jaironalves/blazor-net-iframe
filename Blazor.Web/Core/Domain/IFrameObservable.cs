namespace Blazor.Web.Core.Domain
{
    public interface IFrameObservable
    {
        void Subscribe(IFrameObserver observer);
        void Unsubscribe(IFrameObserver observer);
        void NotifyObservers();
    }
}
