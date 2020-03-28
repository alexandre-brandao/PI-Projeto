#Author: Alexandre Brandao
#Author: Rui
#Date  : 27/03/2020
import sys
import time
import mysql.connector
#Notes: Instead if returning db, try to return just the cursor do f:DBconnection

#Settings
# ACTIVE_HISTORY = 1; #Uncomment for history

#Function to connect to database and return cursor
def DBconnection():
	try:
		cnx = mysql.connector.connect(
			user='rasp1', 
			password='rasp1', 
			host="192.168.1.109", 
			database='altice_labs_db')
		
		cursor = cnx.cursor()
	except mysql.connector as e:
		print("ERROR in database connection")
		sys.exit(1)
	
	return cnx, cursor

#Insert cursor here
def update_location(cursor, NAME, TagID, Location):
	#Check Status
	check_status(cursor, TagID, Location)
	
	update_location = ("UPDATE protótipo \
	SET localização = %s, \
	Reg_nome = %s, \
	Reg_data = NOW() \
	WHERE Código_tag = %s")
	
	values = ('Edificio 1 Andar 1', NAME, TagID)
	cursor.execute(update_location, values)
	
# Update Prototype history to database
def add_to_history(cursor, TagID, Location, ACTIVE_HISTORY=0):
	#GET DATE AND TIME
	DATE_TIME = time.strftime('%Y-%m-%d %H:%M:%S')
	DATE_TIME = DATE_TIME.split(" ")
	
	insert_stmt = ("INSERT INTO histórico (Código_tag, Local, Data, Time) \
	VALUES (%s, %s, %s,%s)") 
	
	data = (TagID, Location, DATE_TIME[0], DATE_TIME[1])

	cursor.execute(insert_stmt, data)
	
	# ACTIVE HISTORY CHECK FLAG
	if ACTIVE_HISTORY == 1: 
		cursor.execute("SELECT * FROM  histórico")
		
		#Print all sequences
		print("------HISTORY TABLE------")
		for x in cursor.fetchall() :
			print(x)
		print("-------------------------")
	

		
#Check if the device if in the building
# Features: Return True  if was just created
# 			Return False if not
def check_status(cursor, TagID, CurrentLocation):
	select_stmt = ("SELECT Local FROM  histórico WHERE Código_tag =" + str(TagID)+";")
	data = (TagID)
	cursor.execute(select_stmt, data)
	data = cursor.fetchall()
	

	if not data: #If there is no history to this Prototype
		update_field = ("UPDATE protótipo \
						SET status = %s")
		info = ("IN-BUILDING")
		print("Prototype status: In Building \nJust Registed\n")
		
	else :		#If there is history to this Prototype
		lastLoc = data[-1][0]
		if  lastLoc!= CurrentLocation : #Did it change locations? Yes
			update_field = ("UPDATE protótipo SET status = %s WHERE Código_tag = %s")
			info = ("IN-BUILDING", TagID)
			cursor.execute(update_field, info)
			print("Prototype status: IN-BUILDING \nWent from "+lastLoc+"-->"+CurrentLocation+"\n")
				
			
		else:		#Did it change locations? NO
			#Get current status
			select_stmt = ("SELECT status FROM  protótipo WHERE Código_tag =" + str(TagID)+";")
			cursor.execute(select_stmt, (TagID))
			status = cursor.fetchall()
			status = status[0][0]
			
			if status != 'OUTSIDE':
				update_field = ("UPDATE protótipo SET status = %s WHERE Código_tag = %s")
				info = ("OUTSIDE", TagID)
				cursor.execute(update_field, info)
				print("Prototype status: OUTSIDE\n")
				
			else:
				update_field = ("UPDATE protótipo  SET status = %s WHERE Código_tag = %s")
				info = ("IN-BUILDING", TagID)
				cursor.execute(update_field, info)
				print("Prototype status: IN-BUILDING \nWent to " +CurrentLocation+"\n")
			
	

	
	
