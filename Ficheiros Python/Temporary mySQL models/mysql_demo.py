 
import mysql.connector

mydb = mysql.connector.connect(
  host="192.168.1.109",       # server location
  user="rasp1",            # root username
  passwd="rasp1",      # root password
  database="Altice_Labs_DB"   # connect to your database
)

mycursor = mydb.cursor()
#mycursor.execute("CREATE DATABASE mydatabase")
#mycursor.execute("SHOW DATABASES")
#mycursor.execute("CREATE TABLE customer (id INT AUTO_INCREMENT PRIMARY KEY, name VARCHAR(255), address VARCHAR(255))")
#mycursor.execute("CREATE TABLE customers (name VARCHAR(255), address VARCHAR(255))")
#mycursor.execute("ALTER TABLE customers ADD COLUMN id INT AUTO_INCREMENT PRIMARY KEY")
#mycursor.execute("DESCRIBE protótipo")
sql = "INSERT INTO protótipo(Código_tag, Nome, Id_protótipo, Projeto, Localização, Reg_nome, Reg_data, Rem_nome, Rem_data) VALUES (%s, %s, %s, %s, %s, %s, %s,%s, %s)"
values=("103030303", "PROTOBOY", '303', 'Teste1', 'Edificio 1 - Andar 1', 'Alexandre Brandao RASP', '2020-03-26', 'Alexandre Brandao RASP', '2020-03-26') 

# or we can insert multiple rows
"""
values = [
  ('Peter', 'Lowstreet 4'),
  ('Amy', 'Apple st 652'),
  ('Hannah', 'Mountain 21'),
  ('Michael', 'Valley 345'),
  ('Sandy', 'Ocean blvd 2'),
  ('Betty', 'Green Grass 1'),
  ('Richard', 'Sky st 331'),
  ('Susan', 'One way 98'),
  ('Vicky', 'Yellow Garden 2'),
  ('Ben', 'Park Lane 38'),
  ('William', 'Central st 954'),
  ('Chuck', 'Main Road 989'),
  ('Viola', 'Sideway 1633')
]
"""
mycursor.execute(sql, values)  #For only 1 execution
#mycursor.executemany(sql, values)

mydb.commit()
print(mycursor.rowcount, "record inserted.")

for x in mycursor:
  print(x)
