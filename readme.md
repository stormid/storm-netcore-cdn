# Storm.AspNetCore.Cdn

Generates fully qualifed url's for resources within an AspNetCore web application via TagHelpers.

## Features

- TagHelpers for `<script>`, `<link>` and `<img>` tags that transparency update references to use a defined CDN (Content Delivery Network)
- Support for multiple active CDN providers within the same solution
- Allows specific resource paths to be excluded from CDN url generation via regular expressions or TagHelper configuration properties
- Supports appending a rotating or custom version string to all CDN urls to prevent caching in local or development environments

# Purpose 

The purpose of this library is to support a transparent workflow  around the inclusion and usage of a CDN to supply assets to a web application running under .NET core 2+.  The library itself targets `netstandard2.0`.

# Installation

The library can be installed via the NuGet package manager:

```
Install-Package Storm.AspNetCore.Cdn
```

# Configuration

Once installed there are 3 steps to setting up a working configuration and utilising the included taghelpers to transparently augment your asset paths to use a CDN.

## 1. Configure the required services within your `Startup` class

```c#
public class Startup
{
  public Startup() { }

  public void ConfigureServices(IServiceCollection services)
  {
    // this will add the default CDN configuration
    services.AddCdn();
  }
}
```

There are additional configuration options available from the `.AddCdn()` extension that allow the customisation of the underlying `ICdnUriProvider` used to generate a valid uri from a given resource path, and also the ability to use a custom implementation of a `ICdnVersionUriProvider`, both are explained later.  The final option is `.ApplyDnsPrefetchTags()`, with this enabled each of your configured CDN providers will have an associated dns-prefetch link tag generated within the `<head>` section of the html document, by default this is disabled.

## 2. Configure CDN Providers within your application configuration

The library supports the configuration of multiple active CDN providers and allows specific providers to be used for specific assets via the included TagHelpers.  The configuration is read from a root configuration node called `Cdn`, see a complete example configuration below:

```json
{
  "Cdn": {
    "DefaultProvider" : "MyProvider",
    "Providers": {
      "TemplateProvider" : {
        "Host": "<valid dns or ip address host using either http or https schemes - can not include a path>",
        "Prefix": "<optional: prefix to use for each resource uri, defaults to / - must be a valid path beginning with /",
        "ExcludePattern" : "<optional: regex pattern matching resources that ignore the cdn>",
        "VersionPattern" : "<optional: regex pattern matching resources that should have a version parameter appended to uri>",
      },
      "MyProvider" : {
        "Host": "https://anothercdn.host.com/",
        "Prefix": "/static/",
        "VersionPattern" : "((.*).js)$"
      },
      "Special" : {
        "Host": "https://special-cdn.host.com/",
        "Prefix": "/static/files/"
      }
    }
  },
}
```

## 3. Add TagHelpers
In order to use the included TagHelpers you will need to add the following lines to an approriate Razor view file, typically this would be your `_ViewImports.cshtml`:

```cshtml
@addTagHelper *, Storm.AspNetCore.Cdn
```

This will enable TagHelpers that function over the existing tags for `<script>`, `<link>` and `<img>`.  You do not need to modify your existing tags if you want them to be transparently converted to use a configured CDN provider.  Here is an example:

```html
<html>
  <head>
    <link rel="stylesheet" href="/css/styles.css" crossorigin="anonymous" />
    <link rel="stylesheet" href="https://some.fontlibrary.com/fonts/myfont.css" crossorigin="anonymous" />
  </head>
  <body>
    <img src="/img/logo.png" />
    <img src="~/local/image.png" />

    <script type="text/javascript" src="/js/app.js" crossorigin="anonymous"></script>
    <script type="text/javascript" src="/js/app.js" storm-cdn-provider="Special" crossorigin="anonymous"></script>
  </body>
</html>
```
This razor file will be rendered as (excluding the comments):

```html
<html>
  <head>
    <!-- default cdn provider is applied -->
    <link rel="stylesheet" href="https://anothercdn.host.com/static/css/styles.css" crossorigin="anonymous" />

    <!-- ignored as it is already an absolute uri -->
    <link rel="stylesheet" href="https://some.fontlibrary.com/fonts/myfont.css" crossorigin="anonymous" />

    <!-- added via the .ApplyDnsPrefetchTags() -->
    <link rel="dns-prefetch" href="https://anothercdn.host.com/">
  </head>
  <body>
    <!-- default cdn provider is applied -->
    <img src="https://anothercdn.host.com/static/img/logo.png" />

    <!-- resources that begin with ~ will be ignored as they are expected to represent local virtual paths -->
    <img src="/local/image.png" />

    <!-- default cdn provider is applied, including version parameter due to "VersionPattern" match -->
    <script type="text/javascript" src="https://anothercdn.host.com/static/js/app.js?_v=9384759384753984"  crossorigin="anonymous"></script>

    <!-- named cdn provider is applied -->
    <script type="text/javascript" src="https://special-cdn.host.com/static/files/js/app.js" crossorigin="anonymous"></script>
  </body>
</html>
```