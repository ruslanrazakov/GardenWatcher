#include <OneWire.h>
#include <DallasTemperature.h>
#include <BH1750.h>
#include <Wire.h>

int temperature = 0;
uint16_t light = 0;
int humidity = 0;

BH1750 lightMeter;    // uses last analog ports   (A4) and (A5)
int humidityPin = 0   // port for humidity sensor (A0)
OneWire oneWire(15);  // port for temp sensor     (A1)
DallasTemperature ds(&oneWire);

void setup() {
  Serial.begin(9600);
  Wire.begin();
  lightMeter.begin();
  ds.begin();
}

void loop() {
  ds.requestTemperatures();   

  temperature =  ds.getTempCByIndex(0);
  light = lightMeter.readLightLevel();
  humidity = analogRead(humidityPin);
  
  Serial.print("t");
  Serial.print(temperature);
  Serial.print("l");
  Serial.print(light);
  Serial.print("h");
  Serial.println(humidity);
  
  delay(5000);
}