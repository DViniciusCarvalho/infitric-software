const float R1 = 30000.0; 
const float R2 = 7500.0; 
float vin = 0.0; 
float vout = 0.0; 
float valor_lido = 0; 
 
void setup()
{
   pinMode(A2, INPUT); 
   Serial.begin(9600); 
}

void loop()
{  
   float valor_leitura = analogRead(A2); 
   vin = (valor_lido * 5.0) / 1024.0; 
   vout = vin / (R2/(R1+R2));
   int valor_final = vout * 1000;    
   Serial.println(valor_final);  
   delay(200); 
}
