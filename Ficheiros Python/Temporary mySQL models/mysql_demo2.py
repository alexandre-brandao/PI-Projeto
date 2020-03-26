import mysql.connector

mydb = mysql.connector.connect(
    host="localhost",  # server location
    user="root",  # root username
    passwd="password",  # root password
    database="mydatabase"  # connect to your database
)

mycursor = mydb.cursor()

sql = "INSERT INTO customers (name, address) VALUES (%s, %s)"
val = ("Michelle", "Blue Village")
mycursor.execute(sql, val)

mydb.commit() # allways commit after a removal/insert or update action

print("1 record inserted, ID:", mycursor.lastrowid)