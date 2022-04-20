# specify the scheme element using NSwag
```app.UseOpenApi(configure => configure.PostProcess = (document, _) => document.Schemes = new[] { NSwag.OpenApiSchema.Https });```