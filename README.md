# Classnames

![logo](logo.png)

A utility for Razor or Blazor web applications to conditionally build class names for components.

## Installation
Install the [NuGet package]() with dotnet or NuGet CLI:

```
# dotnet
dotnet add package classnames

# NuGet CLI
nuget install classnames -OutputDirectory packages
```

## Quickstart

Let's take the following Blazor code snipper which renders a `<button>` component using the classes `btn` and `btn-primary`:

```component.blazor
<button class="btn btn-primary">Click me!</button>
```

Say we wanted to always include the `btn` class, but only include `btn-primary` based on some condition, then we can achieve this:
```
@using Umamimolecule.ClassNames

<button class=@CN.Create("btn", ("btn-primary", ShowPrimary())) @onclick="IncrementCount">Click
    me</button>

@code {
  private bool ShowPrimary()
  {
    // Some logic here
    return true;
  }
}
```
This particular example uses a tuple of the form `(string, bool)`, but there's many more was to pass in conditions.

## Conditions

Conditions can be provided in a number of forms and can be combined.

### String
The string value is always included.

Example:
```
// Returns "apple banana cherry"
CN.Create("apple", "banana", "cherry");
```

### Tuple (string, bool)
If the bool part is true then the string part is included.

Example:
```
// Returns "apple cherry"
CN.Create(("apple", true), ("banana", false), ("cherry", true));
```

### Tuple (string, Func&lt;bool&gt;)
If the bool function evaluates the true then the string part is included.

Example:
```
Func<bool> trueFunc = new Func<bool>(() => true);
Func<bool> falseFunc = new Func<bool>(() => false);

// Returns "apple cherry"
CN.Create(("apple", trueFunc), ("banana", falseFunc), ("cherry", trueFunc));
```

### KeyValuePair&lt;string, bool&gt;
If the value is true then the key is included.

Example:
```
// Returns "apple cherry"
CN.Create(
  new KeyValuePair("apple", true),
  new KeyValuePair("banana", false),
  new KeyValuePair("cherry", true)
);
```

### KeyValuePair&lt;string, Func&lt;bool&gt;&gt;
If the value function evaluates to true then the key is included.

Example:
```
Func<bool> trueFunc = new Func<bool>(() => true);
Func<bool> falseFunc = new Func<bool>(() => false);

// Returns "apple cherry"
CN.Create(
  new KeyValuePair("apple", trueFunc),
  new KeyValuePair("banana", falseFunc),
  new KeyValuePair("cherry", trueFunc)
);
```

### Func&lt;string&gt;
The evaluated string is included.

Example:
```
Func<string> appleFunc = new Func<string>(() => "apple");
Func<string> bananaFunc = new Func<string>(() => "banana");

// Returns "apple banana"
CN.Create(
  appleFunc,
  bananaFunc
);
```

### Dictionary&lt;string, bool&gt;
The values of the dictionary are evaluated and if true the key is included.

Example:
```
Dictionary<string, bool> classes = new Dictionary<string, bool>()
{
  { "apple", true },
  { "banana", false }
  { "cherry", true }
};

// Returns "apple cherry"
CN.Create(classes);
```

### Dictionary&lt;string, Func&lt;bool&gt;&gt;
The value functions of the dictionary are evaluated and if true the key is included.

Example:
```
Func<bool> trueFunc = new Func<bool>(() => true);
Func<bool> falseFunc = new Func<bool>(() => false);

Dictionary<string, Func<bool>> classes = new Dictionary<string, Func<bool>>()
{
  { "apple", trueFunc },
  { "banana", falseFunc }
  { "cherry", trueFunc }
};

// Returns "apple cherry"
CN.Create(classes);
```

## Notes
 - All other types will be converted to a string, and if not null or whitespace will be included.
 - Any null or whitespace values are excluded.
 - Any collections passed in are flattened.