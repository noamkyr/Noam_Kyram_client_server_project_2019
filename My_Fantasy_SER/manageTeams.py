import flask
import requests
import pickle
import json
import json
import os.path
import pickle
import uuid
from json import JSONEncoder
from json import JSONDecoder
import manageUsers
import sumOfList

class Team(object):
    """__init__() functions as the class constructor"""
    def __init__(self, id1,id2,id3,id4,id5, user, nameTeam, year):
        self.players = [id1,id2,id3,id4,id5]
        self.name = nameTeam

        self.user = user

        v = manageUsers.Users()
        userId = v.userToID(user)

        self.ownerId = userId
        self.year = year
        self.teamId = str(uuid.uuid4())
        if id1 != "x":
            self.points = sumOfList.sumlst([id1,id2,id3,id4,id5],year)
        else:
            self.points = 0

class MyTeamsClass:
    myTeamsList = []

    def addTeam(self, id1,id2,id3,id4,id5, user, nameTeam, year):

        newT = Team(id1,id2,id3,id4,id5, user, nameTeam, year)
        self.myTeamsList.append(newT)
        self.writeFile()
        return newT, "Added"



    def retTheT(self, name):
        for i in self.myTeamsList:
            if i.name == name:
                return i
                break



    def isExist(self, nameTeam):
        for i in self.myTeamsList:
            if i.name == nameTeam:
                return True
                break

        return False

    #def printMyList():
    #    #print myUsers[0]
    #    for i in myTeams:
    #        print (i.name + ", id = " + i.ownerId)


    def readFile(self):
        p = "C:\\tstJson\\Teams.json"

#        global myTeams
        if os.path.exists(p):
            m = open(p, "rb")
            if os.path.getsize(p) > 0:
                self.myTeamsList = pickle.load(m)
            else:
                self.myTeamsList = []
            m.close()
        else:
            print("not found")

    def writeFile(self):

        x = pickle.dumps(self.myTeamsList)
        p = "C:\\tstJson\\Teams.json"
        g = open(p, "wb")
        g.write(x)
        g.close()




def main():
    """
    Add Documentation here
    """
#    readFile()


#    x, y = addTeam("1","2","3","4","5","theId","my stars","2018")
#    print (y)
#    x, y = addTeam("1","2","3","4","5","theId","a","2018")
#    print (y)
#    x, y = addTeam("1","2","3","4","5","theId","all stars","2018")
#    print (y)
#    x, y = addTeam("1","2","3","4","5","theId","all stars","2018")
#    print (y)



    #printMyList()


if __name__ == '__main__':
    main()