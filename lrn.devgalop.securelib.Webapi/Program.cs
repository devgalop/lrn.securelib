using lrn.devgalop.securelib.Infrastructure.Security.JWT.Extensions;
using lrn.devgalop.securelib.Infrastructure.Security.TOTP.Extensions;
using lrn.devgalop.securelib.Infrastructure.Security.EncryptDecrypt.Extensions;
using lrn.devgalop.securelib.Infrastructure.Security.JWT.Middleware;
using lrn.devgalop.securelib.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomServices();
builder.Services.AddJwtSecurity();
builder.Services.AddAesEncryption();
builder.Services.AddRSAEncryption();
builder.Services.AddTOTP();
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
app.UseMiddleware<JwtAuthenticationMiddelware>();
app.UseAuthorization();


app.MapControllers();
app.MapHealthChecks("/healthy");

app.Run();
