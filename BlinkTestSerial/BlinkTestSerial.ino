const int jumpPin = 2;
const int duckPin = 3;

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
    Serial.println("-1");
  }
  else if(readJumpValue == HIGH)
  {
    Serial.println("1");
  }
  else
  {
    Serial.println("0");
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
