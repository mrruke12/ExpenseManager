Для запуска проекта необходимо выполнить его настройку: 
1. создайте файл appsettings.json в папке src/ExpenseManager.WebAPI, скопируйте в него следующую структуру и заполните ее своими данными:
    `{
  "AllowedHosts": "*",
  "Jwt": {
    "Key": "your_encryption_key_at_least_32_symbols",
    "Issuer": "https://your-domain",
    "Audience": "https://your-domain"
  },
  "API": {
    "TGAPITOKEN": ""
  },
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=path_to_your_database.db",
    "TelegramBotConnection": "https://t.me/your_bot_nickname"
  },
  "Serilog": {
    "MinimumLevel": {
      "Default": "Debug",
      "Override": {
        "Microsoft": "Warning",
        "System": "Error"
      }
    },
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "outputTemplate": "{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}{Tab}{Exception}"
        }
      },
      {
        "Name": "File",
        "Args": {
          "path": "logs/log-.log",
          "rollingInterval": "Day"
        }
      }
    ]
  }
}`
---
2.  откройте терминал и перейдите в папку src, выполнив создание миграций: 
    1. `dotnet tool install --global dotnet-ef` (Если у вас не установлено средство EF)
	2. `dotnet ef migrations add InitialCreate --project ./ExpenseManager.Infrastructure --startup-project ./ExpenseManager.WebAPI`
	3. `dotnet ef database update --project ./ExpenseManager.Infrastructure --startup-project ./ExpenseManager.WebAPI`
---
Теперь можно сбилдить и запустить решение из той же папки:
`dotnet run --project ./ExpensesManager.WebAPI --environment "Development"`