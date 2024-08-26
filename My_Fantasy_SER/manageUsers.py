# -*- coding: utf-8 -*-
from json import JSONEncoder
from json import JSONDecoder
import json
import os.path
import pickle
import uuid



class User(object):
    """__init__() functions as the class constructor"""
    def __init__(self, name=None):

        self.name = name
        self.userId = str(uuid.uuid4())


class Users:
    myUsers = []

    def __init__(self):
        self.readFile()

    def toJSON(self, onlymine):
        return json.dumps(onlymine, default=lambda o: o.__dict__,
            sort_keys=True, indent=4)

    def getUsers(self):
        return Users.myUsers

    def addUser(self, name):

        if not self.isExist(name):
            print("name added = " + name)
            Users.myUsers.append(User(name))
            self.writeFile()
            return "Added"
        else:
            print("name InList = " + name)
            return "InList"


    def isExist(self, name):
        for i in Users.myUsers:
            if i.name == name:
                return True
                break

        return False

    #def printMyList():
    #    #print myUsers[0]
    #    for i in myUsers:
    #        print (i.name + ", id = " + i.userId)


    def readFile(self):
        p = "C:\\tstJson\\users.json"
        if os.path.exists(p):
            m = open("C:\\tstJson\\users.json", "rb")
            if os.path.getsize(p) > 0:

                Users.myUsers = pickle.load(m)
            else:
                Users.myUsers = []
            m.close()
        else:
            Users.myUsers = []

    def writeFile(self):

        x = pickle.dumps(Users.myUsers)
        p = "C:\\tstJson\\users.json"
        g = open(p, "wb")
        g.write(x)
        g.close()





        #f = open(p, "wb")
        #f.write(content)
        #f.close()


    def userToID(self, user):
        for i in Users.myUsers:
            if i.name is user:
                return i.userId


    def idToName(self, idPar):
        for i in Users.myUsers:
            if i.userId is idPar:
                return i.name



def main():
    """
    Add Documentation here
    """
#    pass  # Add Your Code Here
#    myUsers.append(addUser("a",23))
    readFile()

    print ("add = " + addUser("Noam", 33))
    print ("add = " + addUser("WOW", 22))
    print ("add = " + addUser("Ronnie", 44))
    print ("add = " + addUser("Ronnie", 44))

    printMyList()

if __name__ == '__main__':
    main()