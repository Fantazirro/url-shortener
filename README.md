# Url Shortener
Это Web API, который создаёт короткие URL-адреса, которые перенаправляют на другие сайты

## Настройка сервиса
Настроить сервис можно с помощью следующих файлов конфигурации:
* **appsettings.json** - конфигурация и настройка API
* **compose.yml** - конфигурация всей системы

## Запуск системы
Для запуска системы вам нужны следующие инструменты: Docker и Docker Compose  

Команда для запуска:
```bash
sudo docker compose up --build
```

## Доступные эндпоинты
* **GET /api/{code}** - переход на сайт по короткому URL-адресу
* **POST /api** - создание короткой ссылки
  - **request** - объект, содержащий изначальный URL-адрес

Также список доступных эндпоинтов можно узнать с помощью инструмента Swagger

## Стек технологий
* **Web API**: ASP.NET Core  
* **Фронтенд**: HTML, CSS, JS  
* **База данных**: PostgreSQL  
* **Кэш-сервер**: Redis  
* **Прокси**: Nginx  
* **Контейнеризация**: Docker

## Библиотеки и фреймворки
* **Entity Framework Core** - для подключения к базе данных
* **Swagger** - для документирования API
* **Microsoft.Extensions.Caching.StackExchangeRedis** - для подключения к кэш-серверу
