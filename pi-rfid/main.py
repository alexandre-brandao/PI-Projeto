#!/usr/bin/env python3 
#Author : Alexandre Brandao
#	 : Rui Silva
# Date   : 29/03/2020


import RPi.GPIO as GPIO
from database_conf import *
from  mfrc522 import SimpleMFRC522

#General Data
Connected = 0
Location = "Building 3, Floor 1"

while Connected == 0:
	try:
		reader = SimpleMFRC522()
		cnx, cursor = DBconnection()
		Connected = 1
	finally:
		print("Connected\n")
		
while 1:
	try:
		id,name = reader.read() # Wait for TAG
		print(id)
		
		if already_in(cursor, id, Location):
			#Update data
			update_location(cursor, "Raspberry Reader", id, "OUTSIDE")
			#Update history data related to the Prototype
			add_to_history(cursor, id, "OUTSIDE")
			print("OUTSIDE\n")
		else:
			print(Location+"\n")
			#Update data
			update_location(cursor, "Raspberry Reader", id, Location)
			#Update history data related to the Prototype
			add_to_history(cursor, id, Location)
			
		cnx.commit()   # uploads data to the database
		
		
	finally:
		GPIO.cleanup()
		print("", end='')

cursor.close() # Closes the cursor(To be ignored for now)



