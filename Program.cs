using LethalCompany_Backend;

var builder = WebApplication.CreateBuilder(args);

builder.AddAPI(); //add controller, swagger, apis


//Enable CORS(dýþarýdan gerçekleþen iþlemlere izin)
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});



var app = builder.Build();

//enable cors devamý
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//authentication ekle
//app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
