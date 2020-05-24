#Authors: Alexandre Brandao
#	    : Rui
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
			user='projectpial', 
			password='Uh4r?SyJV85~', 
			host="den1.mysql4.gear.host", 
			database='projectpial')


		cursor = cnx.cursor()
	except mysql.connector as e:
		print("ERROR in database connection")
		sys.exit(1)

	return cnx, cursor

#Insert cursor here
def update_location(cursor, device_name, TagID, Location):

	update_location = ("UPDATE prototype \
	SET location = %s, \
	device = %s \
	WHERE tag_code = %s")

	values = (Location,device_name, TagID)
	cursor.execute(update_location, values)

# Update Prototype history to database
def add_to_history(cursor, TagID, Location, ACTIVE_HISTORY=0):
	#GET DATE AND TIME
	DATE_TIME = time.strftime('%Y-%m-%d %H:%M:%S')

	insert_stmt = ("INSERT INTO history (tag_code, location, date) \
	VALUES (%s, %s, %s)")

	data = (TagID, Location, DATE_TIME)


	cursor.execute(insert_stmt, data)

	# ACTIVE HISTORY CHECK FLAG
	if ACTIVE_HISTORY == 1: 
		cursor.execute("SELECT * FROM  history")

		#Print all sequences
		print("------HISTORY TABLE------")
		for x in cursor.fetchall() :
			print(x)
		print("-------------------------")



#Check if the device if in the building
# Features: Return True  if was just created
# 			Return False if not
def already_in(cursor, TagID, CurrentLocation):
	select_stmt = ("SELECT location FROM prototype WHERE tag_code =" + str(TagID)+";")
	data = (TagID)
	cursor.execute(select_stmt, data)
	data = cursor.fetchall()

	if len(data) != 0 and  data[0][0] == CurrentLocation: 
		return True

	else :
		return False

