#Author: Rui Silva
#Created 1/03/20
#Updated 2/03/20

import mysql.connector

cnx = mysql.connector.connect(user='rasp1', password='rasp1', host='192.168.1.109', database='altice_labs_db')
cursor = cnx.cursor()

update_location = ("UPDATE protótipo SET localização = %s, Reg_nome = %s, Reg_data = NOW() WHERE nome = %s")

codigo = 'resist'

data_local = ('Edificio 1 - Andar 1', 'rasp1', codigo)

cursor.execute(update_location, data_local)

cnx.commit()