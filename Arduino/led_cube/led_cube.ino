#include <MsTimer2.h>

const int anode_pins[] = {2,3,4,5,6,7,8,9,10};
const int base_pin = 11;
const int cathode_pin = 12;
static int filter_id = 0;

const int digits[] = {
  0b000000001, //0
  0b000000010, //1
  0b000000100, //2
  0b000001000, //3
  0b000010000, //4
  0b000100000, //5
  0b001000000, //6
  0b010000000, //7
  0b100000000, //8
  0b000000111, //9
  0b000111000, //10
  0b111000000, //11
  0b000111000, //12
  0b000000111, //13
  0b100010001, //14
  0b001001001, //15
  0b100010001, //16
  0b000000111, //17
  0b000100110, //18
  0b100100100, //19
  0b110100000, //20
  0b111000000, //21
  0b011001000, //22
  0b001001001, //23
  0b000001011, //24
  0b000000111, //25
  0b000100110, //26
  0b100100100, //27
  0b110100000, //28
  0b111000000, //29
  0b011001000, //30
  0b001001001, //31
  0b000001011, //32
  0b000000111, //33
  0b000111111, //34
  0b111111111, //35
  0b000000000, //36
};

void display_number(int n){
  for(int i=0; i<9; i++){
    digitalWrite(anode_pins[i], digits[n] & 1 << i ? HIGH : LOW);
  }
}

void display_led(){
  display_number(filter_id);
}

void setup() {
  Serial.begin(9600);
  
  for(int i=0; i < sizeof(anode_pins); i++){
    pinMode(anode_pins[i], OUTPUT);
  }
  pinMode(cathode_pin, OUTPUT);
  pinMode(base_pin, OUTPUT);

  digitalWrite(cathode_pin, LOW);//cathode_common
  digitalWrite(base_pin, HIGH);// switch on!!!

  MsTimer2::set(15, display_led);
  MsTimer2::start();
}

void loop() {
  filter_id = (int)getFilterIdBySerial();
  delay(500);
}

int getFilterIdBySerial(){
  if(Serial.available()){
    return (int)Serial.read();
  }
  return 0;
}

