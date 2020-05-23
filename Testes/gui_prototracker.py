import tkinter
from tkinter import *
import time

prototracker = tkinter.Tk()
prototracker.attributes("-fullscreen",True)

altice_logo = Canvas(prototracker, width = 1000, height = 1000)
altice_logo.pack()
logo = PhotoImage(file="/home/pi/Desktop/altice_logo.png")
altice_logo.create_image(175,250, image=logo)

entry = Canvas(prototracker, width = 1000, height = 1000)
entry.pack()
entryimg = PhotoImage(file="/home/pi/Desktop/entry.png")
entryid = altice_logo.create_image(600,250, image=entryimg)
altice_logo.itemconfigure(entryid, state='hidden')

#time.sleep(3)

exitsig = Canvas(prototracker, width = 1000, height = 1000)
exitsig.pack()
exitimg = PhotoImage(file="/home/pi/Desktop/exit.png")
exitid = altice_logo.create_image(600,250, image=exitimg)
altice_logo.itemconfigure(exitid, state='hidden')

def normalstate():
    altice_logo.itemconfigure(entryid, state='hidden')
    altice_logo.itemconfigure(exitid, state='hidden')

def entrydetected():
    #entryid = altice_logo.create_image(600,250, image = entryimg)
    altice_logo.itemconfigure(entryid, state='normal')
    altice_logo.after(1000,lambda: altice_logo.itemconfigure(entryid, state='hidden'))

def exitdetected():
    #altice_logo.create_image(600,250, image = exitimg)
    altice_logo.itemconfigure(exitid, state='normal')
    altice_logo.after(1000,lambda: altice_logo.itemconfigure(exitid, state='hidden'))


entrybutton = Button(prototracker, text="entry", command = entrydetected, anchor = W)
entrybutton_window = altice_logo.create_window(10,10,anchor= NW, window = entrybutton)

exitbutton = Button(prototracker, text="exit", command = exitdetected, anchor = W)
exitbutton_window = altice_logo.create_window(100,10,anchor= NW, window = exitbutton)

#normalbutton = Button(prototracker, text="normal", command = normalstate, anchor = W)
#normalbutton_window = altice_logo.create_window(200,10,anchor= NW, window = normalbutton)



prototracker.mainloop()
