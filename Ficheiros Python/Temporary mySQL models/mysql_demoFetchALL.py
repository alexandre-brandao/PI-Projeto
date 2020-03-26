import mysql.connector

mydb = mysql.connector.connect(
    host="192.168.1.109",  # server location
    user="rasp1",  # root username
    passwd="rasp1",  # root password
    database="Altice_Labs_db"  # connect to your database
)

mycursor = mydb.cursor()

mycursor.execute("SELECT * FROM prototipo")
myresult = mycursor.fetchall()

# print all results
for x in myresult:
  print(x)