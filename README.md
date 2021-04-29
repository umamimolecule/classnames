# classnames

![Nuget](https://img.shields.io/nuget/v/Umamimolecule.ClassNames) [![](https://img.shields.io/badge/license-MIT-blue.svg)](#license)

A utility for Razor and Blazor web applications to conditionally build class names for components.
<p align="center">
<img src="https://raw.githubusercontent.com/umamimolecule/classnames/main/readmelogo.png">
</p>

### Table of contents
 - [Installation](#installation)  
 - [Quickstart](#quickstart)  
 - [Conditions](#conditions)  
 - [Notes](#notes)  
 - [Contributing](#contributing)
---

<a name="installation" />

## Installation
Install the [NuGet package](https://www.nuget.org/packages/Umamimolecule.ClassNames) with dotnet CLI:

```
dotnet add package Umamimolecule.ClassNames
```

<a name="quickstart" />

## Quickstart

Let's take the following Blazor code snippet which renders a `<button>` component using the classes `btn` and `btn-primary`:

```component.blazor
<button class="btn btn-primary">
  Click me!
</button>
```

Say we wanted to always include the `btn` class, but only include `btn-primary` based on some condition, then we can achieve this using the `CN.Create` method:
```
@using Umamimolecule.ClassNames

<button class=@CN.Create("btn", ("btn-primary", ShowPrimary()))>
  Click me!
</button>

@code {
  private bool ShowPrimary()
  {
    // Some logic here
    return true;
  }
}
```
This particular example uses a tuple of the form `(string, bool)` to evaluate the classname, but there's many more ways to define conditions.

<a name="conditions" />

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

<a name="notes" />

## Notes
 - All other types will be converted to a string, and if not null or whitespace will be included.
 - Any null or whitespace values are excluded.
 - Any collections passed in are flattened.

<a name="contributing" />

## Contributing

Contributions are most welcome.

### Issues

If you find a bug or feel that some useful functionality is missing, do raise as an issue and/or enhancement request.

### How to contribute

When submitting patches, follow the "fork-and-pull" Git workflow:
 - Fork the repository on GitHub
 - Clone the project to your own machine
 - Commit changes to your own branch
 - Push your work back up to your fork
 - Submit a pull request so that we can review your changes
