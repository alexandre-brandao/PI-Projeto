#!/usr/bin/env python3 Author : Alexandre Brandao
#	 : Rui Silva
# Date   : 29/03/2020


import RPi.GPIO as GPIO
#from database_conf import *
from  mfrc522 import SimpleMFRC522
import tkinter
from tkinter import *

#General Data

prototracker = tkinter.Tk()
prototracker.attributes("-fullscreen",True)

maincanvas = Canvas(prototracker, width = 1000, height = 1000)
maincanvas.pack()
logo = PhotoImage(file="/home/pi/Desktop/altice_logo.png")
maincanvas.create_image(175,250, image=logo)

entryimg = PhotoImage(file="/home/pi/Desktop/entry.png")
entryid = maincanvas.create_image(600,250, image=entryimg)
maincanvas.itemconfigure(entryid, state='hidden')
exitimg = PhotoImage(file="/home/pi/Desktop/exit.png")
exitid = maincanvas.create_image(600,250, image=exitimg)
maincanvas.itemconfigure(exitid, state='hidden')

def entrydetected():
    maincanvas.itemconfigure(entryid, state='normal')
    maincanvas.after(1000,lambda: maincanvas.itemconfigure(entryid, state='hidden'))

def exitdetected():
    maincanvas.itemconfigure(exitid, state='normal')
    maincanvas.after(1000,lambda: maincanvas.itemconfigure(exitid, state='hidden'))

Connected = 0
Location = "room"

#while Connected == 0:
#	try:
#		reader = SimpleMFRC522()
#		cnx, cursor = DBconnection()
#		Connected = 1
#	finally:
#		print("Connected\n")

while 1:
    print("Enter location: ")		
    ch = input()
    if ch == Location:
            exitdetected()
    else:
            entrydetected()

#cursor.close() # Closes the cursor(To be ignored for now)
    #prototracker.mainloop()



