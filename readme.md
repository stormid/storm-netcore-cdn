# Storm.AspNetCore.Cdn

Generates fully qualifed url's for resources within an AspNetCore web application via TagHelpers.

## Features

- TagHelpers for `<script>`, `<link>` and `<img>` tags that transparency update references to use a defined CDN (Content Delivery Network)
- Support for multiple active CDN providers within the same solution
- Allows specific resource paths to be excluded from CDN url generation via regular expressions or TagHelper configuration properties
- Supports appending a rotating or custom version string to all CDN urls to prevent caching in local or development environments