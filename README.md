# BlazorAnyCallback project

This is a simple modification to a boiler plate Blazor Server App to demonstrate how to deal with situations where you cannot get away with an EventCallback<T> in a component. Sometimes you need a value to be return from a callback in one of your components. To make this general you most likely create a **Func<Task< TResult >>** delegate to cover all cases: sync and async. But this forces to use Task. FromResult() in many applications of the code when you have you return a payload you have just there. I personally love the EvenCallback events because they can take any callback method sync or async. But again EventCallback has no result on invokation. AnyFunc is an attempt to resolve this issue given my ignorance of a better solution.

## AnyFunc structure

Found in ../Utils/AnyFunc.cs the structs wraps a (sync or async) callback function, providing a standard way to invoke the function and avoiding unnecessary async delegates when the payload is not the result of an async process.
