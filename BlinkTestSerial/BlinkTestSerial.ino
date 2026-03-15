const int jumpPin = 2;
const int duckPin = 3;
bool jumpButton = false;
bool duckButton = false;

// the setup function runs once when you press reset or power the board
void setup() {
  // initialize digital pin LED_BUILTIN as an output.
  Serial.begin(9600);
  pinMode(LED_BUILTIN, OUTPUT);
  pinMode(jumpPin, INPUT);
  pinMode(duckPin, INPUT);
}

// the loop function runs over and over again forever
void loop() {

  int readJumpValue = digitalRead(jumpPin);
  int readDuckValue = digitalRead(duckPin);
  
  if(readDuckValue == HIGH)
  {
    if(duckButton == false)
    {
      duckButton = true;
      if(Serial.available()){
        Serial.write("DuckTrue");
        Serial.flush();
      }
    }
  }
  else
  {
    if(duckButton == true)
    {
      duckButton = false;
      if(Serial.available()){
        Serial.write("DuckFalse");
        Serial.flush();
      }
    }
  }

  if(readJumpValue == HIGH)
  {
    if(jumpButton == false)
    {
      jumpButton = true;
      if(Serial.available()){
        Serial.write("JumpTrue");
        Serial.flush();
      }
    }
  }
  else
  {
    if(jumpButton == true)
    {
      jumpButton = false;
      if(Serial.available()){
        Serial.write("JumpFalse");
        Serial.flush();
      }
    }
  }

  if(Serial.available()){
    char cmd = Serial.read();
    if(cmd == '1'){
      digitalWrite(LED_BUILTIN, HIGH);
    }
    if(cmd == '0'){
      digitalWrite(LED_BUILTIN, LOW);
    }
  }
}
