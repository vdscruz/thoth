using Confluent.Kafka;
using Serilog;
using Thoth.Abstracoes.Mensageria;
using Thoth.Infra.Mensageria.Producer;
using Thoth.Scanner;
using Thoth.Scanner.Options;
using Thoth.Servico.Interfaces.ProcessamentoArquivos;
using Thoth.Servico.Servicos.ProessamentoArquivos;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging((ctx, loggerBuilder) =>
    {
        var loggerConfiguration = new LoggerConfiguration()
                    .ReadFrom.Configuration(ctx.Configuration)
                    .Enrich.FromLogContext();

        loggerBuilder
                .ClearProviders()
                .AddSerilog(loggerConfiguration.CreateLogger());
    })
    .ConfigureServices((context, services) =>
    {
        services.Configure<ScannerOptions>(context.Configuration.GetSection(nameof(ScannerOptions)));
        services.Configure<ProducerConfig>(context.Configuration.GetSection(nameof(ProducerConfig)));
        services.AddSingleton<IProducer, KafkaProducer>();
        services.AddTransient<IProcessaArquivo, ProcessaArquivoExtratoB3>();

        services.AddMediator(options =>
        {
            options.Namespace = "Thoth.Scanner";
            options.ServiceLifetime = ServiceLifetime.Transient;
        });

        services.AddHostedService<Worker>();
    })
    .UseWindowsService()
    .Build();

host.Run();
