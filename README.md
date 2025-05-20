Для запуска проекта после клонирования репозитория необходимо выполнить начальную настройку.
---
откройте и измените значения словаря params в файле configurator.py своими, после чего запустите его двойным кликом или через консоль: скрипт создаст нужные файлы конфигураций и баз данных. при желании можно выполнить это вручную: 
1. Конфигурация:
  1. скопируйте файл appsettings.template.json из папки `ExpenseManager/config` в папку `ExpenseManager/src/backend/ExpenseManager.WebAPI`; переименуйте файл в appsettings.json и заполните своими данными.
  2. скопируйте файл .env.template из папки `ExpenseManager/config` в папку `ExpenseManager/src/frontend`; переименуйте файл в .env и заполните своими данными.
2.  откройте терминал и перейдите в папку `ExpenseManager/src/backend`, создав и применив миграцию: 
  1. `dotnet tool install --global dotnet-ef` (Если у вас не установлено средство EF)
  2. `dotnet ef migrations add InitialCreate --project ./ExpenseManager.Infrastructure --startup-project ./ExpenseManager.WebAPI`
  3. `dotnet ef database update --project ./ExpenseManager.Infrastructure --startup-project ./ExpenseManager.WebAPI`
---
Теперь можно запустить проект:
1. выполните из папки `ExpenseManager/src/backend` команду `dotnet run --project ./ExpensesManager.WebAPI`
2. выполните из папки `ExpenseManager/src/frontend` команду `npm run dev`
3. перейдите по адресу `https://localhost:{ваш порт для фронтенда}` (указан в консоли, где вы запустили проект фронтенда)
