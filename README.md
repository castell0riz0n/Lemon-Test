# 🧪 xUnit v3 Playground — .NET 10  
A minimal, focused playground for experimenting with **xUnit v3** on **Microsoft .NET 10**.  
This repository is designed as a **showcase**, **learning space**, and **reference** for the new features introduced in xUnit v3.

---

## 🎯 Purpose  
This project exists to:

- Explore **xUnit v3** features and breaking changes  
- Test compatibility with **.NET 10**  
- Demonstrate clean, modern test patterns  
- Provide a simple reference for others adopting xUnit v3  
- Serve as a sandbox for trying new test ideas

---

## 🛠️ Tech Stack  
- **.NET 10**
- **xUnit v3**
- **xunit.runner.visualstudio v3**
- **Microsoft.NET.Test.Sdk**
- **JetBrains Rider / Visual Studio / VS Code**

---

## 📦 Project Setup  
The project uses the standard xUnit v3 package set:

```xml
<ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="18.6.0" />
    <PackageReference Include="xunit.v3.mtp-v2" Version="3.2.2" />
    <PackageReference Include="xunit.runner.visualstudio" Version="3.1.5" />
</ItemGroup>
```

To restore dependencies:

```bash
dotnet restore
```

To run tests:

```bash
dotnet test
```

---

## ▶️ Running Tests in JetBrains Rider  
Rider currently has **partial support** for xUnit v3.  
If you don’t see the **Run Test** gutter icons:

1. Ensure the correct packages are installed (see above)  
2. Rebuild the solution  
3. Restart Rider  
4. Make sure the project targets **.NET 10**  
5. Confirm the test class and methods are `public`

---

## 📁 Repository Structure  
```
/src
  /Lemon-Test.Core
    FizzBuzz.cs
    ...
/tests
  /Lemon-Test.Core.Tests
    FizzBuzzTests.cs
    ...
Lemon-Test.slnx
README.md
```

---

## 🧩 Example Test (xUnit v3)

```csharp
using Xunit;

public class MathTests
{
    [Fact]
    public void Addition_Works()
    {
        var result = 2 + 3;
        Assert.Equal(5, result);
    }
}
```

---

## 📚 Useful Links  
- xUnit v3 GitHub: [https://github.com/xunit/xunit](https://github.com/xunit/xunit)  
- xUnit v3 Release Notes: `https://github.com/xunit/xunit/releases` [(github.com in Bing)](https://www.bing.com/search?q="https%3A%2F%2Fgithub.com%2Fxunit%2Fxunit%2Freleases")  
- .NET SDK Downloads: [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)  

---

## 🤝 Contributing  
This is a playground — contributions, ideas, and experiments are welcome.

---

## 📜 License  
MIT License — feel free to use this as a reference or starting point.