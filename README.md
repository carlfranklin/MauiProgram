# MauiProgram.cs

In this module we are going to cover what is in the MauiProgram class, how does it work, why it works, and how can we configure services. In this episode we will focus on fonts and logging.

This is the repo for episode 22 of The .NET Show, which you can find at [https://thedotnetshow.com](https://thedotnetshow.com)

#### A New Discovery: SrcCpy

Before we get started, I need to tell you about a fairly new free open-source tool  that lets you view your Android screen on your Windows, Mac, or Linux desktop. There are download links and instructions at the repo. Go get it!

[https://github.com/Genymobile/scrcpy](https://github.com/Genymobile/scrcpy)

Run it at the command line so you can see any error messages.

I have my Android phone connected via USB, so I use this command:

```
scrcpy -d --select-usb
```

Here's a more helpful guide:

[https://www.makeuseof.com/tag/mirror-android-screen-pc-mac-without-root/](https://www.makeuseof.com/tag/mirror-android-screen-pc-mac-without-root/)



## MauiProgram

MAUI applications are bootstrapped using a _host_. The _host_ allows applications to be initialized from a single location, as well as configure services, libraries, resources, etc. just like in other .NET Core applications.

The _host_ also encapsulates app's resources, and lifetime like Dependency Injection, Logging, Configuration, and App shutdown.

In the case of MAUI applications, the specific _host_ utilized is a [.NET Generic Host](https://docs.microsoft.com/en-us/dotnet/core/extensions/generic-host), which is also used in other type of applications.

In the demo, we will cover how the _host_ works and how to configure it, as well as understanding of why this components work and what is needed to be able to use them.

## Prerequisites

The following prerequisites are needed for this demo.

### Visual Studio 2022 Preview

For this demo, we are going to use the latest version of [Visual Studio 2022 Preview](https://visualstudio.microsoft.com/vs/community/).

In my case, I already have [Visual Studio 2022 Preview](https://visualstudio.microsoft.com/vs/community/) installed, so the first thing I am going to do, is update it from Preview 4.0 to 6.0.

![image-20220512174201007](images/image-20220512174201007.png)  

>:memo: Everything covered in this demo, is also available in Preview 1, but I like to update to the latest for good measure.

### Mobile Development with .NET Workload

If you have been following [The .NET Show](https://www.youtube.com/playlist?list=PL8h4jt35t1wgW_PqzZ9USrHvvnk8JMQy_) you know that in order to build MAUI apps, the Mobile Development with .NET Workload workload needs to be installed, as well as the .NET MAUI (Preview), so if you do not have that installed you can do that now.

![Mobile Development with .NET Workload](images/ecad71fd4493d0cfcd86e932e21abfe7f483422e0d5e44491d2fc8f226b8b797.png)  

### MAUI Templates Missing?

As I tried to create a MAUI Application, I noticed that the MAUI templates were missing:

![Missing MAUI Templates](images/02e1c7c388f3dc7892fde5f18e86ba08777c280725afb9065d82c5995e2b1989.png)  

>:point_up: If the MAUI Templates are missing, they can be installed running ```dotnetcli dotnet new -i Microsoft.Maui.Templates```

So, first I listed all installed templates by running ```dotnetcli dotnet new --list```, this was the output:

```dotnetcli
dotnet new --list
These templates matched your input:

Template Name                                 Short Name           Language    Tags
--------------------------------------------  -------------------  ----------  -------------------------------------
ASP.NET Core Empty                            web                  [C#],F#     Web/Empty
ASP.NET Core gRPC Service                     grpc                 [C#]        Web/gRPC
ASP.NET Core Web API                          webapi               [C#],F#     Web/WebAPI
ASP.NET Core Web App                          webapp,razor         [C#]        Web/MVC/Razor Pages
ASP.NET Core Web App (Model-View-Controller)  mvc                  [C#],F#     Web/MVC
ASP.NET Core with Angular                     angular              [C#]        Web/MVC/SPA
ASP.NET Core with React.js                    react                [C#]        Web/MVC/SPA
ASP.NET Core with React.js and Redux          reactredux           [C#]        Web/MVC/SPA
Blazor Server App                             blazorserver         [C#]        Web/Blazor
Blazor WebAssembly App                        blazorwasm           [C#]        Web/Blazor/WebAssembly/PWA
Class Library                                 classlib             [C#],F#,VB  Common/Library
Console App                                   console              [C#],F#,VB  Common/Console
dotnet gitignore file                         gitignore                        Config
Dotnet local tool manifest file               tool-manifest                    Config
EditorConfig file                             editorconfig                     Config
global.json file                              globaljson                       Config
MSTest Test Project                           mstest               [C#],F#,VB  Test/MSTest
MVC ViewImports                               viewimports          [C#]        Web/ASP.NET
MVC ViewStart                                 viewstart            [C#]        Web/ASP.NET
NuGet Config                                  nugetconfig                      Config
NUnit 3 Test Item                             nunit-test           [C#],F#,VB  Test/NUnit
NUnit 3 Test Project                          nunit                [C#],F#,VB  Test/NUnit
Protocol Buffer File                          proto                            Web/gRPC
Protocol Buffer File                          proto                            Web/gRPC
Razor Class Library                           razorclasslib        [C#]        Web/Razor/Library/Razor Class Library
Razor Component                               razorcomponent       [C#]        Web/ASP.NET
Razor Page                                    page                 [C#]        Web/ASP.NET
Solution File                                 sln                              Solution
Web Config                                    webconfig                        Config
Windows Forms App                             winforms             [C#],VB     Common/WinForms
Windows Forms Class Library                   winformslib          [C#],VB     Common/WinForms
Windows Forms Control Library                 winformscontrollib   [C#],VB     Common/WinForms
Worker Service                                worker               [C#],F#     Common/Worker/Web
WPF Application                               wpf                  [C#],VB     Common/WPF
WPF Class library                             wpflib               [C#],VB     Common/WPF
WPF Custom Control Library                    wpfcustomcontrollib  [C#],VB     Common/WPF
WPF User Control Library                      wpfusercontrollib    [C#],VB     Common/WPF
xUnit Test Project                            xunit                [C#],F#,VB  Test/xUnit
```

Then, I installed the missing MAUI templates by running ```dotnetcli dotnet new -i Microsoft.Maui.Templates```, and this was the output:

```dotnetcli
dotnet new -i Microsoft.Maui.Templates
The following template packages will be installed:
   Microsoft.Maui.Templates

Success: Microsoft.Maui.Templates::6.0.300-rc.2.5513 installed the following templates:
Template Name                                  Short Name        Language  Tags
---------------------------------------------  ----------------  --------  ---------------------------------------------------------
.NET MAUI App (Preview)                        maui              [C#]      MAUI/Android/iOS/macOS/Mac Catalyst/Windows/Tizen
.NET MAUI Blazor App (Preview)                 maui-blazor       [C#]      MAUI/Android/iOS/macOS/Mac Catalyst/WinUI/Tizen/Blazor
.NET MAUI Class Library (Preview)              mauilib           [C#]      MAUI/Android/iOS/macOS/Mac Catalyst/Windows/Tizen
.NET MAUI ContentPage (C#) (Preview)           maui-page-csharp  [C#]      MAUI/Android/iOS/macOS/Mac Catalyst/WinUI/Tizen/Xaml/Code
.NET MAUI ContentPage (XAML) (Preview)         maui-page-xaml    [C#]      MAUI/Android/iOS/macOS/Mac Catalyst/WinUI/Tizen/Xaml/Code
.NET MAUI ContentView (C#) (Preview)           maui-view-csharp  [C#]      MAUI/Android/iOS/macOS/Mac Catalyst/WinUI/Tizen/Xaml/Code
.NET MAUI ContentView (XAML) (Preview)         maui-view-xaml    [C#]      MAUI/Android/iOS/macOS/Mac Catalyst/WinUI/Tizen/Xaml/Code
.NET MAUI ResourceDictionary (XAML) (Preview)  maui-dict-xaml    [C#]      MAUI/Android/iOS/macOS/Mac Catalyst/WinUI/Xaml/Code
```

:exclamation: Just like that, MAUI templates are back!

![MAUI Templates Installed](images/60c7f22f3523fc664ca5a92b3a8643d015c43a1058a719cc860e2ff625163174.png)  



#### Problems with .NET 7 Preview 3

:warning: If you followed my previous video [Native AOT in .NET 7. The .NET Show with Carl Franklin Ep 21](https://www.youtube.com/watch?v=4THfSynZLq8&list=PL8h4jt35t1wgW_PqzZ9USrHvvnk8JMQy_&index=21) you may run into the same issue, so follow along.

After spending a couple of hours trying to get my environment back in a working state, I finally figured out the reason, the .NET 7 SDK 7.0.100-preview.3.22179.4 needs to be uninstalled. The easiest way to do that, is to run the installer used to install it and click Uninstall.

![Uninstall .NET SDK 7.0.100-preview.3.22179.4](images/cbb06a071717e354ee77f2503d9b3303fb5cfe93bb1e11b003270ea4f33d1a39.png)

![.NET SDK 7.0 Uninstall Successfully Completed](images/20e712d43e5a9f0734143cd6417b5ac4438b15851237271f88ca43882b82e3a5.png)

If you still have trouble, you may have to completely uninstall Visual Studio Preview and re-install after a reboot. Yeah, .NET 7 Preview 3 was pretty nasty.

## Demo

We are going to configure a service and inject it in a class, using Dependency Injection so it can be used.

We are also going to configure logging and inject it as well to log information to the Output window.

The first step, as usual, is to create our demo application.

### Create a MAUI Application

![Create a new MAUI Application project](images/83e0873d11ac7a7ad94f4b6b0b2d37a137b324ba2f9dfaccaf70a47ce2b0e44c.png)  

![Configure your new project](images/dda78b72379639040f37e7b5f1ab9556da96a9d20b306822f1df81229ea4fc68.png)    

### MauiProgram.cs

Traditionally, the _host_ is being created in the *Program.cs* file, as you may remember it from ASP.NET Core Web App as well as in ASP.NET Core Web API applications prior to .NET 6.

The following *Program.cs* file was generated using .NET Core 3.1, but the same code will be in applications built for .NET 5.

#### ASP.NET Core Web App and in ASP.NET Core Web API's Program.cs file

```csharp
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
```

>:point_up: Notice the use of Startup (which is declared in *Startup.cs*) to configure services.

#### .NET MAUI App (Preview) MauiProgram.cs file using .NET 6

In the case of MAUI applications, the _host_ is being created in the `MauiProgram.cs` file.

You can see that a `MauiAppBuilder` is being created to be able to configure fonts, resources and services, here rather than in the Startup class, which is not included anymore.

```csharp
namespace MauiProgram;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
	}
}
```

All this is available thanks to the MAUI Template including the `Microsoft.Maui.Extensions` NuGet package, which includes dependencies to .NET6, Android, iOS, Mac Catalyst, Tizen, and Windows. Those dependencies allow us to use Dependency Injection, Logging, and Interop services, as mentioned before.

![Dependencies](images/2c5bb5fc953b94dbc22a99e65594c6bc8904841111e3f5d6645674402943867a.png)

>:exclamation: Is important to mention than the _host_ has a container with a collection of hosted services, so when the _host_ starts, it calls `IHostedService.StartAsync` on any class implementing the `IHostedService` interface.

#### MauiProgram.cs Explained

I order to explain the MauiProgram.cs I added comments to the code below.

```csharp
namespace MauiProgram;

public static class MauiProgram
{
    // This method returns the entry point (a MauiApp) and is called
    // from each platform's entry point.
    public static MauiApp CreateMauiApp()
    {
        // MauiApp.CreateBuilder returns a MauiAppBuilder, which
        // is used to configure fonts, resources, and services.
        var builder = MauiApp.CreateBuilder();

        builder
            // We give it our main App class, which derives from Application.
            // App is defined in App.xaml.cs.
            .UseMauiApp<App>()
            // Default font configuration.
            .ConfigureFonts(fonts =>
            {
                // AddFont takes a required filename (first parameter)
                // and an optional alias for each font.
                // When using these fonts in XAML you can use them
                // either by filename (without the extension,) or the alias.
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // The MauiAppBuilder returns a MauiApp application.
        return builder.Build();
    }
}
```

If you look at the references for CreateMauiApp, you will see how all 5 platforms entry points, are calling this method to get the MauiApp returned.

![CreateMauiApp References](images/9183c0304b8fbf23518a1eb3949ccde1c49c5289ea2139af0f03f3eda3e365e9.png)  

Below is a graphical representation of the dependencies:

![CreateMauiApp Diagram for a Windows Application](images/6ebbb45b9bb2241e63681a0f8b8625330be209350e1c63567941630e786c81bb.png)  

#### So, what else can we do in MauiProgram.cs?

Well, for starters you can configure animations, a container, dispatching, effects, essentials, fonts, image sources, and MAUI handlers, as you can see in the image below.

##### Hosting Extension Methods

![Hosting Extension Methods](images/de9a4498e04c80861da9006293a4659d3d7ff7e9db1c469a13d6a10f629355cc.png)  

With the exception of `ConfigureContainer`, they are all extension methods provided by the `Microsoft.Maui.Hosting` namespace.

You can also register Services, Logging and Configuration via each of those properties.

##### Services, Logging and Configuration

![Services, Logging and Configuration](images/a8ae16e29bb7ffce9c730b49779fe94d583e1a845a7432ad3177d88aeca5baeb.png)  

#### Adding a Service

Let's imagine we need an service that calls some API to get some test data. Let's add a file `ApiService.cs` and simply return some Json hard-coded data.

```csharp
namespace MauiProgram
{
    public class ApiService : IApiService
    {
        public string GetTestData()
        {
			return @"Test data simulating an API call.";
        }
    }
}
```

As you may know, .NET Core provides Dependency Injection out-of-the box, and Services are added using Dependency injection, so let's extract an interface for the `ApiService` and call it `IApiService.cs`.

![Extract Interface](images/539708ff4618ad12beae4cd7fc8f37966eca6f515c8c38daed5f2c5318f77146.png)  

![Extract Interface Dialog](images/67c473814860876a4169614b773662f0e634d155f3000c9ef47b620981c900e5.png)  

That will create the `IApiService` interface, and also will make ApiService implement it.

Now all you have to do is to append the following code to our `MauiProgram.cs` file, in the builder.

```csharp
.Services
				.AddSingleton<MainPage>()
				.AddSingleton<IApiService, ApiService>()
```

The complete file looks like this:

```csharp
namespace MauiProgram;

public static class MauiProgram
{
	// This method returns the entry point (a MauiApp) and is called from each platform's entry point.
	public static MauiApp CreateMauiApp()
	{
		// MauiApp.CreateBuilder returns a MauiAppBuilder, which is used to configure fonts, resources, and services.
		var builder = MauiApp.CreateBuilder();

		builder
			// We give it our main App class, which derives from Application. App is defined in App.xaml.cs.
			.UseMauiApp<App>()
			// Default font configuration.
			.ConfigureFonts(fonts =>
			{
				// AddFont takes a required filename (first parameter) and an optional alias for each font.
				// When using these fonts in XAML you can use them either by filename (without the extension,) or the alias.
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			// Register services
			.Services
				.AddSingleton<MainPage>()
				.AddSingleton<IApiService, ApiService>();

		// The MauiAppBuilder returns a MauiApp application.
		return builder.Build();
	}
}
```

Remove the image from `MainPage.xaml` and repurpose the Counter button.

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="MauiProgram.MainPage">

    <ScrollView>
        <VerticalStackLayout Spacing="25" Padding="30">

            <Label 
                Text="Hello, World!"
                SemanticProperties.HeadingLevel="Level1"
                FontSize="32"
                HorizontalOptions="Center" />

            <Label 
                Text="Welcome to .NET Multi-platform App UI"
                SemanticProperties.HeadingLevel="Level1"
                SemanticProperties.Description="Welcome to dot net Multi platform App U I"
                FontSize="18"
                HorizontalOptions="Center" />

            <Label 
                Text="Test Data: "
                FontSize="18"
                x:Name="CounterLabel"
                HorizontalOptions="Center" />

            <Button 
                Text="Click me"
                FontAttributes="Bold"
                SemanticProperties.Hint="Get data"
                Clicked="OnGetDataClicked"
                HorizontalOptions="Center" />
        </VerticalStackLayout>
    </ScrollView>
</ContentPage>
```

#### Consuming a Service

In order to consume the service we need to use Dependency Injection to inject the service we registered in the step before.

In `MainPage.xaml.cs` make the following changes:

```csharp
namespace MauiProgram;

public partial class MainPage : ContentPage
{
	private readonly IApiService _apiService;

	public MainPage(IApiService apiService)
	{
		_apiService = apiService;
		InitializeComponent();
	}

    private void OnGetDataClicked(object sender, EventArgs e)
	{
		CounterLabel.Text = $"Test Data: {_apiService?.GetTestData()}";

		SemanticScreenReader.Announce(CounterLabel.Text);
	}
}
```

Now you can run the application and see how the ApiService gets injected and can be used to retrieve the data.

![API Service](images/068eb3e6f0d63adca4553bb908dfa918dd0193ffe1b0493536756f99a3751a83.png)  

![Data](images/3a0a0a59faa4e758d006ad9f33d5586b69c85eb3ff5fae1f71aaa85b6d2c1bf5.png)  

The application displays the data after the Click me button is pressed.

![Get Data](images/52e6f328e1f7d6b53e25539c99f7807f456c269f705c83469338ceae9947a2fc.png)  

#### Add Logging

In order to add Logging, we need to add a Logging provider. In this case I am going to add the Debug Logging, but just as easy, other logging providers can be added in a similar way.

Add a NuGet package reference to `Microsoft.Extensions.Logging.Debug`.

![Microsoft.Extensions.Logging.Debug](images/1d06d9711b2af8b1cf76e5762a817b3b0967c54aa1fb919f73b5ef8b1e3f198c.png)  

Add `using Microsoft.Extensions.Logging;` to your using statements, and add the following code to the builder in `MauiProgram.cs`

```csharp
.AddLogging(configure =>
					{
						configure.AddDebug();
					});
```

The complete file looks like this:

```csharp
using Microsoft.Extensions.Logging;

namespace MauiProgram;

public static class MauiProgram
{
	// This method returns the entry point (a MauiApp) and is called from each platform's entry point.
	public static MauiApp CreateMauiApp()
	{
		// MauiApp.CreateBuilder returns a MauiAppBuilder, which is used to configure fonts, resources, and services.
		var builder = MauiApp.CreateBuilder();

		builder
			// We give it our main App class, which derives from Application. App is defined in App.xaml.cs.
			.UseMauiApp<App>()
			// Default font configuration.
			.ConfigureFonts(fonts =>
			{
				// AddFont takes a required filename (first parameter) and an optional alias for each font.
				// When using these fonts in XAML you can use them either by filename (without the extension,) or the alias.
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			// Register services
			.Services
				.AddSingleton<MainPage>()
				.AddSingleton<IApiService, ApiService>()
				// Add Logging
				.AddLogging(configure =>
					{
						configure.AddDebug();
					});

		// The MauiAppBuilder returns a MauiApp application.
		return builder.Build();
	}
}
```

Inject the logger in the `MainPage.xaml.cs` file, in a similar way we did with the ApiService, and also add `using Microsoft.Extensions.Logging;` to your using statements, and finally add any calls to log information.

The complete file looks like this:

```csharp
using Microsoft.Extensions.Logging;

namespace MauiProgram;

public partial class MainPage : ContentPage
{
	private readonly IApiService _apiService;
	private readonly ILogger<MainPage> _logger;

	public MainPage(IApiService apiService, ILogger<MainPage> logger)
	{
		_apiService = apiService;
		_logger = logger;

		_logger.LogInformation("MainPage constructor called.");
		InitializeComponent();
	}

    private void OnGetDataClicked(object sender, EventArgs e)
	{
		_logger.LogInformation("OnGetDataClicked called.");

		CounterLabel.Text = $"Test Data: {_apiService?.GetTestData()}";
		SemanticScreenReader.Announce(CounterLabel.Text);
	}
}
```

Run the application and logging information will be shown in the Output window.

![Logging Information](images/725f4831a774fa58cba6c03c62e3754b9e8d7e6834b23f98c248b1ccfb584a8c.png)  

#### Configure Animations

After quite some research, I was not able to find any information as of what does `ConfigureAnimations` do or needed for, so I ended up going straight to the code, after all, MAUI is open-source right?

In matter of seconds, I found the code for the extension method `ConfigureAnimations`, and it looks like this:

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Maui.Animations;

#if __ANDROID__
using Microsoft.Maui.Platform;
#endif

namespace Microsoft.Maui.Hosting
{
	public static partial class AppHostBuilderExtensions
	{
		public static MauiAppBuilder ConfigureAnimations(this MauiAppBuilder builder)
		{
#if __ANDROID__
			builder.Services.TryAddSingleton<IEnergySaverListenerManager>(svcs => new EnergySaverListenerManager());
			builder.Services.TryAddScoped<ITicker>(svcs => new PlatformTicker(svcs.GetRequiredService<IEnergySaverListenerManager>()));
#else
			builder.Services.TryAddScoped<ITicker>(svcs => new PlatformTicker());
#endif
			builder.Services.TryAddScoped<IAnimationManager>(svcs => new AnimationManager(svcs.GetRequiredService<ITicker>()));

			return builder;
		}
	}
}
```

From there you can determine that all the code is doing is, adding a service called `AnimationManager`, just as we did in our previous [Adding a Service](#adding-a-service) example,  which requires a Ticker, (a `PlatformTicker` to be exact,) and in the case of Android devices, the Ticker requires a `EnergySaverListenerManager`.

So I tried, and was successfully creating a Ticker, so I can pass to an AnimationManager, which I gave it an Animation object that I created, as shown below:

```csharp
        var ticker = new Ticker();
		var animationManager = new AnimationManager(ticker);
        var animation = new Microsoft.Maui.Controls.Animation();		
		animationManager.Add(animation);
```

With this, I was hoping I could create an animation, as we did in our previous episode [MAUI Animation. The .NET Show with Carl Franklin Ep 20
](https://www.youtube.com/watch?v=EMXjaSHlvXs&list=PL8h4jt35t1wgW_PqzZ9USrHvvnk8JMQy_&index=20&t=43s), and assign it to the `Animation` object above to be reused, but that is as far as the effort went. I was not able to create an actual animation and assign it to the `Animation` object.

I wanted to add a "WiggleAnimation" to the `CounterLabel` with the following code: 

```csharp
		CounterLabel.Animate("WiggleAnimation", animation);

		// Wiggle animation
		CounterLabel.RotateTo(1, 100);
		await CounterLabel.ScaleTo(1.05, 100);
		await CounterLabel.ScaleTo(1, 100);
		await CounterLabel.RotateTo(-2, 100);
		CounterLabel.RotateTo(0, 50);
```

You may want to still add that code at the end of `OnGetDataClicked` inside `MainPage.xaml.cs` to see what it does.

At the end, I was not able to make anything useful out of it.

However, there are two lessons to be learned here:

1. Either `ConfigureAnimations`, `AnimationManager` and `Animation` objects are used internally, and do not meant to give us animation capabilities, or we still have more things to wait for and learn regarding the development of MAUI, (we will just need to wait and see,) and

2. It may be easier than you thought, to go ahead and explore open-source code, and see how things are made of to understand things better, or even contribute to it. You may find out you can contribute more than you thought.

#### Register Handlers

In order to register your own handlers, call the `ConfigureMauiHandlers` method on the `MauiAppBuilder` object. Then, on the `IMauiHandlersCollection` object, call the AddHandler method to add the required handler.

Add the following code to MauiProgram.cs:

```csharp
			// Configure MAUI Handlers
			.ConfigureMauiHandlers(handlers =>
			{
				handlers.AddHandler(typeof(MyViewType), typeof(MyViewTypeHandler));
			})
```

Now go ahead and create `MyViewType` using the refactoring tools:

![Create MyViewType](images/ddb45af306f0b919e0aa5f2e716833309eaf5f5495ec682cf548bf4fca74c1de.png)  

Then go ahead and create `MyViewTypeHandler`, also using the refactoring tools:

![Create MyViewTypeHandler](images/7a79082d31ce157c6dc8c7b886a926a3a8b45fb6cdb96e78ee10a1477de3703c.png)

Then, the `MyViewTypeHandler` handler is registered against the `MyViewType` control. Therefore, any instances of the MyViewType control will be handled by the MyViewTypeHandler.

In there, you can add any handler code, to customize things for instance, the background color of all controls just for Android for instances, or remove underlines for Android, etc.

## Conclusion

We barely scratched the surface of what is possible in the MauiProgram class, and demoed some of the most common usages of it. To learn more, follow the links in the Resources section down below.

## Complete Code

The complete code for this demo can be found in the link below.

- <https://github.com/payini/MauiProgram>

## Resources

| Resource Title                                     | Url                                                                                                                                      |
| -------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------- |
| The .NET Show with Carl Franklin                   | <https://www.youtube.com/playlist?list=PL8h4jt35t1wcontainergW_PqzZ9USrHvvnk8JMQy_>                                                      |
| MAUI Templates Missing                             | <https://github.com/dotnet/maui/issues/5355?msclkid=6320df98ce5011ec9343dac76b4764f4>                                                    |
| Configure fonts, services, and handlers at startup | https://docs.microsoft.com/en-us/dotnet/maui/fundamentals/app-startup?msclkid=932cab7dce7511ec85837ed885f1ad6a                           |
| .NET Generic Host                                  | https://docs.microsoft.com/en-us/dotnet/core/extensions/generic-host                                                                     |
| Configure Animations                               | https://github.com/dotnet/maui/blob/3717ce0d385d1aadbd3b9795c203d438426aae17/src/Core/src/Hosting/Animations/AppHostBuilderExtensions.cs |
| Customize controls with handlers                   | https://docs.microsoft.com/en-us/dotnet/maui/user-interface/handlers/customize?msclkid=43512179d0f711ecbf2aa7647c70b84e                  |