#!/usr/bin/env python3 Author : Alexandre Brandao
#	 : Rui Silva
# Date   : 29/03/2020


import RPi.GPIO as GPIO
from database_conf import *
from  mfrc522 import SimpleMFRC522
import tkinter
from tkinter import *

#General Data

prototracker = tkinter.Tk()
prototracker.attributes("-fullscreen",True)

maincanvas = Canvas(prototracker, width = 1000, height = 1000)
maincanvas.pack()
logo = PhotoImage(file="altice_logo.png")
maincanvas.create_image(175,250, image=logo)

entryimg = PhotoImage(file="entry.png")
entryid = maincanvas.create_image(600,250, image=entryimg)
maincanvas.itemconfigure(entryid, state='hidden')
exitimg = PhotoImage(file="exit.png")
exitid = maincanvas.create_image(600,250, image=exitimg)
maincanvas.itemconfigure(exitid, state='hidden')

def entrydetected():
    maincanvas.itemconfigure(entryid, state='normal')
    maincanvas.after(1500,lambda: maincanvas.itemconfigure(entryid, state='hidden'))

def exitdetected():
    maincanvas.itemconfigure(exitid, state='normal')
    maincanvas.after(1500,lambda: maincanvas.itemconfigure(exitid, state='hidden'))


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
			print("OUTSIDE")
			exitdetected()
		else:
			print(Location)
			#Update data
			update_location(cursor, "Raspberry Reader", id, Location)
			#Update history data related to the Prototype
			add_to_history(cursor, id, Location)
			entrydetected()
			
			
		cnx.commit()   # uploads data to the database
		
		
	finally:
		GPIO.cleanup()

cursor.close() # Closes the cursor(To be ignored for now)



