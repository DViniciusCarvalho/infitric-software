int Vin = 0; 
int Vout = 0; 
int R1 = 30000; 
int R2 = 7500; 
float valorLido = 0; 
 
void setup()
{
   pinMode(A2, INPUT); 
   Serial.begin(9600); 
}

void loop()
{  
   valorLido = analogRead(A2); 
   Vin = (valorLido * 5.0) / 1024.0; 
   Vout = Vin / (R2/(R1+R2));
   int tensao2 = Vout * 1000;    
   Serial.println(Vout);     
   delay(500); 
}
