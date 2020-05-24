#!/usr/bin/env python3 Author : Alexandre Brandao
#	 : Rui Silva
# Date   : 29/03/2020


import RPi.GPIO as GPIO
from database_conf import *
from  mfrc522 import SimpleMFRC522
import tkinter
from tkinter import *

#General Data

def entrydetected():
    maincanvas.itemconfigure(entryid, state='normal')
    maincanvas.after(1500,maincanvas.itemconfigure(entryid, state='hidden'))

def exitdetected():
    maincanvas.itemconfigure(exitid, state='normal')
    maincanvas.after(1500,lambda: maincanvas.itemconfigure(exitid, state='hidden'))

def mainexec(Location,id,name):
    Connected = 0
    while Connected == 0:
        try:
            reader = SimpleMFRC522()
            cnx, cursor = DBconnection()
            Connected = 1
        finally:
            print("Connected\n")
    try:
        print(id)
        if already_in(cursor, id, Location):
            #Update data
            update_location(cursor, "Raspberry Reader", id, "OUTSIDE")
	    #Update history data related to the Prototype
            add_to_history(cursor, id, "OUTSIDE")
            print("OUTSIDE")
            #exitdetected()
            maincanvas.itemconfigure(exitid, state='normal')
            maincanvas.after(1500,lambda: maincanvas.itemconfigure(exitid, state='hidden'))
        else:
            print(Location)
            #Update data
            update_location(cursor, "Raspberry Reader", id, Location)
            #Update history data related to the Prototype
            add_to_history(cursor, id, Location)
            #entrydetected()
            maincanvas.itemconfigure(entryid, state='normal')
            maincanvas.after(1500,maincanvas.itemconfigure(entryid, state='hidden'))

        cnx.commit()   # uploads data to the database

    finally:
        GPIO.cleanup()
        cursor.close() # Closes the cursor(To be ignored for now)
        name = '0'

prototracker = tkinter.Tk()
prototracker.attributes("-fullscreen",True)

maincanvas = Canvas(prototracker, width = 1000, height = 1000)

logo = PhotoImage(file="altice_logo.png")
logoid = maincanvas.create_image(175,250, image=logo)

entryimg = PhotoImage(file="entry.png")
entryid = maincanvas.create_image(600,250, image=entryimg)
maincanvas.itemconfigure(entryid, state='hidden')
exitimg = PhotoImage(file="exit.png")
exitid = maincanvas.create_image(600,250, image=exitimg)
maincanvas.itemconfigure(exitid, state='hidden')

maincanvas.itemconfigure(logoid, state='normal')
maincanvas.pack()

Location = "Building 3, Floor 1"
reader = SimpleMFRC522()
name = '0'
interaction = 2
		
while 1:
    #maincanvas.after(1500,lambda: maincanvas.itemconfigure(entryid, state='hidden'))
    #maincanvas.after(1500,lambda: maincanvas.itemconfigure(exitid, state='hidden'))
    print("Waiting for tag")
    #id,name = reader.read()
    time.sleep(10)
    id = 54321
    name = 'Test'
    if name != '0':
        mainexec(Location,id,name)
        #prototracker.after(30000, mainexec(Location))
        
    prototracker.update_idletasks()
    prototracker.update()
    time.sleep(0.01)




