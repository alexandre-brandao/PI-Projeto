#!/usr/bin/env python
# Author : Alexandre Brandao
# Date   : 29/03/2020


import RPi.GPIO as GPIO
from database_conf import *
from  mfrc522 import SimpleMFRC522

#General Data
Location = "Edificio 2 - Andar 1"

reader = SimpleMFRC522()
cnx, cursor = DBconnection()

while 1:
	try:
		id,name = reader.read() # Wait for TAG
		print(id)
		print(name)
		print(Location)
		
		#Update data
		update_location(cursor, name, id, Location)
		#Update history data related to the Prototype
		add_to_history(cursor, id, Location)
		
		cnx.commit()   # uploads data to the database
		
		
	finally:
		GPIO.cleanup()

cursor.close() # Closes the cursor(To be ignored for now)



