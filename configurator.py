import os

APPSETTINGS_TEMPLATE_PATH = "./configs/appsettings.template.json"
DOTENV_TEMPLATE_PATH = "./configs/.env.template"
APPSETTINGS_PATH = "./src/backend/ExpenseManager.WebAPI/appsettings.json"
DOTENV_PATH = "./src/frontend/.env"

params = {
    "encryption-key": "your key",
    "backend-port": "5001",
    "frontend-port": "5173",
    "telegram-api-token": "your key",
    "telegram-bot-name": "your bot name",
    "db-name": "app",
    "certificate-password": "123"
}


with open(APPSETTINGS_TEMPLATE_PATH, "r") as a, open(DOTENV_TEMPLATE_PATH, "r") as b, open(APPSETTINGS_PATH, "w") as c, open(DOTENV_PATH, "w") as d:
    text = "".join(a.readlines())

    for key in params:
        text = text.replace("{" + key + "}", params[key])

    c.write(text)

    text = "".join(b.readlines())

    for key in params:
        text = text.replace("{" + key + "}", params[key])

    d.write(text)

os.system(f'dotnet dev-certs https -ep "./src/certificate.pfx" -p {params["certificate-password"]}')
os.system("dotnet ef migrations add InitialCreate --project ./src/backend/ExpenseManager.Infrastructure --startup-project ./src/backend/ExpenseManager.WebAPI")
os.system("dotnet ef database update --project ./src/backend/ExpenseManager.Infrastructure --startup-project ./src/backend/ExpenseManager.WebAPI")