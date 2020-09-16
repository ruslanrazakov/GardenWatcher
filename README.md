# GardenWatcher

ASP.NET core MVC Client-Server application that helps tracking of growing plants with Arduino.

GardenWatcher application consists of 3 parts:
1. Arduino and sensors side
2. Web-api server side (Server folder)
3. Web client side (Client folder)


# Arduino and sensors
There are 3 types of sensors that server awaits: temperature, light and humidity. Arduino sketch is in the Server folder, it takes measures from analog pins 0-2 and creates string like t33l22h11 with some interval (1 minute). This string is enrolling in COM.

# Web-api server side
In Server folder there is ASP.Net core web-api application.
There are three main parts of this app:

1. Business Context</br>
Includes main Hangfire background job: every hour Hangfire sheduler in ArduinoTrackingService starts reading from COM port and writing measures in DB. Also, it tryes to get last photo in /home/Photos and writes path in DB.</br>
When the application starts, IBashService, starts bash script, that turns on fswebcam linux application every 24h and saves photo in /home/Photos.

2. Data Context</br>
Contains Model, ApplicationContext and ApllicationRepository

3. MVC layer</br>
Contains HomeController, that can returns measures from DB with Get() method, or GetById() in JSON.

All layers were implemented as services and were placed in DI containers.

# Web Client side
In progress. Now it is a simple ASP.Net core MVC web application, that gets data from server with HttpGetRequest() method in controller.

# Installation

