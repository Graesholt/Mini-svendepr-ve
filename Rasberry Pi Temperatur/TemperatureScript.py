#Libraries
import Adafruit_DHT as dht
from time import sleep
import requests
DHT = 4 #DATA pin
while True:
    try:
        humidity, temperature = dht.read_retry(dht.DHT22, DHT)
    
        print('Temp=' + str(temperature) + '*C')
        tempUrl = 'https://api.temp.computerx.dk/temperatures'
        tempContent = {'TemperatureCentigrade': temperature, 'Room':{'RoomName': 'Kantinen'}}
        tempRequest = requests.post(tempUrl, json = tempContent)
        print(tempRequest.text)

        print('Humidity=' + str(humidity) + '%')
        humUrl = 'https://api.temp.computerx.dk/Moistures'
        humContent = {'MoistureValue': humidity, 'Room':{'RoomName': 'Kantinen'}}
        humRequest = requests.post(humUrl, json = humContent)
        print(humRequest.text)

    except Exception as ErrorValue:
        print(ErrorValue)

    sleep(1)