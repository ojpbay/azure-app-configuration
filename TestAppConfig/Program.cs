using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Retrieve the app configuration endpoint
string endpoint = builder.Configuration.GetValue<string>("Endpoints:AppConfiguration")
    ?? throw new InvalidOperationException("The setting `Endpoints:AppConfiguration` was not found.");

// Load configuration from Azure App Configuration 
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(new Uri(endpoint), new DefaultAzureCredential());
});


// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
