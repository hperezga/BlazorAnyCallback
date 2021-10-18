# BlazorAnyCallback project

This is a simple modification to a boiler plate Blazor Server App to demonstrate how to deal with situations where you cannot get away with an EventCallback<T> in a component. 
  
Sometimes you need to request a value via a callback in your components. To make this general, you most likely create a **Func<Task< TResult >>** delegate to cover all the cases: sync and async. But this forces your clients to use Task.FromResult() in many places of the code because of the **Func<Task< TResult >>** delegate, even when the value is not the result from an async call. 

I personally like the EvenCallback events because they can take any callback method (sync or async), but again EventCallback has no result on invocation. _AnyFunc_ is an attempt to resolve this issue given my ignorance of a better solution.

## AnyFunc structure

Found in **../BlazorAnyCallback/Utils/AnyFunc.cs** is a struct that wraps a sync or async callback function, providing a standard way to invoke the function and avoiding unnecessary async delegates when the payload is not the result of an async process. This struct can facilitate the process of hooking your callback events to sync or async methods.
```
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
 ```
## FunnyButton.razor - A sample component with a AnyFunc<string> callback parameter
  
```
<button class="btn btn-primary" @onclick="Clicked">@_template</button>

@code {
    [Parameter]
    public AnyFunc<string> GetTemplate { get; set; }

    private string _template = "Click me!";

    private async void Clicked()
    {
        _template = await GetTemplate.InvokeAsync();
    }
}
```

## FunnyButton2.razor - A sample component with a AnyFunc<int, string> callback parameter
  
```
<button class="btn btn-primary" @onclick="Clicked">@_template</button>

@code {
    [Parameter]
    public AnyFunc<int, string> GetTemplate { get; set; }

    private string _template = "Click me too!";

    private async void Clicked()
    {
        _template = await GetTemplate.InvokeAsync(100);
    }
}
```
  
## FunnyPage.razor - A page using the FunnyButton components and hooking different methods to the callbacks
  
```
@page "/funny"

<div>
    <FunnyButton GetTemplate=new(GetFunny) />
</div>

<div>
    <FunnyButton GetTemplate=@new(GetFunny) />
</div>

<div>
    <FunnyButton GetTemplate=new(GetFunnyAsync) />
</div>

<div>
    <FunnyButton GetTemplate="new(GetFunny)" />
</div>

<div>
    <FunnyButton />
</div>

<div>
    <FunnyButton2 GetTemplate=new(GetFunnyTwo) />
</div>

<div>
    <FunnyButton2 GetTemplate=new(GetFunnyTwoAsync) />
</div>

<div>
    <FunnyButton2 GetTemplate=@new(n => $"Inline value is: {n}") />
</div>

@code {

    private string GetFunny()
    {
        return $"Ok, funny call.";
    }

    private async Task<string> GetFunnyAsync()
    {
        return await Task.FromResult($"Ok, funny async call.");
    }

    private string GetFunnyTwo(int v) => $"Funny value = {v}";

    private async Task<string> GetFunnyTwoAsync(int v) => await Task.FromResult($"Funny async value = {v}");
}
  
```
  
## Why the _new_ around my callback methods?

I was hopping that I could convert the delegate functions directly to AnyFunc instances, but the C# compiler is smart, but not so smart to resolve the issue; so the only solution was to add constructors to the AnyFun struct and becase the compiler detects the type of the parameter, **new** will resolve the convertion. It is not the end of the world but I wanted to match the elegance of the EvenCallback delegates. Maybe next time!
  

  
