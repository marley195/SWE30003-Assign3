using System.Collections.Generic;
namespace RelaxingKoala
{
    public interface ISubject
    {
        void AttachObserver(IObserver observer);
        void DetachObserver(IObserver observer);
        void NotifyObserver();
    }
}
