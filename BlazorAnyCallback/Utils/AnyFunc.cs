namespace BlazorAnyCallback.Utils
{
    /// <summary>
    /// AnyFunc wraps a (sync or async) callback function, 
    /// providing a standard way to invoke the function and
    /// avoiding unnecessary async delegates when the payload is not 
    /// the result of an async process.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public struct AnyFunc<TResult>
    {
        private readonly Func<TResult>? _callback;
        private readonly Func<Task<TResult>>? _asyncCallback;

        public AnyFunc(Func<TResult> callback)
        {
            _callback = callback;
            _asyncCallback = null;
        }

        public AnyFunc(Func<Task<TResult>> callback)
        {
            _callback = null;
            _asyncCallback = callback;
        }

        public static implicit operator AnyFunc<TResult>(Func<TResult> callback) => new(callback);

        public static implicit operator AnyFunc<TResult>(Func<Task<TResult>> callback) => new(callback);

        public async Task<TResult> InvokeAsync()
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

    public struct AnyFunc<T, TResult>
    {
        private readonly Func<T, TResult>? _callback;
        private readonly Func<T, Task<TResult>>? _asyncCallback;

        public AnyFunc(Func<T, TResult> callback)
        {
            _callback = callback;
            _asyncCallback = null;
        }

        public AnyFunc(Func<T, Task<TResult>> callback)
        {
            _callback = null;
            _asyncCallback = callback;
        }

        public static implicit operator AnyFunc<T, TResult>(Func<T, TResult> callback) => new(callback);

        public static implicit operator AnyFunc<T, TResult>(Func<T, Task<TResult>> callback) => new(callback);

        public async Task<TResult> InvokeAsync(T arg)
        {
            if (_asyncCallback is not null)
                return await _asyncCallback(arg);

            if (_callback is not null)
                return _callback(arg);

            #pragma warning disable CS8603 // Possible null reference return.
            return default;
            #pragma warning restore CS8603 // Possible null reference return.
        }
    }

    public struct AnyFunc<T0, T1, TResult>
    {
        private readonly Func<T0, T1, TResult>? _callback;
        private readonly Func<T0, T1, Task<TResult>>? _asyncCallback;

        public AnyFunc(Func<T0, T1, TResult> callback)
        {
            _callback = callback;
            _asyncCallback = null;
        }

        public AnyFunc(Func<T0, T1, Task<TResult>> callback)
        {
            _callback = null;
            _asyncCallback = callback;
        }

        public static implicit operator AnyFunc<T0, T1, TResult>(Func<T0, T1, TResult> callback) => new(callback);

        public static implicit operator AnyFunc<T0, T1, TResult>(Func<T0, T1, Task<TResult>> callback) => new(callback);

        public async Task<TResult> InvokeAsync(T0 arg0, T1 arg1)
        {
            if (_asyncCallback is not null)
                return await _asyncCallback(arg0, arg1);

            if (_callback is not null)
                return _callback(arg0, arg1);

#pragma warning disable CS8603 // Possible null reference return.
            return default;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }

    public struct AnyFunc<T0, T1, T2, TResult>
    {
        private readonly Func<T0, T1, T2, TResult>? _callback;
        private readonly Func<T0, T1, T2, Task<TResult>>? _asyncCallback;

        public AnyFunc(Func<T0, T1, T2, TResult> callback)
        {
            _callback = callback;
            _asyncCallback = null;
        }

        public AnyFunc(Func<T0, T1, T2, Task<TResult>> callback)
        {
            _callback = null;
            _asyncCallback = callback;
        }

        public static implicit operator AnyFunc<T0, T1, T2, TResult>(Func<T0, T1, T2, TResult> callback) => new(callback);

        public static implicit operator AnyFunc<T0, T1, T2, TResult>(Func<T0, T1, T2, Task<TResult>> callback) => new(callback);

        public async Task<TResult> InvokeAsync(T0 arg0, T1 arg1, T2 arg2)
        {
            if (_asyncCallback is not null)
                return await _asyncCallback(arg0, arg1, arg2);

            if (_callback is not null)
                return _callback(arg0, arg1, arg2);

#pragma warning disable CS8603 // Possible null reference return.
            return default;
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
