#Author: Rui Silva
#Created 2/03/20


def sendToDb(tag_code):
    import mysql.connector

    cnx = mysql.connector.connect(
    user='rasp1', 
    password='rasp1', 
    host="192.168.1.109", 
    database='Altice_Labs_db')
    cursor = cnx.cursor()

    update_location = ("UPDATE protótipo SET localização = %s, Reg_nome = %s, Reg_data = NOW() WHERE Código_tag = %s")
    data_local = ('Edificio 1 - Andar 1', 'rasp1', tag_code)
    
    cursor.execute(update_location, data_local)
    cnx.commit()
    cnx.close()
    return


"""
Private Ip: 192.168.1.181
Public Ip: 149.90.230.242
proto_status():
    if andar != andar_lido:
        current_proto_status == 'ENTRY'
    else:
        if current_proto_status == 'ENTRY':
            current_proto_status = 'EXIT'
        else :
            current_proto_status = 'ENTRY'
"""
