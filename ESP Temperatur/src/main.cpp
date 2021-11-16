#include <Arduino.h>

#include <Adafruit_Sensor.h>
#include <DHT.h>
#include <DHT_U.h>

#include <ESP8266HTTPClient.h>
#include <ESP8266WiFi.h>
#include <WifiClient.h>

#define DHTPIN 2 // Datapin

#define DHTTYPE DHT11

DHT_Unified dht(DHTPIN, DHTTYPE);

uint32_t delayMS;

WiFiClient client;
HTTPClient http;

void setup()
{
  Serial.begin(9600);

  dht.begin();

  WiFi.begin("NodeMCU_Station_Mode", "h5pd091121_Styrer"); // H5PD091121 Klasseværelset
  //WiFi.begin("FTTH_SC7618", "TrobotAcjov9"); // Hjemme hos Graesholt!
  while (WiFi.status() != WL_CONNECTED) // Wait for WiFI connection
  {
    delay(500);
    Serial.println(F("Connecting to WiFi"));
  }
}

void loop()
{
  delay(1000); // Delay between measurements.

  sensors_event_t event;
  dht.temperature().getEvent(&event); // Get temperature
  if (isnan(event.temperature))
  {
    Serial.println(F("Error reading temperature!"));
  }
  else
  {
    Serial.print(F("Temperature: "));
    Serial.print(event.temperature); // Print temperature
    Serial.println(F("°C"));

    if (WiFi.status() == WL_CONNECTED) // If WiFi connected
    {
      http.begin(client, "http://api.temp.computerx.dk:5000/temperatures"); // Request destination
      http.addHeader("Content-Type", "application/json"); // Request content-type header
      String content = "{\"TemperatureCentigrade\":" + String(event.temperature) + ", \"Room\":{\"RoomName\": \"H5PD091121\"}}"; // Request content

      int httpCode = http.POST(content); // Send the request
      String payload = http.getString(); // Get response

      Serial.println(httpCode); // Print HTTP return code
      Serial.println(payload); // Print response

      http.end(); // Close connection
    }
    else
    {
      Serial.println(F("Error in WiFi connection"));
    }
  }

  dht.humidity().getEvent(&event); // Get humidity
  if (isnan(event.relative_humidity))
  {
    Serial.println(F("Error reading humidity!"));
  }
  else
  {
    Serial.print(F("Humidity: "));
    Serial.print(event.relative_humidity); // Print humidity
    Serial.println(F("%"));

    if (WiFi.status() == WL_CONNECTED) // If WiFi connected
    {
      http.begin(client, "http://api.temp.computerx.dk:5000/moistures"); // Request destination
      http.addHeader("Content-Type", "application/json"); // Request content-type header
      String content = "{\"MoistureValue\":" + String(event.relative_humidity) + ", \"Room\":{\"RoomName\": \"H5PD091121\"}}"; // Request content

      int httpCode = http.POST(content); // Send the request
      String payload = http.getString(); // Get response

      Serial.println(httpCode); // Print HTTP return code
      Serial.println(payload); // Print response

      http.end(); // Close connection
    }
    else
    {
      Serial.println(F("Error in WiFi connection"));
    }
  }
}
