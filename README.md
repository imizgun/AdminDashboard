# Admin Dashboar

## Стек
 - Бэкенд - `ASP.NET Core 8 (minimal API)`
 - База данных - `PostgreSQL`
 - Фронтенд - `Vite` + `React`

## Запуск проекта
 - Для запуска использовать команду `docker-compose up --build` из корневой папки проекта (убедитесь, что запущен движок докера)
 - Если у вас установлена PostgreSQL, возможен запуск команадами `dotnet run` из папки `AdminDashboardServer` и `npm install` + `npm run dev` из папки `admin-dashboard-front`
 - При любом варианте запуска бэкенд будет доступен по `localhost:5000`, фронтенд - по `localhost:5173`

## Структура фронтенда
### Routes:
 - `/login` - страница входа
 - `/dashboard` - страница с клиентами и курсом
 - `/payments` - страница с платежами, отсортированными по убыванию даты

## Структура бэкенда
### Эндпоинты
 - ` GET /clients` - возвращает список клиентов
 - `POST /clients` - добавляет нового клиента. Тело запроса:
 ```
    {
    "name": "string",
    "email": "string",
    "balanceT": 0
    }
 ```
 - `PUT /clients{id}` - изменить клиента по `id`. Тело запроса:
 ```
    {
    "name": "string",
    "email": "string",
    "balanceT": 0
    }
 ```
  - `DELETE /clients{id}` - удалить клиента по `id`

  - `GET /payments?take=x&skip=y` - возвращает `x` платежей, пропустив `y`
  - `GET /rate` - возвращает текущий курс
  - `POST /rate` - изменяет текущий курс. Тело запроса:
  ```
    {
    "rate": 0
    }
  ```
  - `POST /auth/login` - войти в систему (креды - `admin@mirra.dev` + `admin123`). Тело запроса:
  ```
    {
    "email": "string",
    "password": "string"
    }
  ```
  Возвращает новый `access-token`, действителен 5 минут.
