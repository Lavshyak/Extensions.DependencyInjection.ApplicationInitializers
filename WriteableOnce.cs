namespace LAVSHYAK.Extensions.DependencyInjection.ApplicationInitializers;

internal class WriteableOnce<T>
{
    public WriteableOnce()
    {
    }

    public WriteableOnce(T initialValue)
    {
        _value = initialValue;
    }

    public bool Initialized { get; private set; }

    private T _value = default!;

    private readonly object _locker = new object();

    public T Value
    {
        get => _value;
        set
        {
            lock (_locker)
            {
                if (Initialized)
                    throw new InvalidOperationException();

                _value = value;
                Initialized = true;
            }
        }
    }
}