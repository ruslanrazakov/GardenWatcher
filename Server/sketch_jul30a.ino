
int temperature = 0;
int light = 0;
int humidity = 0;


void setup() {
  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  light = analogRead(0);
  humidity = analogRead(1);
  temperature = analogRead(2);
  Serial.print("t");
  Serial.print(temperature);
  Serial.print("l");
  Serial.print(light);
  Serial.print("h");
  Serial.println(humidity);
  
  delay(5000);
}
