namespace BlazorAnyCallback.Pages
{
    /// <summary>
    /// AnyCallback wraps a (sync or async) callback function, 
    /// providing a standard way to invoke the function and
    /// avoiding unnecessary async delegates when not needed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct AnyCallback<T>
    {
        private readonly Func<T>? _callback;
        private readonly Func<Task<T>>? _asyncCallback;

        public AnyCallback(Func<T> callback)
        {
            _callback = callback;
            _asyncCallback = null;
        }

        public AnyCallback(Func<Task<T>> callback)
        {
            _callback = null;
            _asyncCallback = callback;
        }

        public static implicit operator AnyCallback<T>(Func<T> callback) => new(callback);

        public static implicit operator AnyCallback<T>(Func<Task<T>> callback) => new(callback);

        public async Task<T> InvokeAsync()
        {
            if (_asyncCallback is not null)
                return await _asyncCallback();

            if (_callback is not null)
                return _callback();

#pragma warning disable CS8603 // Possible null reference return.
            return default;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
