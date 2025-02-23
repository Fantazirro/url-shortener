# Url Shortener
Это веб-приложение, который создаёт короткие URL-адреса, которые перенаправляют на другие сайты

<h3 align="center">Главная страница</h3>
<p align="center">
<img src="https://i.imgur.com/lfr5PoR.jpeg" alt="Url Shortener" height="400" style="align:center;"/>
</p>

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
