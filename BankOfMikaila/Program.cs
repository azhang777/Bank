using BankOfMikaila.Config;
using BankOfMikaila.Data;
using BankOfMikaila.Middleware;
using BankOfMikaila.Models;
using BankOfMikaila.Models.DTO;
using BankOfMikaila.Repository;
using BankOfMikaila.Repository.IRepository;
using BankOfMikaila.Response;
using BankOfMikaila.Services;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection")));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IBillRepository, BillRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IWithdrawalRepository, WithdrawalRepository>();
builder.Services.AddScoped<IDepositRepository, DepositRepository>();
builder.Services.AddScoped<IP2PRepository, P2PRepository>();

builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<BillService>();
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<WithdrawalService>();
builder.Services.AddScoped<DepositService>();
builder.Services.AddScoped<P2PService>();

builder.Services.AddScoped<CustomerResponse>();
builder.Services.AddScoped<AccountResponse>();
builder.Services.AddScoped<BillResponse>();
builder.Services.AddScoped<TransactionResponse>();
builder.Services.AddScoped<WithdrawalResponse>();
builder.Services.AddScoped<DepositResponse>();
builder.Services.AddScoped<P2PResponse>();

builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection("Kafka"));
builder.Services.Configure<SchemaRegistryConfig>(builder.Configuration.GetSection("SchemaRegistry"));

builder.Services.AddSingleton<ISchemaRegistryClient>(sp =>
{
    var config = sp.GetRequiredService<IOptions<SchemaRegistryConfig>>();

    return new CachedSchemaRegistryClient(config.Value);
});

builder.Services.AddSingleton<IProducer<string, AccountDTO>>(sp =>
{
    var config = sp.GetRequiredService<IOptions<ProducerConfig>>();
    var schemaRegistry = sp.GetRequiredService<ISchemaRegistryClient>();

    return new ProducerBuilder<String, AccountDTO>(config.Value)
        .SetValueSerializer(new JsonSerializer<AccountDTO>(schemaRegistry).AsSyncOverAsync())
        .Build();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy",
        policy =>
        {
            policy.
                WithOrigins("http://localhost:5173").
                AllowAnyHeader().
                AllowAnyMethod();
        });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddHangfire((sp, config) => 
{
    var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("DefaultSQLConnection");
    config.UseSqlServerStorage(connectionString);
});
builder.Services.AddHangfireServer();
//builder.Host.UseSerilog((context, configuration) => 
//{
//    configuration.WriteTo.Console().MinimumLevel.Information();
//}); //hard code serilog configuiration or

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
}); //configure serilog from application settings

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging(); //allows us to log http requests

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("MyPolicy");

app.UseAuthorization();
//method 1:
//app.Use(async (context, next) =>
//{
//    try
//    { //every request will be executed inside try catch block statement. The exception thrown during the execution and not handled explicitly will be caught by catch block!
//        await next(context);
//    }
//    catch (Exception ex)
//    {
//        //ErrorResponse errorResponse = new() {}

//        Console.WriteLine(ex);
//        throw;
//    }
//});

//method 2:
app.UseMiddleware<GlobalMiddleware>();

app.MapControllers();

app.UseHangfireDashboard();

app.Run();
