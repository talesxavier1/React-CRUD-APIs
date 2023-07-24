using SingularChatAPIs.BD;
using SingularChatAPIs.Loger;
using SingularChatAPIs.utils;

var builder = WebApplication.CreateBuilder(args);
MongoDBConnection.start();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var logger = new LoggerService();
app.Use(async (context, next) => {
    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");


    /* = = = = = = = = = = = = = = = = = = = = LOGGER = = = = = = = = = = = = = = = = = = = = */
    /*
    var headerDictionary = new Dictionary<string, string>();
    foreach (var VALUE in context.Request.Headers.Keys) {
        headerDictionary.Add(VALUE, context.Request.Headers[VALUE]);
    }
    string jsonString = JsonSerializer.Serialize(headerDictionary);


    context.Request.EnableBuffering();
    logger.printLog(new Log() {
        body = await new StreamReader(context.Request.Body, Encoding.UTF8).ReadToEndAsync(),
        logDate = DateTime.UtcNow.AddHours(-3),
        method = context.Request.Method,
        path = context.Request.Path,
        headers = jsonString,
        isProd = !app.Environment.IsDevelopment()
    });
    context.Request.Body.Position = 0;
    */
    /* = = = = = = = = = = = = = = = = = = = = = = =  = = = = = = = = = = = = = = = = = = = = */

    await next.Invoke();
});

app.UseCors(x => {
    x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
});

String SwaggerEnabled = AppSettings.appSetting["SwaggerEnabled"];
if (SwaggerEnabled.Equals("true")) {
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();




