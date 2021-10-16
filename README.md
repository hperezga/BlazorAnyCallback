# BlazorAnyCallback project

This is a simple modification to a boiler plate Blazor Server App to demonstrate how to deal with situations where you cannot get away with an EventCallback<T> in a component. 
  
Sometimes you need to request a value via a callback in your components. To make this general, you most likely create a **Func<Task< TResult >>** delegate to cover all the cases: sync and async. But this forces your clients to use Task.FromResult() in many applications of the code becuase of the **Func<Task< TResult >>** delegate. I personally love the EvenCallback events because it can take any callback method (sync or async), but again EventCallback has no result on invocation. _AnyFunc_ is an attempt to resolve this issue given my ignorance of a better solution.

## AnyFunc structure

Found in **../BlazorAnyCallback/Utils/AnyFunc.cs** is a struct the wraps a (sync or async) callback function, providing a standard way to invoke the function and avoiding unnecessary async delegates when the payload is not the result of an async process. This struct can facilitate the process of hooking your callback events to sync or async methods.
  
## FunnyButton.razor - A sample component with a AnyFunc<T> callback parameter
  
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
  
  
  

  
