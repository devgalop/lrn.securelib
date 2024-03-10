using lrn.devgalop.securelib.Infrastructure.Security.JWT.Extensions;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Extensions;
using lrn.devgalop.securelib.Infrastructure.Security.JWT.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtSecurity();
builder.Services.AddAesEncryption();
builder.Services.AddRSAEncryption();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<JwtAuthenticationMiddelware>();

app.MapControllers();
app.MapHealthChecks("/healthy");

app.Run();
