#SREVER SIDE
import socket


def dosomething(data):

    """this func manages the commands"""
    print ("dosomething data = " + data)

    com = data.split("/")

    print ("spint 1 = " + com[1])

    retstr = "function is " + com[1]

    return retstr
"""
    if com[0] == 'add':
        retstr = doAdd(com)
    elif com[0] == 'sub':
        retstr = doSub(com)
    elif com[0] == 'mul':
        retstr = doMul(com)



    return retstr
"""


s = socket.socket(socket.AF_INET,socket.SOCK_STREAM)
s.bind(('0.0.0.0', 5000))
s.listen(1)


while 1:

    print("server is up")
    conn, addr = s.accept()
    data = conn.recv(2048)
    print ("data = " + str(data))

    retstr = dosomething(str(data))


    retstr = 'HTTP/1.1 200 OK\r\n\r\n' + retstr
    conn.send(str.encode(retstr))

    conn.close()


