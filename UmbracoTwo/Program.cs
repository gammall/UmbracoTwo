using Umbraco.Commerce.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.CreateUmbracoBuilder()
    .AddBackOffice()
    .AddWebsite()
    .AddUmbracoCommerce(commerceBuilder =>
    {
        commerceBuilder.AddSQLite();
    })
    .AddDeliveryApi()
    .AddComposers()
    .Build();

WebApplication app = builder.Build();

await app.BootUmbracoAsync();

app.UseHttpsRedirection();

app.UseUmbraco()
    .WithMiddleware(u =>
    {
        u.UseBackOffice();
        u.UseWebsite();
    })
    .WithEndpoints(u =>
    {
        u.UseBackOfficeEndpoints();
        u.UseWebsiteEndpoints();
    });

await app.RunAsync();
